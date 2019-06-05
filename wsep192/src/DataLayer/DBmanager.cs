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
        private bool isTest;
        private const string dbAdress = "mongodb+srv://wesp192:wesp192@cluster0-r882l.gcp.mongodb.net/test";
        private const string dbName = "wesp192";
        private IMongoDatabase db;
        private IMongoCollection<BsonDocument> usersTable;
        private IMongoCollection<BsonDocument> storesTable;
        private IMongoCollection<BsonDocument> productsTable;

        public bool IsTest { get => isTest; set => isTest = value; }

        public DBmanager()
        {
            IsTest = false;
            connect();
            usersTable = db.GetCollection<BsonDocument>("Users");
            storesTable = db.GetCollection<BsonDocument>("Stores");
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




    }
}
