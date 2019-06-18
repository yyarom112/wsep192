using src.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.DataLayer
{
    class DBtransactions
    {
        private DBmanager db;
        private static DBtransactions instance = null;

        internal DBmanager Db { get => db; set => db = value; }

        private DBtransactions(bool isTest)
        {
            this.Db = new DBmanager(isTest);
            Db.IsTest = isTest;
        }


        public static DBtransactions getInstance(bool isTest)
        {
            if (instance == null)
            {
                instance = new DBtransactions(isTest);
            }
            return instance;
        }

        public void isTest(bool isTest)
        {
            Db.IsTest = isTest;
        }

        public bool initSystem(TradingSystem sys)
        {
            if (db.IsTest)
                return true;
            var session = Db.Client.StartSession();
            session.StartTransaction();
            try
            {
                //restore store
                List<int> storesIDList = db.getAllStoreID();
                int maxStoreID = -1, maxProductID = -1;
                foreach (int storeID in storesIDList)
                {
                    if (!sys.Stores.ContainsKey(storeID))
                    {
                        //create the store from db
                        Store tmp = db.getStore(storeID);
                        List<ProductInStore> products = db.getAllProductInStore(tmp);
                        //restore the product in store
                        foreach (ProductInStore product in products)
                        {
                            tmp.Products.Add(product.Product.Id, product);
                            //find product counter
                            if (maxProductID < product.Product.Id)
                                maxProductID = product.Product.Id;
                        }
                        //find storeCounter For sys 
                        if (maxStoreID < storeID)
                            maxStoreID = storeID;
                        sys.Stores.Add(storeID, tmp);
                    }
                }
                sys.StoreCounter = maxStoreID + 1;
                sys.ProductCounter = maxProductID;

                //find user counter
                int maxUserID = -1;
                List<User> userList = db.getAllUser();
                foreach (User user in userList)
                {
                    if (maxUserID < user.Id)
                        maxUserID = user.Id;
                }
                sys.UserCounter = maxUserID;
                session.CommitTransaction();
            }
            catch (Exception e)
            {
                session.AbortTransaction();
                ErrorManager.Instance.WriteToLog("DBtransactions-initSystem- Restore system failed - " + e + " .");
                return false;
            }
            return true;
        }

        public bool signIn(int userID, TradingSystem sys)
        {
            if (db.IsTest)
                return true;
            var session = Db.Client.StartSession();
            session.StartTransaction();
            try
            {
                //restore user
                User userToRestore = db.getUser(userID);

                //restore product in cart
                //int[]={0-UserId,1-StoreID,2-ProductID,3-quantity}
                List<int[]> productList = db.getALLProductInCartPerUsers(userToRestore.Id);
                foreach (int[] product in productList)
                {
                    Product p = sys.getProduct(product[2], product[1]);
                    if (p == null)
                        continue;
                    LinkedList<KeyValuePair<Product, int>> productToInsert = new LinkedList<KeyValuePair<Product, int>>();
                    productToInsert.AddFirst(new KeyValuePair<Product, int>(p, product[3]));
                    userToRestore.Basket.addProductsToCart(productToInsert, product[1], product[0]);
                }

                //restore store in all cart like tha addProductTocart path
                foreach (ShoppingCart cart in userToRestore.Basket.ShoppingCarts.Values)
                {
                    cart.Store = sys.Stores[cart.StoreId];
                }
                //restore owners
                List<int> storeIDList = db.getAllOwnerDBbyUserID(userID);
                if (storeIDList != null)
                {
                    foreach (int storeId in storeIDList)
                    {
                        Owner toinsert = new Owner(sys.Stores[storeId], userToRestore);
                        sys.Stores[storeId].RolesDictionary.Add(userToRestore.Id, sys.Stores[storeId].Roles.AddChild(toinsert));
                        userToRestore.Roles.Add(storeId, toinsert);
                    }
                }

                //restore manger
                List<KeyValuePair<int, string>> managerList = db.getManegerByUserID(userID);
                if (managerList != null)
                {
                    foreach (KeyValuePair<int, string> managerDetails in managerList)
                    {
                        List<String> stringpre = managerDetails.Value.Split(',').ToList();
                        List<int> premissions = new List<int>();
                        foreach (string premission in stringpre)
                        {
                            premissions.Add(Int32.Parse(premission));
                        }
                        Manager manager = new Manager(sys.Stores[managerDetails.Key], userToRestore, premissions);
                        sys.Stores[managerDetails.Key].RolesDictionary.Add(userToRestore.Id, sys.Stores[managerDetails.Key].Roles.AddChild(manager));
                        userToRestore.Roles.Add(managerDetails.Key, manager);
                    }
                }

                sys.Users.Add(userToRestore.Id, userToRestore);
                session.CommitTransaction();
            }
            catch (Exception e)
            {
                session.AbortTransaction();
                ErrorManager.Instance.WriteToLog("DBtransactions-initSystem- Restore system failed - " + e + " .");
                return false;
            }
            return true;
        }

        public bool registerNewUserDB(User user)
        {
            if (db.IsTest)
                return true;
            if (!Db.IsTest)
            {
                var session = Db.Client.StartSession();
                session.StartTransaction();
                try
                {
                    //store the user
                    if (!Db.addNewUser(user))
                    {
                        session.AbortTransaction();
                        return false;
                    }
                    //store the basket
                    foreach (ShoppingCart cart in user.Basket.ShoppingCarts.Values)
                    {
                        foreach (ProductInCart product in cart.Products.Values)
                        {
                            if (!Db.addProductInCart(product, user.Id))
                            {
                                session.AbortTransaction();
                                return false;
                            }
                            db.addNewProduct(product.Product);
                        }
                    }
                    session.CommitTransaction();
                }
                catch (Exception e)
                {
                    session.AbortTransaction();
                    ErrorManager.Instance.WriteToLog("DBtransaction-registerNewUser- Add new user failed - " + e + " .");
                    return false;
                }
                return true;
            }
            return true;

        }

        public bool assignManagerDB(Manager manager)
        {
            if (db.IsTest)
                return true;
            if (!Db.IsTest)
            {
                var session = Db.Client.StartSession();
                session.StartTransaction();
                try
                {
                    //store the manager
                    if (!Db.addNewManager(manager))
                    {
                        session.AbortTransaction();
                        return false;
                    }
                    session.CommitTransaction();
                    return true;
                }
                catch (Exception e)
                {
                    session.AbortTransaction();
                    ErrorManager.Instance.WriteToLog("DBtransaction-assignManager- Add new manager failed - " + e + " .");
                    return false;
                }
                
            }
            return true;

        }

        public bool removeManagerDB(int manager_id)
        {
            if (db.IsTest)
                return true;
            if (!Db.IsTest)
            {
                var session = Db.Client.StartSession();
                session.StartTransaction();
                try
                {
                    //removes the manager
                    if (!Db.removeManager(manager_id))
                    {
                        session.AbortTransaction();
                        return false;
                    }
                    session.CommitTransaction();
                }
                catch (Exception e)
                {
                    session.AbortTransaction();
                    ErrorManager.Instance.WriteToLog("DBtransaction-removeManager- remove manager failed - " + e + " .");
                    return false;
                }
                return true;
            }
            return true;

        }

        public bool removeUserDB(int user_id)
        {
            if (db.IsTest)
                return true;
            if (!Db.IsTest)
            {
                var session = Db.Client.StartSession();
                session.StartTransaction();
                try
                {
                    //removes the manager
                    if (!Db.removeUser(user_id))
                    {
                        session.AbortTransaction();
                        return false;
                    }
                    session.CommitTransaction();
                }
                catch (Exception e)
                {
                    session.AbortTransaction();
                    ErrorManager.Instance.WriteToLog("DBtransaction-removeUser- remove user failed - " + e + " .");
                    return false;
                }
                return true;
            }
            return true;

        }

        public bool AddProductToCart(Dictionary<int, ProductInCart> products, int userId)
        {
            if (db.IsTest)
                return true;
            var session = Db.Client.StartSession();
            session.StartTransaction();
            try
            {
                foreach (ProductInCart product in products.Values)
                {
                    if (!db.addProductInCart(product, userId))
                    {
                        session.AbortTransaction();
                        LogManager.Instance.WriteToLog("DBtransaction-AddProductToCart- Add new product to cart");
                        return false;
                    }
                }
                session.CommitTransaction();
            }
            catch (Exception e)
            {
                session.AbortTransaction();
                ErrorManager.Instance.WriteToLog("DBtransaction-AddProductToCart- Add new product to cart - " + e + " .");
                return false;
            }
            return true;
        }

        public bool removeProductsFromCart(List<int> products, int storeId, int userId)
        {
            if (db.IsTest)
                return true;
            var session = Db.Client.StartSession();
            session.StartTransaction();
            try
            {
                foreach (int productId in products)
                {
                    if (!db.removeProductInCartBy(userId, storeId, productId))
                    {
                        session.AbortTransaction();
                        LogManager.Instance.WriteToLog("DBtransaction-removeProductsFromCart- Add new product to cart");
                        return false;
                    }
                }
                session.CommitTransaction();
            }
            catch (Exception e)
            {
                session.AbortTransaction();
                ErrorManager.Instance.WriteToLog("DBtransaction-removeProductsFromCart- Add new product to cart - " + e + " .");
                return false;
            }
            return true;
        }

        public bool EditProductQuantityInCart(int productId, int storeId, int userId, int quntity)
        {

            if (db.IsTest)
                return true;
            var session = Db.Client.StartSession();
            session.StartTransaction();
            try
            {

                if (!db.updateProductInCart(userId, storeId, productId, quntity))
                {
                    session.AbortTransaction();
                    LogManager.Instance.WriteToLog("DBtransaction-EditProductQuantityInCart- Add new product to cart");
                    return false;
                }
                session.CommitTransaction();
            }
            catch (Exception e)
            {
                session.AbortTransaction();
                ErrorManager.Instance.WriteToLog("DBtransaction-EditProductQuantityInCart- Add new product to cart - " + e + " .");
                return false;
            }
            return true;
        }



        public bool assignOwner(Owner owner)
        {
            if (db.IsTest)
                return true;
            var session = Db.Client.StartSession();
            session.StartTransaction();
            try{

                if (!db.addNewOwner(owner, -1)){
                    session.AbortTransaction();
                    return false;
                }
                session.CommitTransaction();
            }
            catch (Exception e) {
                session.AbortTransaction();
                ErrorManager.Instance.WriteToLog("DBtransaction - assignOwner - failed - " + e + " .");
                return false;
            }
            return true;
        }




        public bool OpenStoreDB(Store store, Owner owner)
        {
            if (db.IsTest)
                return true;
            var session = Db.Client.StartSession();
            session.StartTransaction();
            try
            {
                //store the store
                if (!Db.addNewStore(store))
                {
                    session.AbortTransaction();
                    return false;
                }
                //store the product
                foreach (ProductInStore product in store.Products.Values)
                {
                    if (!Db.addNewProductInStore(product) || !Db.addNewProduct(product.Product))
                    {
                        session.AbortTransaction();
                        return false;
                    }
                }
                //store the super owner
                db.addNewOwner(owner, -1);
                session.CommitTransaction();
            }
            catch (Exception e)
            {
                session.AbortTransaction();
                ErrorManager.Instance.WriteToLog("DBtransaction-OpenStoreDB- Open new store failed - " + e + " .");
                return false;
            }
            return true;
        }

        public bool createProductInstore(ProductInStore product)
        {
            if (db.IsTest)
                return true;
            var session = Db.Client.StartSession();
            session.StartTransaction();
            try
            {
                if (!db.addNewProductInStore(product))
                {
                    session.AbortTransaction();
                    LogManager.Instance.WriteToLog("DBtransaction-AddProductInStore- Add new product in store");
                    return false;
                }
            }
            catch (Exception e)
            {
                session.AbortTransaction();
                ErrorManager.Instance.WriteToLog("DBtransaction-AddProductInStore- Add new product in store - " + e + " .");
                return false;
            }
            return true;
        }

        public bool removeProductInStore(int productId, int storeId)
        {
            if (db.IsTest)
                return true;
            var session = Db.Client.StartSession();
            session.StartTransaction();
            try
            {
                if (!db.removeProductInStore(storeId, productId))
                {
                    session.AbortTransaction();
                    LogManager.Instance.WriteToLog("DBtransaction-removeProductInStore- Add new product in store");
                    return false;
                }
            }
            catch (Exception e)
            {
                session.AbortTransaction();
                ErrorManager.Instance.WriteToLog("DBtransaction-removeProductInStore- Add new product in store - " + e + " .");
                return false;
            }
            return true;
        }

        public bool editProductInStore(int productId, int storeId, int quntity)
        {
            if (db.IsTest)
                return true;
            var session = Db.Client.StartSession();
            session.StartTransaction();
            try
            {

                if (!db.updateProductInStore(storeId, productId, quntity))
                {
                    session.AbortTransaction();
                    LogManager.Instance.WriteToLog("DBtransaction-editProductInStore- edit product in store");
                    return false;
                }
                session.CommitTransaction();
                return true;
            }
            catch (Exception e)
            {
                session.AbortTransaction();
                ErrorManager.Instance.WriteToLog("DBtransaction-editProductInStore- edit product in store - " + e + " .");
                return false;
            }
        }
    }
}
