using src.Domain;
using System;
using System.Collections.Generic;
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
        private int storeCounter;
        private int userCounter;
        private int purchasePolicyCounter;
        private int discountPolicyCounter;



        private ServiceLayer()
        {
            system = new TradingSystem(new ProductSupplySystemImpl(), new FinancialSystemImpl());
            users = new Dictionary<String, int>();
            stores = new Dictionary<String, int>();
            permissions = new Dictionary<String, int>();
            storeCounter = 0;
            userCounter = 0;
            purchasePolicyCounter = 0;
            discountPolicyCounter = 0;
            addPermissions();
            init("admin", "admin");
            setUp();

        }
        public static ServiceLayer getInstance()
        {
            if (instance == null)
                instance = new ServiceLayer();
            return instance;
        }
        public void shutDown()
        {
            instance = null;
        }


        /*
        public bool setUp()
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

        }*/

        public bool setUp()
        {
            bool flag = true;
            string user = initUser();
            flag = flag & register("user", "user", user);
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
            return flag;

        }

        private void addPermissions()
        {
            permissions.Add("Add/EditDiscountPolicy", 1);
            permissions.Add("Add/EditPurchasePolicy", 2);
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
            return system.signIn(username, password, users[username]);
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
            if (!users.ContainsKey(user))
            {
                List<String[]> output = new List<string[]>();
                String[] soutput = { "Error: invalid user" };
                output.Add(soutput);
                return output;
            }
            return system.payForBasket(cardNum, date, users[user]);
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
            return system.assignOwner(stores[store], users[owner], users[user]);
        }
        //req4.4
        public bool removeOwner(String ownerToRemove, String store, String user)
        {
            if (!users.ContainsKey(ownerToRemove) || !users.ContainsKey(user) || !stores.ContainsKey(store))
                return false;
            return system.removeOwner(users[user], users[ownerToRemove], stores[store]);
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


        public int addSimplePurchasePolicy(String type, String first, String second, String third, String fourth, String act, string adress, String isregister, String store, String user)
        {
            try
            {
                if (!users.ContainsKey(user) || !stores.ContainsKey(store))
                    return -1;
                return this.system.addSimplePurchasePolicy(Int32.Parse(type), Int32.Parse(first), Int32.Parse(second), Int32.Parse(third), Int32.Parse(fourth), Int32.Parse(act), adress, Int32.Parse(isregister) == 1, this.stores[store], this.users[user]);
            }
            catch (Exception e)
            {
                return -1;
            }

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
                return -1;
            }
        }

        public int addRevealedDiscountPolicy(List<KeyValuePair<String, int>> products, String discountPrecentage, String expiredDiscountDate, String logic, String user, String store)
        {
            try
            {
                if (!users.ContainsKey(user) || !stores.ContainsKey(store))
                    return -1;
                return system.addRevealedDiscountPolicy(products, Double.Parse(discountPrecentage), this.users[user], this.stores[store], Int32.Parse(expiredDiscountDate), Int32.Parse(logic));
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public int addConditionalDiscuntPolicy(List<String> products, String condition, String discountPrecentage, String expiredDiscountDate, String duplicate, String logic, String user, String store)
        {
            try
            {
                if (!users.ContainsKey(user) || !stores.ContainsKey(store))
                    return -1;
                return system.addConditionalDiscuntPolicy(products, condition, Double.Parse(discountPrecentage), Int32.Parse(expiredDiscountDate), Int32.Parse(duplicate), Int32.Parse(logic), this.users[user], this.stores[store]);
            }
            catch (Exception e)
            {
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
    }
}