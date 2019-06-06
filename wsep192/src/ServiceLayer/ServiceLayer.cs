using Newtonsoft.Json;
using src.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace src.ServiceLayer
{
    class ServiceLayer
    {
        private static ServiceLayer instance = null;
        private TradingSystem system;
        private Dictionary<String, int> users;
        private Dictionary<String, int> stores;
        private Dictionary<String, int> permissions;
        private Dictionary<string, List<bool>> requests;
        private Dictionary<string, List<string>> storesStackholders = new Dictionary<string, List<string>>();
        private Dictionary<int, OwnerRequest> systemRequests; //reqId - flag,count,tmpcount
        private NotificationsManager manager = new NotificationsManager();
        private int storeCounter;
        private int userCounter;
        private int requestCounter;


        private ServiceLayer()
        {
            system = new TradingSystem(new ProductSupplySystemImpl(), new FinancialSystemImpl());
            users = new Dictionary<String, int>();
            stores = new Dictionary<String, int>();
            permissions = new Dictionary<String, int>();
            storeCounter = 0;
            userCounter = 0;
            manager.init();
            addPermissions();
            systemRequests = new Dictionary<int, OwnerRequest>();

            //setUp();

        }

        public static bool fileSetUp()
        {
            return SystemState.fileSetUp();
        }


        public static ServiceLayer getInstance()
        {
            if (instance == null)
            {
                instance = new ServiceLayer();
                fileSetUp();
            }
            return instance;
        }
        public void shutDown()
        {
            instance = null;
        }



        public bool setUp1()
        {
            bool flag = true;
            string user = instance.initUser();
            flag = flag & instance.register("user", "user", user);
            flag = flag & instance.openStore("store", "user");
            flag = flag & instance.createNewProductInStore("product", "cat", "details", 10, "store", "user");
            List<KeyValuePair<string, int>> products = new List<KeyValuePair<string, int>>();
            products.Add(new KeyValuePair<string, int>("product", 10));
            if (!flag)
                return flag;
            flag = flag & instance.addProductsInStore(products, "store", "user");
            return flag;

        }

        public bool setUp()
        {
            bool flag = true;
            string user = initUser();
            flag = flag & register("user", "user", user);
            flag = flag & signIn("user", "user");
            string[] stores = { "Zara", "Bershka", "Forever21", "Castro", "Renuar", "AmericanEagle" };
            string[] details = { "New", "On Sale", "Last chance", "Hot staff" };
            string[] cats = { "Tops", "Jeans", "Shoes", "Skirts" };
            for (int i = 0; i < 1 && flag; i++)
            {
                flag = flag & openStore(stores[i], "user");
                for (int j = 0; j < 3 && flag; j++)
                {
                    string cat = cats[new Random().Next(0, 4)];
                    string[] product = { cat + (j + 1).ToString(), cat, details[new Random().Next(0, 4)], stores[i] };
                    flag = flag & createNewProductInStore(product[0], product[1], product[2], new Random().Next(10, 100), product[3], "user");
                    List<KeyValuePair<string, int>> products = new List<KeyValuePair<string, int>>();
                    products.Add(new KeyValuePair<string, int>(product[0], new Random().Next(10, 100)));
                    if (flag)
                        flag = flag & addProductsInStore(products, stores[i], "user");
                }
            }

            /**********/
            string user2 = initUser();
            flag = flag & register("maor", "1", user2);
            flag = flag & assignOwner("user", "maor", "Zara");
            // flag = flag & assignOwner("user", "maor", "Bershka");
            flag = flag & removeOwner("maor", "Zara", "user");
            flag = flag & signOut("user");
            return flag;

        }

        private void addPermissions()
        {
            permissions.Add("AddDiscountPolicy", 1);
            permissions.Add("AddPurchasePolicy", 2);
            permissions.Add("CreateNewProductInStore", 3);
            permissions.Add("AddProductsInStore", 4);
            permissions.Add("RemoveProductsInStore", 5);
            permissions.Add("EditProductInStore", 6);
            permissions.Add("CommunicationWithCustomers", 7);
            permissions.Add("PurchasesHistory", 8);
        }

        public string initUser()
        {
            string user;
            do
            {
                user = getId(users.Count);
            }
            while (users.ContainsKey(user));
            system.initUserGuest(user, userCounter);
            users.Add(user, userCounter);
            userCounter++;
            return user;
        }

        //req1.1
        public bool init(String adminName, String adminPassword)
        {
            bool result = system.init(adminName, adminPassword, userCounter);
            if (result)
            {
                users.Add(adminName, userCounter);
                userCounter++;
            }
            return result;
        }
        //req2.2
        public bool signIn(String username, String password)
        {
            if (!users.ContainsKey(username))
                return false;
            bool flag = system.signIn(username, password, users[username]);
            if (flag)
            {
                foreach (String message in system.getMessagesByUser(users[username]))
                    notify(username, message);
                system.deleteMessagesByUser(users[username]);

                foreach (OwnerRequest r in system.getRequestsByUser(users[username]))
                {
                    request(username, r.message, r.id);
                }
                system.deleteRequestsByUser(users[username]);


            }
            return flag;
        }


        //req2.3
        public bool register(String username, String password, String user)
        {
            if (!users.ContainsKey(user) || users.ContainsKey(username))//CHANGED
                return false;

            bool result = system.register(username, password, users[user]);
            if (result)
            {
                int key = users[user];
                users.Remove(user);
                users.Add(username, key);
            }

            return result;
        }
        //req2.5
        public String searchProduct(String details)
        {
            return system.searchProduct(details);
        }
        //req2.6
        public bool addProductsToCart(List<KeyValuePair<String, int>> products, String store, String user)
        {
            if (!users.ContainsKey(user) || !stores.ContainsKey(store) || !productsExist(products, stores[store]))
            {
                return false;
            }
            return system.addProductsToCart(getProductsInts(products, stores[store]), stores[store], users[user]);

        }

        private bool productsExist(List<KeyValuePair<String, int>> products, int store)
        {
            foreach (KeyValuePair<String, int> pair in products)
            {
                if (!system.productExist(pair.Key, store))
                    return false;
            }
            return true;
        }

        public bool[] getVisibility(String userName)
        {
            return system.getVisibility(users[userName], userName);
        }

        private List<KeyValuePair<int, int>> getProductsInts(List<KeyValuePair<String, int>> products, int store)
        {
            List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
            foreach (KeyValuePair<String, int> pair in products)
            {
                list.Add(new KeyValuePair<int, int>(system.getProduct(pair.Key, store), pair.Value));
            }
            return list;
        }

        //req2.7
        public List<KeyValuePair<String, int>> showCart(String store, String user)
        {
            if (!users.ContainsKey(user) || !stores.ContainsKey(store))
            {
                return null;
            }
            return system.showCart(stores[store], users[user]); ;
        }
        public bool editProductQuantityInCart(String product, int quantity, String store, String user)
        {
            if (!users.ContainsKey(user) || !stores.ContainsKey(store) || !system.productExist(product, stores[store]) || quantity < 0)
            {
                return false;
            }
            return system.editProductQuantityInCart(system.getProduct(product, stores[store]), quantity, stores[store], users[user]);
        }
        public bool removeProductsFromCart(List<string> productsToRemove, String store, String user)
        {
            if (!users.ContainsKey(user) || !stores.ContainsKey(store) || !productsExist2(productsToRemove, stores[store]))
            {
                return false;
            }
            return system.removeProductsFromCart(getProductsInts2(productsToRemove, stores[store]), stores[store], users[user]);
        }

        private List<int> getProductsInts2(List<String> products, int store)
        {
            List<int> list = new List<int>();
            foreach (String p in products)
            {
                list.Add(system.getProduct(p, store));
            }
            return list;
        }

        private bool productsExist2(List<String> products, int store)
        {
            foreach (String p in products)
            {
                if (!system.productExist(p, store))
                    return false;
            }
            return true;
        }

        //req2.8
        public double basketCheckout(String address, String user)
        {
            if (!users.ContainsKey(user))
                return -1;
            return system.basketCheckout(address, users[user]);
        }

        public List<String[]> payForBasket(long cardNum, DateTime date, String user)
        {
            List<String[]> output;
            if (!users.ContainsKey(user))
            {
                output = new List<string[]>();
                String[] soutput = { "Error: invalid user" };
                output.Add(soutput);
                return output;
            }
            output = system.payForBasket(cardNum, date, users[user]);
            if (output != null)
            {
                List<String> stores = system.getOrderStoresByUser(users[user]);
                foreach (String store in stores)
                {
                    notifyAll(store, user + " successfully ordered.");
                }
            }
            return output;

        }


        //req3.1
        public bool signOut(String user)
        {
            if (!users.ContainsKey(user))
                return false;
            return system.signOut(users[user]);
        }

        //req3.1
        public bool openStore(String storeName, String user)
        {
            if (!users.ContainsKey(user))
                return false;
            if (stores.ContainsKey(storeName)) //ADDED
                return false;

            bool result = system.openStore(storeName, users[user], storeCounter);
            if (result)
            {
                stores.Add(storeName, storeCounter);
                storeCounter++;
                List<string> users = new List<string>();
                users.Add(user);
                storesStackholders.Add(storeName, users);
            }
            return result;
        }


        //req4.1
        public bool createNewProductInStore(String productName, String category, String details, int price, String store, String user)
        {
            if (!users.ContainsKey(user) || !stores.ContainsKey(store))
            {
                return false;
            }
            return system.createNewProductInStore(productName, category, details, price, stores[store], users[user]);
        }

        internal bool assignOwnerResult(int reqId, bool result)
        {
            if (!systemRequests.ContainsKey(reqId))
                return false;
            OwnerRequest r = systemRequests[reqId];
            r.responsesCounter++;
            r.result &= result;
            if (r.result && r.responsesCounter == r.storesOwnersCount - 1)
            {
                return assignOwner(r.owner, r.user, r.store);
            }
            return true;
        }

        //pair of <produc,quantity>
        public bool addProductsInStore(List<KeyValuePair<String, int>> productsToAdd, String store, String user)
        {
            if (!users.ContainsKey(user) || !stores.ContainsKey(store) || !productsExist(productsToAdd, stores[store]))
            {
                return false;
            }
            return system.addProductsInStore(getProductsInts(productsToAdd, stores[store]), stores[store], users[user]);
        }
        public bool removeProductsInStore(List<KeyValuePair<String, int>> productsToRemove, String store, String user)
        {
            if (!users.ContainsKey(user) || !stores.ContainsKey(store) || !productsExist(productsToRemove, stores[store]))
            {
                return false;
            }
            return system.removeProductsInStore(getProductsInts(productsToRemove, stores[store]), stores[store], users[user]);
        }
        public bool editProductInStore(String product, String productName, String category, String details, int price, String store, String user)
        {
            if (!users.ContainsKey(user) || !stores.ContainsKey(store) || !system.productExist(product, stores[store]))
            {
                return false;
            }
            return system.editProductInStore(system.getProduct(product, stores[store]), productName, category, details, price, stores[store], users[user]);
        }

        //req4.3

        public bool assignOwner(String owner, String user, String store)
        {
            if (!users.ContainsKey(owner) || !users.ContainsKey(user) || !stores.ContainsKey(store))
                return false;
            bool flag = system.assignOwner(stores[store], users[owner], users[user]);
            if (flag)
            {
                storesStackholders[store].Add(user);
                String message = user + " have successfully assigned as an owner in " + store;
                notifyAll(store, message);
            }
            return flag;
        }

        public bool assignOwnerSetUp(String owner, String user, String store)
        {
            if (!users.ContainsKey(owner) || !users.ContainsKey(user) || !stores.ContainsKey(store))
                return false;
            bool flag = system.assignOwner(stores[store], users[owner], users[user]);
            if (flag)
            {
                storesStackholders[store].Add(user);
                String message = user + " have successfully assigned as an owner in " + store;
                notifyAllWithoutLoginUsers(store, message);
            }
            return flag;
        }

        public bool assignOwnerRequest(String owner, String user, String store)
        {

            //store,user,owner
            if (!users.ContainsKey(owner) || !users.ContainsKey(user) || !stores.ContainsKey(store))
                return false;
            String message = owner + " is requesting " + user + " to be assigned as owner in " + store;
            systemRequests.Add(requestCounter, new OwnerRequest(message, requestCounter, user, store, owner, storesStackholders[store].Count));
            requestAll(store, message, owner, requestCounter);
            requestCounter++;
            return true;
        }


        //req4.4
        public bool removeOwner(String ownerToRemove, String store, String user)
        {
            if (!users.ContainsKey(ownerToRemove) || !users.ContainsKey(user) || !stores.ContainsKey(store))
                return false;
            var res = system.removeOwner(users[user], users[ownerToRemove], stores[store]);
            if (res)
            {
                storesStackholders[store].Remove(ownerToRemove);
                String message = "You have successfully removed from being an owner in " + store;
                if (system.isLoggedIn(users[ownerToRemove]))
                    notify(ownerToRemove, message);
                else
                    system.addMessageToUser(users[ownerToRemove], message);
            }
            return res;
        }
        //req4.5
        public bool assignManager(String manager, String store, List<String> permissions, String user)
        {
            if (!users.ContainsKey(manager) || !users.ContainsKey(user) || !stores.ContainsKey(store) || !validatePermissions(permissions))
                return false;
            return system.assignManager(users[user], users[manager], stores[store], getPermissionsInts(permissions));
        }

        private bool validatePermissions(List<string> permissions)
        {
            foreach (String p in permissions)
            {
                if (!this.permissions.ContainsKey(p))
                    return false;
            }
            return true;
        }

        private List<int> getPermissionsInts(List<string> permissions)
        {
            List<int> list = new List<int>();
            foreach (String p in permissions)
            {
                list.Add(this.permissions[p]);
            }
            return list;
        }

        //req4.6
        public bool removeManager(String managerToRemove, String store, String user)
        {
            if (!users.ContainsKey(managerToRemove) || !users.ContainsKey(user) || !stores.ContainsKey(store))
                return false;
            return system.removeManager(users[user], users[managerToRemove], stores[store]);
        }
        //req6.2
        public bool removeUser(String userToRemove, String user)
        {
            if (!users.ContainsKey(userToRemove) || !users.ContainsKey(user))
                return false;
            bool result = system.removeUser(users[user], users[userToRemove]);
            if (result)
                users.Remove(userToRemove);
            return result;
        }

        public static string getId(int length)
        {
            char[] id = "0123456789".ToCharArray();
            Random _random = new Random();
            var sb = new StringBuilder(length);

            for (int i = 0; i < length; i++)
                sb.Append(id[_random.Next(10)]);

            return sb.ToString();
        }

        public int addComplexPurchasePolicy(String purchesData, String store, String user)
        {
            try
            {
                if (!users.ContainsKey(user) || !stores.ContainsKey(store))
                    return -1;
                return this.system.addComplexPurchasePolicy(purchesData, this.stores[store], this.users[user]);

            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("ServiceLayer-addComplexPurchasePolicy- Search for user or store that not exist ");
                return -1;
            }
        }
        public int addConditionalDiscuntPolicy(List<String> products, String condition, String discountPrecentage, String expiredDiscountDate, String duplicate, String logic, String user, String store)
        {
            try
            {
                if (!users.ContainsKey(user) || !stores.ContainsKey(store))
                    return -1;
                return system.addConditionalDiscuntPolicy(products, condition, Double.Parse(discountPrecentage) / 100.0, Int32.Parse(expiredDiscountDate), Int32.Parse(duplicate), Int32.Parse(logic), this.users[user], this.stores[store]);
            }
            catch (Exception e)
            {
                return -1;
            }
        }
        public int addRevealedDiscountPolicy(List<KeyValuePair<String, int>> products, String discountPrecentage, String expiredDiscountDate, String logic, String user, String store)
        {
            try
            {
                if (!users.ContainsKey(user) || !stores.ContainsKey(store))
                    return -1;
                return system.addRevealedDiscountPolicy(products, Double.Parse(discountPrecentage) / 100.0, this.users[user], this.stores[store], Int32.Parse(expiredDiscountDate), Int32.Parse(logic));
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("ServiceLayer-removeDiscountPolicy- Search for user or store that not exist or parsing failed ");
                return -1;
            }
        }

        public int removeDiscountPolicy(String discountId, String store, String user)
        {
            try
            {
                if (!users.ContainsKey(user) || !stores.ContainsKey(store))
                    return -1;
                return system.removeDiscountPolicy(Int32.Parse(discountId), this.stores[store], this.users[user]);
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public int removePurchasePolicy(String purchaseId, String store, String user)
        {
            try
            {
                if (!users.ContainsKey(user) || !stores.ContainsKey(store))
                    return -1;
                return system.removePurchasePolicy(Int32.Parse(purchaseId), this.stores[store], this.users[user]);
            }
            catch (Exception e)
            {
                return -1;
            }
        }


        public void notify(string user, string message)
        {
            if (system.isLoggedIn(users[user]))
                manager.notify(user, message);

        }

        public void notifyAll(string store, string message)
        {
            foreach (string user in storesStackholders[store])
            {
                if (system.isLoggedIn(users[user]))
                    notify(user, message);
                else
                    system.addMessageToUser(users[user], message);
            }

        }

        public void notifyAllWithoutLoginUsers(string store, string message)
        {
            foreach (string user in storesStackholders[store])
            {
                system.addMessageToUser(users[user], message);
            }



        }
        public void requestAll(string store, string message, string owner, int reqId)
        {

            foreach (string user in storesStackholders[store])
            {
                if (owner == user)
                    continue;
                if (system.isLoggedIn(users[user]))
                    request(user, message, reqId);
                else
                    system.addRequestToUser(users[user], systemRequests[reqId]);
            }

        }

        public void request(string user, string message, int reqId)
        {
            if (system.isLoggedIn(users[user]))
                manager.request(user, message, reqId.ToString());

        }
    }

}