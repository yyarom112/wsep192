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
            if (document==null || document.Count==0)
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


    }
}
