using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class TradingSystem
    {
        private Dictionary<int,User> users;
        private Dictionary<int,Store> stores;
        private ProductSupplySystem supplySystem;
        private FinancialSystem financialSystem;
        private int productCounter;
        private int storeCounter;
        private int userCounter;
        private int purchasePolicyCounter;
        private int discountPolicyCounter;
        private Encryption encryption;

        public TradingSystem(ProductSupplySystem supplySystem, FinancialSystem financialSystem)
        {
            this.users = new Dictionary<int, User>();
            this.stores = new Dictionary<int, Store>();
            this.supplySystem = supplySystem;
            this.financialSystem = financialSystem;
            this.productCounter = 0;
            this.purchasePolicyCounter = 0;
            this.discountPolicyCounter =  0;
            this.encryption = new EncryptionImpl();
        }

        public int basketCheckout(String address, int userID)
        {
            if (!this.users.ContainsKey(userID))
                return -1;
            else
                return this.users[userID].basketCheckout(address);
        }
        public ShoppingBasket payForBasket(long cardNumber, DateTime date, int userID)
        {
            ShoppingBasket basket = users[userID].Basket;
            foreach (ShoppingCart cart in basket.ShoppingCarts.Values)
            {
                cart.Store.updateCart(cart, "-");
            }

            if (!this.financialSystem.payment(cardNumber, date, basket.basketCheckout()))
            {
                foreach (ShoppingCart cart in basket.ShoppingCarts.Values)
                {
                    cart.Store.updateCart(cart, "+");
                }
                return null;
            }


            if (!this.supplySystem.deliverToCustomer(this.Users[userID].Address, "Some package Details"))
            {
                return null;
            }

            return basket;
        }


        internal bool removeUser(int removingID, int toRemoveID)

        {
            if (!(users.ContainsKey(toRemoveID) && users.ContainsKey(removingID)))
            {
                LogManager.Instance.WriteToLog("TradingSystem-Remove user fail- one of the users does not exists\n");
                return false;
            }
            if (!users[removingID].IsAdmin)
            {
                LogManager.Instance.WriteToLog("TradingSystem-Remove user fail- the removing user is not an admin\n");
                return false;
            }

            if (users.Remove(toRemoveID))
            {
                LogManager.Instance.WriteToLog("TradingSystem-Remove user id" + toRemoveID +" success\n");
                return true;
            }
            return false;
        }

        internal bool openStore(string storeName, int userID, int storeCounter)
        {
            List<PurchasePolicy> purchasePolicy = new List<PurchasePolicy>();
            List<DiscountPolicy> discountPolicy = new List<DiscountPolicy>();
            if (!stores.ContainsKey(storeCounter))
            {
                Store store = new Store(storeCounter, storeName, purchasePolicy, discountPolicy);
                if (Users.ContainsKey(userID) && Users[userID].IsRegistered)
                {
                    Stores.Add(storeCounter, store);
                    User user = searchUser(userID);
                    store.initOwner(user);
                    LogManager.Instance.WriteToLog("TradingSystem-open store" +storeName+" succu\n");
                    return true;
                }
                LogManager.Instance.WriteToLog("TradingSystem-open store fail- the user does not exists or not registerd\n");
            }
            LogManager.Instance.WriteToLog("TradingSystem-open store fail- the store does not exists\n");
            return false;

        }
        public String showCart(int store, int user)
        {
            if (!Users.ContainsKey(user) || !Stores.ContainsKey(store))
                return "Error : Invalid user or store";
            return Users[user].showCart(store);

        }
        public bool editProductQuantityInCart(int product, int quantity, int store, int user)
        {
            if (!Users.ContainsKey(user) || !Stores.ContainsKey(store))
                return false;
            return Users[user].editProductQuantityInCart(product, quantity, store);
        }
        public bool removeProductsFromCart(List<KeyValuePair<int, int>> productsToRemove, int store, int user)
        {
            if (!Users.ContainsKey(user) || !Stores.ContainsKey(store))
                return false;
            return Users[user].removeProductsFromCart(productsToRemove, store);
        }


        internal bool productExist(string product, int store)
        {
            return Stores[store].productExist(product);
        }


        public bool removeManager(int userID, int userIDToRemove, int storeID)
        {
            if (!this.users.ContainsKey(userID) || !this.users.ContainsKey(userIDToRemove) || !this.stores.ContainsKey(storeID))
            {
                LogManager.Instance.WriteToLog("TradingSystem-Remove manager fail- The store or user is not exist\n");
                return false;
            }
            if (users[userID].removeManager(userIDToRemove, storeID))
            {
                LogManager.Instance.WriteToLog("TradingSystem-Remove manager id" + userIDToRemove + " from store " + storeID + " success\n");
                return true;
            }
            return false;
        }


        public bool init(string adminUserName, string adminPassword)
        {
            User admin = new User(userCounter, adminUserName, adminPassword, true, true);
            users.Add(userCounter, admin);
            userCounter++;
            if (!financialSystem.connect() || !supplySystem.connect() || !encryption.connect())
                return false;

            return true;
        }
        


        public bool removeOwner(int userID,int userIDToRemove,int storeID)
        {
            return users[userID].removeOwner(userIDToRemove, storeID);
        }
        public String searchProduct(String details)
        {
            List<ProductInStore> products  = new List<ProductInStore>();
            String[] detailsForFilter = details.Split(' ');
            if (detailsForFilter.Length != 7)
            {
                LogManager.Instance.WriteToLog("TradingSystem-search Product " + details + " bad input");

                return "";
            }
            KeyValuePair<int, int> priceRange = new KeyValuePair<int, int>(Int32.Parse(detailsForFilter[3]),
                Int32.Parse(detailsForFilter[4]));
            Filter filter = new Filter(detailsForFilter[0],
                detailsForFilter[1], detailsForFilter[2], priceRange,
                Int32.Parse(detailsForFilter[5]), Int32.Parse(detailsForFilter[6]));
            foreach (Store s in stores.Values)
            {
                s.searchProduct(filter,products);
            }
            return productsToString(products);
        }
        private String productsToString(List<ProductInStore> products)
        {
            String res = "";
            foreach(ProductInStore p in products)
            {
                res += res + "Name: " + p.Product.ProductName + "\n"
                     + "Store Name: " + p.Store.Name +"\n"
                    + "Quantity: " + p.Quantity
                    ;
            }
            return res;
        }
        internal bool initUserGuest(string user,int userCounter)
        {
            User guest = new User(userCounter, user , null,false,false);
            users.Add(userCounter, guest);

            return true;
        }

        public User searchUser(int userID)
        {
            foreach (User u in users.Values)
                if (u.Id == userID)
                    return u;
            return null;
        }
        public Store searchStore(int storeID)
        {
            foreach (Store s in stores.Values)
                if (s.Id == storeID)
                    return s;
            return null;
        }

        public int ProductCounter { get => productCounter; set => productCounter = value; }
        public int StoreCounter { get => storeCounter; set => storeCounter = value; }
        public int UserCounter { get => userCounter; set => userCounter = value; }
        public int PurchasePolicyCounter { get => purchasePolicyCounter; set => purchasePolicyCounter = value; }
        public int DiscountPolicyCounter { get => discountPolicyCounter; set => discountPolicyCounter = value; }


        internal Dictionary<int, User> Users { get => users; set => users = value; }
        internal Dictionary<int, Store> Stores { get => stores; set => stores = value; }
        internal ProductSupplySystem SupplySystem { get => supplySystem; set => supplySystem = value; }
        internal FinancialSystem FinancialSystem { get => financialSystem; set => financialSystem = value; }



        public bool init(string adminUserName, string adminPassword,int userCounter)
        {
            User admin = new User(userCounter, adminUserName, adminPassword, true, true);
            users.Add(userCounter, admin);
            if (!financialSystem.connect() || !supplySystem.connect() || !encryption.connect())
                return false;

            return true;
        }

        public bool signOut(int id) {
            if (!users.ContainsKey(id))
                return false;
            return users[id].signOut();

        }
        public Boolean register(String userName, String password, int userId)
        {
            int currUserId = userId;
            if (this.users.ContainsKey(currUserId))
            {
                if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password)
                    || userName.Equals("") || password.Equals("") || userName.Contains(" "))
                    return false;
                User currUser = this.users[currUserId];
                if (currUser != null)
                {
                    password = encryption.encrypt(userName + password);
                    return currUser.register(userName, password);
                }
                return false;
            }
            return false;
        }


        public Boolean signIn(String userName, String password, int userId)
        {
            int currUserId = userId;
            if (this.users.ContainsKey(currUserId))
            {
                User currUser = this.users[currUserId];
                if (currUser != null)
                {
                    if (!currUser.IsRegistered)
                    {
                        return false;
                    }
                    password = encryption.encrypt(userName + password);
                    if (currUser.Password == password)
                    {
                        return currUser.signIn(userName, password);
                    }
                }
                return false;
            }
            return false;
        }


        public bool addProductsToCart(List<KeyValuePair<int, int>> products, int storeId, int userId)
        {
            if (!this.Users.ContainsKey(userId) || !this.Stores.ContainsKey(storeId) || products == null)
                return false;
            LinkedList<KeyValuePair<Product, int>> toInsert = createProductsList(products, storeId);
            if (toInsert == null)
                return false;
            ShoppingCart newCartCheck = this.users[userId].addProductsToCart(toInsert, storeId);
            if (newCartCheck != null)
                newCartCheck.Store = this.stores[storeId];
            return true;
        }


        public LinkedList<KeyValuePair<Product, int>> createProductsList(List<KeyValuePair<int, int>> products, int storeId)
        {
            bool check = true;
            LinkedList<KeyValuePair<Product, int>> output = new LinkedList<KeyValuePair<Product, int>>();
            foreach (KeyValuePair<int, int> productId in products)
            {
                if (!this.Stores[storeId].Products.ContainsKey(productId.Key))
                {
                    check = false;
                }
                else
                {
                    output.AddLast(new KeyValuePair<Product, int>(this.Stores[storeId].Products[productId.Key].Product, productId.Value));
                }
            }
            if (!check)
                return null;
            return output;
        }

       internal bool createNewProductInStore(string productName, string category, string details, int price, int storeID,int userID)
        {
            if (Stores.ContainsKey(storeID))
                if(Stores[storeID].createNewProductInStore(productName, category, details, price, ProductCounter++, userID))
                {
                    LogManager.Instance.WriteToLog("TradingSystem-create new product in store"+storeID+" -success\n");
                    return true;

                }
                
            LogManager.Instance.WriteToLog("TradingSystem-create new product in store fail- the store does not exists\n");
            return false;
        }

      internal bool addProductsInStore(List<KeyValuePair<int, int>> productsInStore, int storeID, int userID)
        {
            if (Stores.ContainsKey(storeID))
                if(Stores[storeID].addProductsInStore(productsInStore, userID))
                {
                    LogManager.Instance.WriteToLog("TradingSystem-add product to store" +storeID+" -success");
                    return true;
                }
            LogManager.Instance.WriteToLog("TradingSystem-add product to store fail- the store does not exists\n");
            return false;
        }

        internal int getProduct(string product, int store)
        {
            return Stores[store].getProduct(product);
        }

        internal bool removeProductsInStore(List<KeyValuePair<int, int>> productsInStore, int storeID, int userID)
        {
            if (Stores.ContainsKey(storeID))
                if(Stores[storeID].removeProductsInStore(productsInStore, userID))
                {
                    LogManager.Instance.WriteToLog("TradingSystem-remove product from store" + storeID + " -success");
                    return true;
                }
            LogManager.Instance.WriteToLog("TradingSystem-remove product from store fail- the store does not exists\n");
            return false;
        }

        public Boolean assignManager(int ownerId, int managerId, int storeId, List<int> permissionToManager)
        {
            if (this.users.ContainsKey(ownerId) && this.users.ContainsKey(managerId) && ownerId != managerId)
            {
                User ownerUser = this.users[ownerId];
                User managerUser = this.users[managerId];
                return ownerUser.assignManager(managerUser, storeId, permissionToManager);
            }
            return false;
        }

        internal bool assignOwner(int storeID, int assignID, int assignedID)
        {
            if (Users.ContainsKey(assignID))
                if (Users[assignID].assignOwner(storeID, Users[assignedID]))
                {
                    LogManager.Instance.WriteToLog("TradingSystem-Assign owner " + assignedID + " -success");
                    return true;
                }
            LogManager.Instance.WriteToLog("TradingSystem-Assign owner fail- the owner does not exists\n");
            return false;
        }

        internal bool editProductInStore(int productID, string productName, string category, string details, int price, int storeID, int userID)
        {
            if (Stores.ContainsKey(storeID))
                if (Stores[storeID].editProductsInStore(productID,productName,category,details,price,userID))
                {
                    LogManager.Instance.WriteToLog("TradingSystem-edit product to store" + storeID + " success");
                    return true;
                }
            LogManager.Instance.WriteToLog("TradingSystem-edit product from store fail- the store does not exists\n");
            return false;
        }
    }
}
