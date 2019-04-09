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

        private TradingSystem system;
        private Dictionary<String, int> users;
        private Dictionary<String, int> stores;
        private Dictionary<String, int> permissions;
        private int storeCounter;
        private int userCounter;
        private int purchasePolicyCounter;
        private int discountPolicyCounter;



        public ServiceLayer()
        {
            system = new TradingSystem(null, null);
            users = new Dictionary<String, int>();
            stores = new Dictionary<String, int>();
            permissions = new Dictionary<String, int>();
            storeCounter = 0;
            userCounter = 0;
            purchasePolicyCounter = 0;
            discountPolicyCounter = 0;
            addPermissions();
        }


        private void addPermissions()
        {

            permissions.Add("AddDiscountPolicy", 1);
            permissions.Add("AddPurchasePolicy", 2);
            permissions.Add("EditProductQuantityInStore", 3);
            permissions.Add("AddProductToStore", 4);
            permissions.Add("RemoveProductFromStore", 5);
            permissions.Add("EditProductInStore", 6);
            permissions.Add("RemoveManager", 7);
            permissions.Add("RemoveOwner", 8);
            permissions.Add("CommunicationWithCustomers", 9);
            permissions.Add("PurchasesHistory", 10);
            permissions.Add("AssignOwner", 11);
            permissions.Add("AssignManager", 12);
            permissions.Add("CloseStore", 13);

        }

        public bool initUser(String user)
        {
            bool result = system.initUserGuest(user, userCounter);
            if (result)
            {
                users.Add(user, userCounter);
                userCounter++;
            }
            return result;
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
        public bool signIn(String username, String password, String user)
        {
            if (!users.ContainsKey(user))
                return false;
            return system.signIn(username, password, user);
        }
        //req2.3
        public bool register(String username, String password, String user)
        {
            if (!users.ContainsKey(user))
                return false;
            return system.register(username, password, user);
        }
        //req2.5
        public String searchProduct(String details)
        {
            //return system.searchProduct(details);
            return null;
        }
        //req2.6
        public bool addProductsToCart(List<KeyValuePair<String, int>> products, String store, String user)
        {
            if (!users.ContainsKey(user) || !stores.ContainsKey(store) || !productsExist(products, stores[store]))
            {
                return false;
            }
            // return system.addProductsToCart(getProductsInts(products,stores[store]), stores[store], users[user]);
            return false;
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
        public String showCart(String store, String user)
        {
            if (!users.ContainsKey(user) || !stores.ContainsKey(store))
            {
                return "Error: Invalid user or store";
            }
            return system.showCart(store, user);
        }
        public bool editProductQuantityInCart(String product, int quantity, String store, String user)
        {
            if (!users.ContainsKey(user) || !stores.ContainsKey(store) || !system.productExist(product, stores[store]))
            {
                return false;
            }
            return system.editProductQuantityInCart(system.getProduct(product, stores[store]), quantity, stores[store], users[user]);
        }
        public bool removeProductsFromCart(List<KeyValuePair<String, int>> productsToRemove, String store, String user)
        {
            if (!users.ContainsKey(user) || !stores.ContainsKey(store) || !productsExist(productsToRemove, stores[store]))
            {
                return false;
            }
            return system.removeProductsFromCart(getProductsInts(productsToRemove, stores[store]), stores[store], users[user]);
        }

        //req2.8
        public int basketCheckout(String address, String user)
        {
            if (!users.ContainsKey(user))
                return -1;
            return system.basketCheckout(address, users[user]);
        }
        public String payForBasket(long cardNum, DateTime date, String user)
        {
            if (!users.ContainsKey(user))
                return "Error: invalid user";
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
            return system.assignOwner(users[owner], users[user], stores[store]);
        }
        //req4.4
        public bool removeOwner(String ownerToRemove, String store, String user)
        {
            if (!users.ContainsKey(ownerToRemove) || !users.ContainsKey(user) || !stores.ContainsKey(store))
                return false;
            return system.removeOwner(users[ownerToRemove], stores[store], users[user]);
        }
        //req4.5
        public bool assignManager(String manager, String user, String store, List<String> permissions)
        {
            if (!users.ContainsKey(manager) || !users.ContainsKey(user) || !stores.ContainsKey(store) || !validatePermissions(permissions))
                return false;
            return system.assignManager(users[manager], users[user], stores[store], getPermissionsInts(permissions));
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
            return system.removeManager(users[managerToRemove], stores[store], users[user]);
        }
        //req6.2
        public bool removeUser(String userToRemove, String user)
        {
            if (!users.ContainsKey(userToRemove) || !users.ContainsKey(user))
                return false;
            bool result = system.removeUser(users[userToRemove], users[user]);
            if (result)
                users.Remove(userToRemove);
            return result;
        }


    }
}