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
            this.storeCounter = 0;
            this.userCounter = 0;
            this.purchasePolicyCounter = 0;
            this.discountPolicyCounter =  0;
            this.encryption = new EncryptionImpl();
        }
        public bool removeOwner(int userID,int userIDToRemove,int storeID)
        {
            return users[userID].removeOwner(userIDToRemove, storeID);
        }
        public List<ProductInStore> searchProduct(String details)
        {
            List<ProductInStore> products  = new List<ProductInStore>();
            String[] detailsForFilter = details.Split(' ');
            if (detailsForFilter.Length != 7)
                return products;
            KeyValuePair<int, int> priceRange = new KeyValuePair<int, int>(Int32.Parse(detailsForFilter[3]),
                Int32.Parse(detailsForFilter[4]));
            Filter filter = new Filter(detailsForFilter[0],
                detailsForFilter[1], detailsForFilter[2], priceRange,
                Int32.Parse(detailsForFilter[5]), Int32.Parse(detailsForFilter[6]));
            foreach (Store s in stores.Values)
            {
                s.searchProduct(filter,products);
            }
            return products;
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



        public bool init(string adminUserName, string adminPassword)
        {
            User admin = new User(userCounter, adminUserName, adminPassword, true, true);
            users.Add(userCounter, admin);
            userCounter++;
            if (!financialSystem.connect() || !supplySystem.connect() || !encryption.connect())
                return false;

            return true;
        }

        public bool signOut(int id) {
            if (!users.ContainsKey(id))
                return false;
            return users[id].signOut();

        }
        public Boolean register(String userName, String password, String userId)
        {
            int currUserId = Convert.ToInt32(userId);
            if (this.users.ContainsKey(currUserId))
            {
                if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password)
                    || userName.Equals("") || password.Equals("") || userName.Contains(" "))
                    return false;
                User currUser = this.users[currUserId];
                if (currUser != null && userName == currUser.UserName && password == currUser.Password)
                {
                    password = encryption.encrypt(userName + password);
                    return currUser.register(userName, password);
                }
                return false;
            }
            return false;
        }

        public Boolean signIn(String userName, String password, String userId)
        {
            int currUserId = Convert.ToInt32(userId);
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

        public bool addProductsToCart(LinkedList<KeyValuePair<int, int>> products,int storeId,int userId)
        {
            if (!this.Users.ContainsKey(userId) || !this.Stores.ContainsKey(storeId) || products==null)
                return false;
            LinkedList<KeyValuePair<Product, int>> toInsert = createProductsList(products, storeId);
            if (toInsert == null)
                return false;
            ShoppingCart newCartCheck= this.users[userId].addProductsToCart(toInsert, storeId);
            if (newCartCheck != null)
                newCartCheck.Store = this.stores[storeId];
            return true;
        }

        public LinkedList<KeyValuePair<Product, int>> createProductsList(LinkedList<KeyValuePair<int, int>> products, int storeId)
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


    }
}
