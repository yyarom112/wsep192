using src.Domain.Dataclass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class TradingSystem
    {
        private Dictionary<int, User> users;
        private Dictionary<int, Store> stores;
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
            this.discountPolicyCounter = 0;
            this.encryption = new EncryptionImpl();
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

        public bool[] getVisibility(int userID,String userName)
        {
            /*
              0 - admin
              1 - owner
              2 - manager
              3 - AddDiscountPolicy
              4 - AddPurchasePolicy
              5 - CreateNewProductInStore
              6 - AddProductsInStore
              7 - RemoveProductsInStore
              8 - EditProductInStore
              9 - CommunicationWithCustomers
              10 - PurchasesHistory
             */
            bool flag;
            bool[] result = new bool[11];
            for (int i = 0; i < result.Length; i++)
                result[i] = false;
            if (userName == "admin")
                result[0] = true;
            foreach (Role r in users[userID].Roles.Values)
            {
                if (r.GetType() == typeof(Owner)) { 
                    result[1] = true;
                    break;
                }
                if (r.GetType() == typeof(Manager))
                {
                    result[2] = true;
                    Manager m = (Manager)r;
                    for (int j = 1; j < 9; j++) {
                        flag = m.validatePermission(j);
                        if (flag)
                            result[j + 2] = flag;
                    }
                }
            }
            return result;
        }

        public double basketCheckout(String address, int userID)
        {
            if (!this.users.ContainsKey(userID))
            {
                return -1;
            }
            else
            {
                double output = this.users[userID].basketCheckout(address);
                if (output == -1)
                {
                    LogManager.Instance.WriteToLog("basketCheckout - Could not close basket.\n");
                }
                else
                    LogManager.Instance.WriteToLog("Successfully closed the basket.\n");
                return output;
            }
        }

        public List<String[]> payForBasket(long cardNumber, DateTime date, int userID)
        {
            ShoppingBasket basket = users[userID].Basket;
            Dictionary<int, double> storeToPay = new Dictionary<int, double>(); //<storeId,Sum>
            foreach (ShoppingCart cart in basket.ShoppingCarts.Values)
            {
                cart.Store.updateCart(cart, "-");
                storeToPay.Add(cart.Store.Id, cart.cartCheckout(new UserDetailes(this.Users[userID].Address, this.Users[userID].IsRegistered)));
            }
            foreach (KeyValuePair<int, double> storeSum in storeToPay)
            {
                if (!this.financialSystem.payment(cardNumber, date, storeSum.Value, storeSum.Key))
                {
                    foreach (ShoppingCart cart in basket.ShoppingCarts.Values)
                    {
                        cart.Store.updateCart(cart, "+");
                    }
                    LogManager.Instance.WriteToLog("payForBasket - Purchase failed due to product billing failure.\n");
                    return null;
                }
            }
            if (!this.supplySystem.deliverToCustomer(this.Users[userID].Address, "Some package Details"))
            {
                foreach (KeyValuePair<int, double> storeSum in storeToPay)
                {
                    this.financialSystem.Chargeback(cardNumber, date, storeSum.Value);
                }
                foreach (ShoppingCart cart in basket.ShoppingCarts.Values)
                {
                    cart.Store.updateCart(cart, "+");
                }
                LogManager.Instance.WriteToLog("payForBasket - The purchase failed due to a failure in the delivery system.\n");

                return null;
            }
            List<String[]> output = new List<string[]>();
            foreach (ShoppingCart cart in basket.ShoppingCarts.Values)
            {
                foreach (String[] toInsert in cartToString(cart))
                    output.Add(toInsert);
            }
            LogManager.Instance.WriteToLog("Making the cart purchase succeeded\n");
            users[userID].setOrderStores();
            this.users[userID].Basket = new ShoppingBasket();
            return output;
        }

        public List<String> getOrderStoresByUser(int userID)
        {
            return users[userID].getOrderStores();
        }

        public List<String[]> cartToString(ShoppingCart cart)
        {
            List<String[]> output = new List<string[]>();
            foreach (ProductInCart p in cart.Products.Values)
            {
                String[] p_string = new String[5];
                p_string[0] = p.Product.ProductName;
                p_string[1] = p.ShoppingCart.Store.Name;
                p_string[2] = p.Quantity.ToString();
                p_string[3] = p.Product.Price.ToString();
                p_string[4] = (p.Quantity * p.Product.Price).ToString();
                output.Add(p_string);
            }
            return output;
        }


        private String productsToString(List<ProductInStore> products)
        {
            String res = "";
            int i = 0;
            foreach (ProductInStore p in products)
            {
                res += "name" + i + "=" + p.Product.ProductName + "&"
                     + "store" + i + "=" + p.Store.Name + "&"
                    + "quantity" + i + "=" + p.Quantity + "&"
                    ;
                i++;

            }
            if (res != "")
                res = res.Substring(0, res.Length - 1);
            return res;
        }

        internal bool isMainOwner(int UserID)
        {
            foreach (Role r in Users[UserID].Roles.Values)
            {
                if (r.Store.RolesDictionary[UserID].Equals(r.Store.Roles))
                    return true;
            }
            return false;
        }

        internal bool removeUser(int removingID, int toRemoveID)
        {

            if (!(users.ContainsKey(toRemoveID) && users.ContainsKey(removingID)))
            {
                LogManager.Instance.WriteToLog("TradingSystem - Remove user fail - one of the users does not exists.\n");
                return false;
            }

            if (isMainOwner(toRemoveID))
            {
                LogManager.Instance.WriteToLog("TradingSystem - Remove user fail - the user to remove is the main user.\n");
                return false;
            }

            if (!users[removingID].IsAdmin)
            {
                LogManager.Instance.WriteToLog("TradingSystem - Remove user fail - the removing user is not an admin.\n");
                return false;
            }

            if (users.Remove(toRemoveID))
            {
                LogManager.Instance.WriteToLog("TradingSystem - Remove user id" + toRemoveID + " success.\n");
                return true;
            }
            return false;
        }

        internal bool openStore(string storeName, int userID, int storeCounter)
        {
            if (!stores.ContainsKey(storeCounter))
            {
                Store store = new Store(storeCounter, storeName);
                if (Users.ContainsKey(userID) && Users[userID].IsRegistered)
                {
                    Stores.Add(storeCounter, store);
                    User user = searchUser(userID);
                    store.initOwner(user);
                    LogManager.Instance.WriteToLog("TradingSystem-open store" + storeName + " success\n");
                    return true;
                }
                LogManager.Instance.WriteToLog("TradingSystem - open store fail- the user does not exists or not registerd\n");
            }
            LogManager.Instance.WriteToLog("TradingSystem - open store fail - the store id does not exists\n");
            return false;

        }

        public List<KeyValuePair<string, int>> showCart(int store, int user)
        {
            if (!Users.ContainsKey(user) || !Stores.ContainsKey(store))
                return null;
            return Users[user].showCart(store);

        }

        public bool editProductQuantityInCart(int product, int quantity, int store, int user)
        {
            if (!Users.ContainsKey(user) || !Stores.ContainsKey(store))
                return false;
            return Users[user].editProductQuantityInCart(product, quantity, store);
        }

        public bool removeProductsFromCart(List<int> productsToRemove, int store, int user)
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
                LogManager.Instance.WriteToLog("TradingSystem - Remove manager fail - The store or user is not exist.\n");
                return false;
            }
            if (users[userID].removeManager(userIDToRemove, storeID))
            {
                LogManager.Instance.WriteToLog("TradingSystem - Remove manager id" + userIDToRemove + " from store " + storeID + " success\n");
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

        public bool removeOwner(int userID, int userIDToRemove, int storeID)
        {
            return users[userID].removeOwner(userIDToRemove, storeID);
        }





        public String searchProduct(String details)
        {
            List<ProductInStore> products = new List<ProductInStore>();
            String[] detailsForFilter = details.Split(',');
            if (detailsForFilter.Length != 7)
            {
                LogManager.Instance.WriteToLog("TradingSystem - search Product " + details + " bad input");
                return "";
            }

            int productRate = -1, storeRate = -1, minPrice = -1, maxPrice = -1;

            try
            {
                minPrice = Int32.Parse(detailsForFilter[3]);
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("TradingSystem- searchProduct - Try parse int failed");
                minPrice = -1;
            }
            try
            {
                maxPrice = Int32.Parse(detailsForFilter[4]);
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("TradingSystem- searchProduct - Try parse int failed");
                maxPrice = -1;
            }

            try
            {
                productRate = Int32.Parse(detailsForFilter[5]);
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("TradingSystem- searchProduct - Try parse int failed");
                productRate = -1;
            }
            try
            {
                storeRate = Int32.Parse(detailsForFilter[6]);
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("TradingSystem- searchProduct - Try parse int failed");
                storeRate = -1;
            }
            KeyValuePair<int, int> priceRange = new KeyValuePair<int, int>(minPrice, maxPrice);

            Filter filter = new Filter(detailsForFilter[0],
                detailsForFilter[1], detailsForFilter[2], priceRange,
                productRate, storeRate);
            foreach (Store s in stores.Values)
            {
                s.searchProduct(filter, products);
            }
            return productsToString(products);
        }

        internal bool initUserGuest(string user, int userCounter)
        {
            User guest = new User(userCounter, user, null, false, false);
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

        public bool init(string adminUserName, string adminPassword, int userCounter)
        {
            User admin = new User(userCounter, adminUserName, encryption.encrypt(adminUserName + adminPassword), true, true);
            users.Add(userCounter, admin);
            if (!financialSystem.connect() || !supplySystem.connect() || !encryption.connect())
                return false;

            return true;
        }

        public bool signOut(int id)
        {
            if (!users.ContainsKey(id))
            {
                return false;
            }
            return users[id].signOut();

        }

        public Boolean register(String userName, String password, int userId)
        {
            int currUserId = userId;
            if (this.users.ContainsKey(currUserId))
            {
                if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password)
                    || userName.Equals("") || password.Equals("") || userName.Contains(" "))
                {
                    LogManager.Instance.WriteToLog("TradingSystem - Register - userName or password in wrong format");
                    return false;
                }
                User currUser = this.users[currUserId];
                if (currUser != null)
                {
                    password = encryption.encrypt(userName + password);
                    return currUser.register(userName, password);
                }
                LogManager.Instance.WriteToLog("TradingSystem - Register - user not exist as guest.\n");
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
                        LogManager.Instance.WriteToLog("TradingSystem - signIn - user not register.\n");
                        return false;
                    }
                    password = encryption.encrypt(userName + password);
                    if (currUser.Password == password)
                    {
                        return currUser.signIn(userName, password);
                    }
                }
                LogManager.Instance.WriteToLog("TradingSystem - signIn - user id not exist.\n");
                return false;
            }
            return false;
        }

        public bool addProductsToCart(List<KeyValuePair<int, int>> products, int storeId, int userId)
        {
            if (!this.Users.ContainsKey(userId) || !this.Stores.ContainsKey(storeId) || products == null)
            {
                LogManager.Instance.WriteToLog("Add to cart fail- one of the parameter Invalid. /n");
                return false;

            }
            LinkedList<KeyValuePair<Product, int>> toInsert = createProductsList(products, storeId);
            if (toInsert == null)
            {
                return false;
            }
            ShoppingCart newCartCheck = this.users[userId].addProductsToCart(toInsert, storeId);
            if (newCartCheck != null)
                newCartCheck.Store = this.stores[storeId];
            LogManager.Instance.WriteToLog("Add to cart success. /n");
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
                    LogManager.Instance.WriteToLog("Add to cart fail-Product " + productId.Key + " does not exist. \n");
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

        internal bool createNewProductInStore(string productName, string category, string details, int price, int storeID, int userID)
        {
            if (Stores.ContainsKey(storeID))
                if (users[userID].createNewProductInStore(productName, category, details, price, ProductCounter++, storeID))
                {
                    LogManager.Instance.WriteToLog("TradingSystem-create new product in store" + storeID + " -success\n");
                    return true;
                }
            LogManager.Instance.WriteToLog("TradingSystem-create new product in store fail- the store does not exists\n");
            return false;
        }

        internal bool addProductsInStore(List<KeyValuePair<int, int>> productsInStore, int storeID, int userID)
        {
            if (Stores.ContainsKey(storeID))
                if (users[userID].addProductsInStore(productsInStore, storeID))
                {
                    LogManager.Instance.WriteToLog("TradingSystem-add product to store" + storeID + " -success");
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
                if (users[userID].removeProductsInStore(productsInStore, storeID))
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
                if (users[userID].editProductsInStore(productID, productName, category, details, price, storeID))
                {
                    LogManager.Instance.WriteToLog("TradingSystem-edit product to store" + storeID + " success");
                    return true;
                }
            LogManager.Instance.WriteToLog("TradingSystem-edit product from store fail- the store does not exists\n");
            return false;
        }

        public int addSimplePurchasePolicy(int type, int first, int second, int third, int fourth, int act, string adress, bool isregister, int storeID, int userID)
        {
            PurchesPolicyData purchesData;
            switch (type)
            {
                case 0:
                    if (first < 0 || second < 0 || third < 0 || act < 0)
                        return -1;
                    purchesData = new PurchesPolicyData(type, this.PurchasePolicyCounter++, first, -1, second, third, -1, -1, EnumActivaties.ConvertIntToLogicalConnections(act), null, false);
                    break;
                case 1:
                    if (first < 0 || second < 0 || act < 0)
                        return -1;
                    purchesData = new PurchesPolicyData(type, this.PurchasePolicyCounter++, first, -1, second, -1, -1, -1, EnumActivaties.ConvertIntToLogicalConnections(act), null, false);
                    break;
                case 2:
                    if (first < 0 || second < 0 || third < 0 || fourth < 0 || act < 0)
                        return -1;
                    purchesData = new PurchesPolicyData(type, this.PurchasePolicyCounter++, -1, -1, first, second, third, fourth, EnumActivaties.ConvertIntToLogicalConnections(act), null, false);
                    break;
                case 3:
                    if (((adress == null || adress.Equals("")) && isregister == false) || act < 0)
                        return -1;
                    purchesData = new PurchesPolicyData(type, this.PurchasePolicyCounter++, -1, -1, -1, -1, -1, -1, EnumActivaties.ConvertIntToLogicalConnections(act), adress, isregister);
                    break;
                default:
                    LogManager.Instance.WriteToLog("Trading System- addSimplePurchasePolicy- type " + type + " is not recognized\n");
                    return -1;
            }
            if (this.Users.ContainsKey(userID))
            {
                PurchasePolicy p = Users[userID].addSimplePurchasePolicy(purchesData, storeID);
                if (p != null)
                    return p.getId();
            }
            LogManager.Instance.WriteToLog("Trading System- addSimplePurchasePolicy- User does not exist\n");
            return -1;
        }


        public int addComplexPurchasePolicy(String purchesData, int storeID, int userID)
        {
            if (this.Users.ContainsKey(userID))
            {
                PurchasePolicy p = Users[userID].addComplexPurchasePolicy(this.PurchasePolicyCounter++, purchesData, storeID);
                if (p != null)
                    return p.getId();
            }
            LogManager.Instance.WriteToLog("Trading System- addComplexPurchasePolicy- User does not exist\n");
            return -1;
        }

        public int addRevealedDiscountPolicy(List<KeyValuePair<String, int>> products, double discountPrecentage, int userID, int storeID, int expiredDiscountDate, int logic)
        {

            if (this.Users.ContainsKey(userID))
            {
                return Users[userID].addRevealedDiscountPolicy(products, discountPrecentage, storeID, expiredDiscountDate, discountPolicyCounter++, EnumActivaties.ConvertIntToDuplicatePolicy(logic));
            }
            LogManager.Instance.WriteToLog("Trading System- addRevealedDiscountPolicy - User does not exist\n");
            return -1;
        }

        public int addConditionalDiscuntPolicy(List<String> products, String condition, double discountPrecentage, int expiredDiscountDate, int duplicate, int logic, int userId, int storeId)
        {
            if (this.Users.ContainsKey(userId))
            {
                return Users[userId].addConditionalDiscuntPolicy(products, condition, discountPrecentage, expiredDiscountDate, EnumActivaties.ConvertIntToDuplicatePolicy(duplicate), EnumActivaties.ConvertIntToLogicalConnections(logic), discountPolicyCounter++, storeId);
            }
            LogManager.Instance.WriteToLog("Trading System- addConditionalDiscuntPolicy - User does not exist\n");
            return -1;
        }

        public int removeDiscountPolicy(int discountId, int storeId, int userId)
        {
            if (this.Users.ContainsKey(userId))
            {
                return Users[userId].removeDiscountPolicy(discountId, storeId);
            }
            LogManager.Instance.WriteToLog("Trading System- removeDiscountPolicy - User does not exist\n");
            return -1;
        }

        public int removePurchasePolicy(int purchaseId, int storeId, int userId)
        {
            if (this.Users.ContainsKey(userId))
            {
                return Users[userId].removePurchasePolicy(purchaseId, storeId);
            }
            LogManager.Instance.WriteToLog("Trading System- removePurchasePolicy - User does not exist\n");
            return -1;
        }

        public bool isLoggedIn(int userId)
        {
            if (this.Users.ContainsKey(userId))
                return Users[userId].isLoggedIn();
            return false;
        }
        public void addMessageToUser(int userID,String message)
        {
            users[userID].getMessages().Add(message);
        }
        public List<String> getMessagesByUser(int userID)
        {
            return users[userID].getMessages();
        }
        public void deleteMessagesByUser(int userID)
        {
            users[userID].deleteMessages();
        }

    }
}

