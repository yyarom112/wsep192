using MongoDB.Bson;
using MongoDB.Driver;
using src.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.DataLayer
{
    class DBmanager
    {
        private bool isTest = true;
        private const string dbAdress = "mongodb+srv://wesp192:wesp192@cluster0-r882l.gcp.mongodb.net/test";
        private const string dbName = "wesp192";
        private IMongoDatabase db;
        private MongoClient client;
        private IMongoCollection<BsonDocument> usersTable;
        private IMongoCollection<BsonDocument> storesTable;
        private IMongoCollection<BsonDocument> ownersTable;
        private IMongoCollection<BsonDocument> managersTable;
        private IMongoCollection<BsonDocument> productsTable;
        private IMongoCollection<BsonDocument> productInCartTable;
        private IMongoCollection<BsonDocument> ProductInStoreTable;



        public bool IsTest { get => isTest; set => isTest = value; }
        public MongoClient Client { get => client; set => client = value; }

        public DBmanager(bool isTest)
        {
            if (!isTest)
            {
                IsTest = false;
                connect();
                usersTable = db.GetCollection<BsonDocument>("Users");
                storesTable = db.GetCollection<BsonDocument>("Stores");
                ownersTable = db.GetCollection<BsonDocument>("Owners");
                managersTable = db.GetCollection<BsonDocument>("Managers");
                productsTable = db.GetCollection<BsonDocument>("Products");
                productInCartTable = db.GetCollection<BsonDocument>("ProductsInCart");
                ProductInStoreTable = db.GetCollection<BsonDocument>("ProductInStore");
            }
            this.isTest = isTest;

        }

        public void connect()
        {
            if (IsTest)
                return;
            try
            {
                Client = new MongoClient(dbAdress);
                db = Client.GetDatabase(dbName);
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("DBmanager- connect- Connect to db failed - " + e + " .");
                return;
            }
        }

        public void stupidtransaction(bool iswork)
        {
            try
            {
                var session = client.StartSession();
                productsTable = session.Client.GetDatabase(dbName).GetCollection<BsonDocument>("Products");
                session.StartTransaction();
                var document = new BsonDocument
            {
                { "_product id", 123456},
                { "_name", "nana banana"},
                { "category","a"},
                { "details", "a"},
                { "price", 123456789},
                { "rate",1},
            };

                productsTable.InsertOneAsync(document);
                if(iswork)
                    session.CommitTransaction();
                else
                {
                    session.AbortTransaction();
                }
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("DBmanager-RegisterNewUser- Add new user failed - " + e + " .");
            }


        }

        public bool Isconnected()
        {
            return db != null;
        }

        //User table functions

        public bool addNewUser(User user)
        {
            if (IsTest)
                return true;
            var document = new BsonDocument
            {
                { "_id", user.Id },
                { "username", user.UserName },
                { "password", user.Password },
                { "isAdmin", user.IsAdmin },
                { "state" , user.State},
            };
            try
            {
                usersTable.InsertOneAsync(document);
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("DBmanager-RegisterNewUser- Add new user failed - " + e + " .");
                return false;
            }
            return true;
        }

        public bool removeUser(int id)
        {
            if (IsTest)
                return true;
            try
            {
                usersTable.DeleteOneAsync(Builders<BsonDocument>.Filter.Eq("_id", id));
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("DBmanager-removeUser- Remove user failed - " + e + " .");
                return false;
            }
            return true;
        }

        public bool updateUser(User user)
        {
            if (IsTest)
                return true;
            var filter = Builders<BsonDocument>.Filter.Eq("_id", user.Id);
            var updateName = Builders<BsonDocument>.Update.Set("username", user.UserName);
            var updatePassword = Builders<BsonDocument>.Update.Set("password", user.Password);
            var updateIsAdmin = Builders<BsonDocument>.Update.Set("isAdmin", user.IsAdmin);
            var updateState = Builders<BsonDocument>.Update.Set("state", user.State);

            try
            {
                usersTable.UpdateOneAsync(filter, updateName);
                usersTable.UpdateOneAsync(filter, updatePassword);
                usersTable.UpdateOneAsync(filter, updateIsAdmin);
                usersTable.UpdateOneAsync(filter, updateState);

            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("DBmanager-updateUser- Remove user failed - " + e + " .");
                return false;
            }
            return true;
        }

        public User getUser(int UserID)
        {
            if (IsTest)
                return null;
            var filter = Builders<BsonDocument>.Filter.Eq("_id", UserID);

            var document = usersTable.Find(filter).ToList();
            if (document == null || document.Count == 0)
            {
                return null;
            }
            User output = new User(UserID, document[0]["username"].AsString,
                               document[0]["password"].AsString, document[0]["isAdmin"].AsBoolean,
                               true);
            output.State = EnumActivaties.intToState(document[0]["state"].AsInt32);

            //must add recover of basket

            return output;


        }

        public List<User> getAllUser()
        {
            if (IsTest)
                return null;
            List<User> output = new List<User>();
            var document = usersTable.Find(new BsonDocument()).ToList();
            if (document == null || document.Count == 0)
            {
                return null;
            }
            foreach (var doc in document)
            {
                output.Add(getUser(doc["_id"].AsInt32));
            }
            return output;
        }

        //Stores table functions
        public bool addNewStore(Store store)
        {
            if (IsTest)
                return true;
            var document = new BsonDocument
            {
                { "_id", store.Id },
                { "store name", store.Name },
                { "rate", store.StoreRate },
            };
            try
            {
                storesTable.InsertOneAsync(document);
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("DBmanager - Create Store - Create new store failed - " + e + " .");
                return false;
            }
            return true;
        }

        public bool removeStore(int id)
        {
            if (IsTest)
                return true;
            try
            {
                storesTable.DeleteOneAsync(Builders<BsonDocument>.Filter.Eq("_id", id));
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("DBmanager-removeStore- Remove store failed - " + e + " .");
                return false;
            }
            return true;
        }

        public bool updateStore(Store store)
        {
            if (IsTest)
                return true;
            var filter = Builders<BsonDocument>.Filter.Eq("_id", store.Id);
            var update = Builders<BsonDocument>.Update.Set("store name", store.Name);
            update.AddToSet("rate", store.StoreRate);
            try
            {
                storesTable.UpdateOneAsync(filter, update);
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("DBmanager-updateStore- Update Store failed - " + e + " .");
                return false;
            }
            return true;
        }

        public Store getStore(int StoreID)
        {
            if (IsTest)
                return null;
            var filter = Builders<BsonDocument>.Filter.Eq("_id", StoreID);

            var document = storesTable.Find(filter).ToList();
            if (document == null || document.Count == 0)
            {
                return null;
            }
            Store output = new Store(StoreID, document[0]["store name"].AsString);
            output.StoreRate = document[0]["rate"].AsInt32;
            return output;
        }

        public List<int> getAllStoreID()
        {
            if (IsTest)
                return null;
            List<int> output = new List<int>();
            var document = storesTable.Find(new BsonDocument()).ToList();
            if (document == null || document.Count == 0)
            {
                return null;
            }
            foreach (var doc in document)
            {
                output.Add(doc["_id"].AsInt32);
            }
            return output;
        }

        //Owners table functions
        public bool addNewOwner(Owner owner, int father)
        {
            if (IsTest)
                return true;
            var document = new BsonDocument
            {
                 { "_store id", owner.Store.Id},
                { "_user id", owner.User.Id},
            };
            try
            {
                ownersTable.InsertOneAsync(document);
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("DBmanager - Create Store - Create new store failed - " + e + " .");
                return false;
            }
            return true;
        }

        public bool removeOwner(int user_id)
        {
            if (IsTest)
                return true;
            try
            {
                ownersTable.DeleteOneAsync(Builders<BsonDocument>.Filter.Eq("_user id", user_id));
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("DBmanager-removeOwner- Remove owner failed - " + e + " .");
                return false;
            }
            return true;
        }

        public bool isOwnerDB(int storeID, int userID)
        {
            if (IsTest)
                return true;
            var filter = Builders<BsonDocument>.Filter.Eq("_store id", storeID) & Builders<BsonDocument>.Filter.Eq("_user id", userID);
            var document = ownersTable.Find(filter).ToList();
            if (document == null || document.Count == 0)
                return false;
            return true;
        }

        public List<int> getAllOwnerDBbyUserID(int userID)
        {
            if (IsTest)
                return new List<int>();
            List<int> output = new List<int>();
            var filter = Builders<BsonDocument>.Filter.Eq("_user id", userID);
            var document = ownersTable.Find(filter).ToList();
            if (document == null )
                return null;
            foreach(var doc in document)
            {
                output.Add(doc["_store id"].AsInt32);
            }
            return output;
        }
        //Managers table functions
        public bool addNewManager(Manager manager, int father)
        {
            if (IsTest)
                return true;
            var document = new BsonDocument
            {
                { "_store id", manager.Store.Id},
                { "_user id", manager.User.Id},
                { "permission", string.Join(",",manager.Permissions)},

            };
            try
            {
                managersTable.InsertOneAsync(document);
            }
            catch (Exception e)
            {

                ErrorManager.Instance.WriteToLog("DBmanager - AddNewManager - Add new manager failed - " + e + ".");
                return false;
            }
            return true;
        }

        public bool removeManager(int user_id)
        {
            if (IsTest)
                return true;
            try
            {
                managersTable.DeleteOneAsync(Builders<BsonDocument>.Filter.Eq("_user id", user_id));
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("DBmanager-removeManager- Remove manager failed - " + e + " .");
                return false;
            }
            return true;
        }

        public string getPermission(int storeID, int userID)
        {
            if (IsTest)
                return "";
            var filter = Builders<BsonDocument>.Filter.Eq("_store id", storeID) & Builders<BsonDocument>.Filter.Eq("_user id", userID);

            var document = managersTable.Find(filter).ToList();
            if (document == null || document.Count == 0)
            {
                return null;
            }
            string output = document[0]["permission"].AsString;
            return output;
        }

        //return value is <storeId,Premmision-String>
        public List<KeyValuePair<int,string>> getManegerByUserID(int userID)
        {
            if (IsTest)
                return new List<KeyValuePair<int, string>>();
            var filter = Builders<BsonDocument>.Filter.Eq("_user id", userID);
            List<KeyValuePair<int, string>> output = new List<KeyValuePair<int, string>>();
            var document = managersTable.Find(filter).ToList();
            if (document == null || document.Count == 0)
            {
                return null;
            }
            foreach (var doc in document)
            {
                output.Add(new KeyValuePair<int, string>(doc["_store id"].AsInt32, doc["permission"].AsString));
            }
            return output;
        }

        //Products table functions
        public bool addNewProduct(Product product)
        {
            if (IsTest)
                return true;
            var document = new BsonDocument
            {
                { "_product id", product.Id},
                { "_name", product.ProductName},
                { "category",product.Category},
                { "details", product.Details},
                { "price", product.Price},
                { "rate",product.ProductRate},
            };
            try
            {
                productsTable.InsertOneAsync(document);
            }
            catch (Exception e)
            {

                ErrorManager.Instance.WriteToLog("DBmanager - AddNewProduct - Add new product failed - " + e + ".");
                return false;
            }
            return true;
        }

        public bool removeProduct(int product_id)
        {
            if (IsTest)
                return true;
            try
            {
                productsTable.DeleteOneAsync(Builders<BsonDocument>.Filter.Eq("_product id", product_id));
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("DBmanager-removeManager- Remove manager failed - " + e + " .");
                return false;
            }
            return true;
        }

        public Product getProduct(int ProductID)
        {
            if (IsTest)
                return null;
            var filter = Builders<BsonDocument>.Filter.Eq("_product id", ProductID);

            var document = productsTable.Find(filter).ToList();
            if (document == null || document.Count == 0)
            {
                return null;
            }
            Product output = new Product(document[0]["_product id"].AsInt32, document[0]["_name"].AsString, document[0]["category"].AsString,
                                        document[0]["details"].AsString, document[0]["price"].AsDouble);
            output.ProductRate = document[0]["rate"].AsInt32;
            return output;
        }

        //Product in cart table function

        public bool addProductInCart(ProductInCart product, int userID)
        {
            if (IsTest)
                return true;
            var document = new BsonDocument
            {
                { "_id",  userID+"_"+product.ShoppingCart.StoreId+"_"+product.Product.Id } ,
                { "_userid",  userID},
                { "_storeid",  product.ShoppingCart.StoreId},
                { "_productid",  product.Product.Id},
                { "quantity", product.Quantity },
            };
            try
            {
                productInCartTable.InsertOneAsync(document);
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("DBmanager-addProductInCart- Add new ProductInCart failed - " + e + " .");
                return false;
            }
            return true;
        }

        public bool removeProductInCartBy(int userID, int storeID, int productID)
        {
            if (IsTest)
                return true;
            try
            {
                productInCartTable.DeleteOneAsync(Builders<BsonDocument>.Filter.Eq("_storeid", storeID) & Builders<BsonDocument>.Filter.Eq("_userid", userID) & Builders<BsonDocument>.Filter.Eq("_productid", productID));
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("DBmanager-removeProductInCartBy- Remove ProductInCart failed - " + e + " .");
                return false;
            }
            return true;
        }


        public bool removeAllProductInCartByStoreId(int storeId)
        {
            if (IsTest)
                return true;
            try
            {
                productInCartTable.DeleteManyAsync(Builders<BsonDocument>.Filter.Eq("_storeid", storeId));
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("DBmanager-removeAllProductInCartByStoreId- Remove ProductInCart failed - " + e + " .");
                return false;
            }
            return true;
        }

        public bool removeAllProductInCartByUserId(int userID)
        {
            if (IsTest)
                return true;
            try
            {
                productInCartTable.DeleteManyAsync(Builders<BsonDocument>.Filter.Eq("_userid", userID));
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("DBmanager-removeAllProductInCartByUserId- Remove ProductInCart failed - " + e + " .");
                return false;
            }
            return true;
        }

        public bool updateProductInCart(int userID, int storeID, int productID, int quantity)
        {
            if (IsTest)
                return true;

            var filter = Builders<BsonDocument>.Filter.Eq("_storeid", storeID) & Builders<BsonDocument>.Filter.Eq("_userid", userID) & Builders<BsonDocument>.Filter.Eq("_productid", productID);
            var update = Builders<BsonDocument>.Update.Set("quantity", quantity);

            try
            {
                productInCartTable.UpdateOneAsync(filter, update);

            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("DBmanager-updateProductInCart- Update productInCart failed - " + e + " .");
                return false;
            }
            return true;
        }

        public int getProductInCartquntity(int userID, int storeID, int productID)
        {
            if (IsTest)
                return 0;
            var filter = Builders<BsonDocument>.Filter.Eq("_storeid", storeID) & Builders<BsonDocument>.Filter.Eq("_userid", userID) & Builders<BsonDocument>.Filter.Eq("_productid", productID);

            var document = productInCartTable.Find(filter).ToList();
            if (document == null || document.Count == 0)
            {
                return -1;
            }
            int output = document[0]["quantity"].AsInt32;
            return output;
        }

        public List<int[]> getALLProductInCartPerUsers(int userID)
        {
            if (IsTest)
                return null;
            List<int[]> output = new List<int[]>();
            var filter = Builders<BsonDocument>.Filter.Eq("_userid", userID);

            var document = productInCartTable.Find(filter).ToList();
            if (document == null || document.Count == 0)
            {
                return null;
            }
            foreach (var doc in document)
            {
                int[] tmp = { doc["_userid"].AsInt32, doc["_storeid"].AsInt32, doc["_productid"].AsInt32, doc["quantity"].AsInt32 };
                output.Add(tmp);
            }
            return output;
        }


        // productInStore function

        public bool addNewProductInStore(ProductInStore product)
        {
            if (IsTest)
                return true;
            var document = new BsonDocument
            {
                { "_id",  product.Store.Id+"_"+product.Product.Id } ,
                { "_storeid",  product.Store.Id},
                { "_productid",  product.Product.Id},
                { "quantity", product.Quantity },
            };
            try
            {
                ProductInStoreTable.InsertOneAsync(document);
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("DBmanager-addNewProductInStore- Add new productInStore failed - " + e + " .");
                return false;
            }
            return true;
        }

        public bool removeProductInStore(int storeID, int productID)
        {
            if (IsTest)
                return true;
            try
            {
                ProductInStoreTable.DeleteOneAsync(Builders<BsonDocument>.Filter.Eq("_storeid", storeID) & Builders<BsonDocument>.Filter.Eq("_productid", productID));
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("DBmanager-removeProductInStore- Remove ProductInStore failed - " + e + " .");
                return false;
            }
            return true;
        }

        public bool updateProductInStore(int storeID, int productID, int quantity)
        {
            if (IsTest)
                return true;

            var filter = Builders<BsonDocument>.Filter.Eq("_storeid", storeID) & Builders<BsonDocument>.Filter.Eq("_productid", productID);
            var update = Builders<BsonDocument>.Update.Set("quantity", quantity);

            try
            {
                ProductInStoreTable.UpdateOneAsync(filter, update);

            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("DBmanager-updateProductInCart- Update productInCart failed - " + e + " .");
                return false;
            }
            return true;
        }

        public int getProductInStoreQuntity(int storeID, int productID)
        {
            if (IsTest)
                return 0;
            var filter = Builders<BsonDocument>.Filter.Eq("_storeid", storeID) & Builders<BsonDocument>.Filter.Eq("_productid", productID);

            var document = ProductInStoreTable.Find(filter).ToList();
            if (document == null || document.Count == 0)
            {
                return -1;
            }
            int output = document[0]["quantity"].AsInt32;
            return output;
        }

        public List<ProductInStore> getAllProductInStore(Store store)
        {
            if (IsTest)
                return new List<ProductInStore>();
            List<ProductInStore> output = new List<ProductInStore>();
            var filter = Builders<BsonDocument>.Filter.Eq("_storeid", store.Id);

            var document = ProductInStoreTable.Find(filter).ToList();
            if (document == null || document.Count == 0)
            {
                return output;
            }
            foreach (var doc in document)
            {
                output.Add(new ProductInStore(doc["quantity"].AsInt32, store, getProduct(doc["_productid"].AsInt32)));
            }
            return output;
        }
    }
}
