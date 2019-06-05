﻿using MongoDB.Bson;
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
        private bool isTest;
        private const string dbAdress = "mongodb+srv://wesp192:wesp192@cluster0-r882l.gcp.mongodb.net/test";
        private const string dbName = "wesp192";
        private IMongoDatabase db;
        private IMongoCollection<BsonDocument> usersTable;
        private IMongoCollection<BsonDocument> storesTable;
        private IMongoCollection<BsonDocument> ownersTable;
        private IMongoCollection<BsonDocument> managersTable;
        private IMongoCollection<BsonDocument> productsTable;
        private IMongoCollection<BsonDocument> discountPoliciesTable;
        private IMongoCollection<BsonDocument> purchasePoliciesTable;

        public bool IsTest { get => isTest; set => isTest = value; }

        public DBmanager()
        {
            IsTest = false;
            connect();
            usersTable = db.GetCollection<BsonDocument>("Users");
            storesTable = db.GetCollection<BsonDocument>("Stores");
            ownersTable = db.GetCollection<BsonDocument>("Owners");
            managersTable = db.GetCollection<BsonDocument>("Managers");
            productsTable = db.GetCollection<BsonDocument>("Products");
        }

        public void connect()
        {
            if (IsTest)
                return;
            try
            {
                var client = new MongoClient(dbAdress);
                db = client.GetDatabase(dbName);
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

        //User table function
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
            var update = Builders<BsonDocument>.Update.Set("username", user.UserName);
            update.AddToSet("password", user.Password);
            update.AddToSet("isAdmin", user.IsAdmin);
            update.AddToSet("state", user.State);
            try
            {
                usersTable.UpdateOne(filter, update);
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("DBmanager-updateUser- Remove user failed - " + e + " .");
                return false;
            }
            return true;
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

    }
}
