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
        private bool isTest=true;
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

        public DBmanager()
        {
            IsTest = false;
            connect();
            usersTable = db.GetCollection<BsonDocument>("Users");
            storesTable = db.GetCollection<BsonDocument>("Stores");
            ownersTable = db.GetCollection<BsonDocument>("Owners");
            managersTable = db.GetCollection<BsonDocument>("Managers");
            productsTable = db.GetCollection<BsonDocument>("Products");
            productInCartTable= db.GetCollection<BsonDocument>("ProductsInCart");
            ProductInStoreTable = db.GetCollection<BsonDocument>("ProductInStore");

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
                ErrorManager.Instance.WriteToLog("DBmanager- connect- Connect to db failed - "+e+" .");
                return;
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
                usersTable.InsertOne(document);
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
                usersTable.DeleteOne(Builders<BsonDocument>.Filter.Eq("_id", id));
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
                usersTable.UpdateOne(filter, updateName);
                usersTable.UpdateOne(filter, updatePassword);
                usersTable.UpdateOne(filter, updateIsAdmin);
                usersTable.UpdateOne(filter, updateState);

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
                storesTable.InsertOne(document);
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
                storesTable.DeleteOne(Builders<BsonDocument>.Filter.Eq("_id", id));
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
                storesTable.UpdateOne(filter, update);
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("DBmanager-updateStore- Update Store failed - " + e + " .");
                return false;
            }
            return true;
        }

        //Owners table functions
        public bool addNewOwner(Owner owner)
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
                ownersTable.InsertOne(document);
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
                ownersTable.DeleteOne(Builders<BsonDocument>.Filter.Eq("_user id", user_id));
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("DBmanager-removeOwner- Remove owner failed - " + e + " .");
                return false;
            }
            return true;
        }
        //Managers table functions
        public bool addNewManager(Manager manager)
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
                managersTable.InsertOne(document);
            }
            catch (Exception e) { 
            
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
                managersTable.DeleteOne(Builders<BsonDocument>.Filter.Eq("_user id", user_id));
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("DBmanager-removeManager- Remove manager failed - " + e + " .");
                return false;
            }
            return true;
        }

        public int getPermission(int storeID, int userID)
        {
            if (IsTest)
                return 0;
            var filter = Builders<BsonDocument>.Filter.Eq("_store id", storeID) & Builders<BsonDocument>.Filter.Eq("_user id", userID);

            var document = ProductInStoreTable.Find(filter).ToList();
            if (document == null || document.Count == 0)
            {
                return -1;
            }
            int output = document[0]["quantity"].AsInt32;
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
                productsTable.InsertOne(document);
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
                productsTable.DeleteOne(Builders<BsonDocument>.Filter.Eq("_product id", product_id));
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("DBmanager-removeManager- Remove manager failed - " + e + " .");
                return false;
            }
            return true;
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
                productInCartTable.InsertOne(document);
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("DBmanager-addProductInCart- Add new ProductInCart failed - " + e + " .");
                return false;
            }
            return true;
        }

        public bool removeProductInCartBy(int userID,int storeID, int productID)
        {
            if (IsTest)
                return true;
            try
            {
                productInCartTable.DeleteOne(Builders<BsonDocument>.Filter.Eq("_storeid", storeID) & Builders<BsonDocument>.Filter.Eq("_userid", userID) & Builders<BsonDocument>.Filter.Eq("_productid", productID));
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
                productInCartTable.DeleteMany(Builders<BsonDocument>.Filter.Eq("_storeid", storeId));
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
                productInCartTable.DeleteMany(Builders<BsonDocument>.Filter.Eq("_userid", userID));
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
                productInCartTable.UpdateOne(filter, update);

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
            var filter =  Builders<BsonDocument>.Filter.Eq("_userid", userID);

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
                ProductInStoreTable.InsertOne(document);
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
                ProductInStoreTable.DeleteOne(Builders<BsonDocument>.Filter.Eq("_storeid", storeID)  & Builders<BsonDocument>.Filter.Eq("_productid", productID));
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
                ProductInStoreTable.UpdateOne(filter, update);

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
            var filter = Builders<BsonDocument>.Filter.Eq("_storeid", storeID) &  Builders<BsonDocument>.Filter.Eq("_productid", productID);

            var document = ProductInStoreTable.Find(filter).ToList();
            if (document == null || document.Count == 0)
            {
                return -1;
            }
            int output = document[0]["quantity"].AsInt32;
            return output;
        }
    }
}
