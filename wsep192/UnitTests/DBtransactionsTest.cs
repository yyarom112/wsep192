using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.DataLayer;
using src.Domain;
using MongoDB.Bson;
using MongoDB.Driver;

namespace UnitTests
{
    [TestClass]
    public class DBtransactionsTest
    {
        private Store store;
        private User admin;
        private Product p1;
        private ProductInStore pis1;
        private ProductInCart pic1;
        private Owner adminOwner;
        private DBtransactions db;

        public void Setup()
        {
            db = DBtransactions.getInstance(false);
            store = new Store(7, "adidas");
            admin = new User(14, "guti", "1234", true, true);
            p1 = new Product(4, "ramos", "", "", 10);
            pis1 = new ProductInStore(10, store, p1);
            store.Products.Add(p1.Id, pis1);
            admin.Basket.ShoppingCarts.Add(store.Id, new ShoppingCart(store.Id, store));
            pic1 = new ProductInCart(1, admin.Basket.ShoppingCarts[store.Id], p1);
            admin.Basket.ShoppingCarts[store.Id].Products.Add(p1.Id, pic1);
            adminOwner = new Owner(store, admin);
            store.RolesDictionary.Add(admin.Id, store.Roles.AddChild(adminOwner));
            admin.Roles.Add(store.Id, adminOwner);
        }




        [TestMethod]
        public void TestMethod_register_success()
        {
            Setup();
            var session = db.Db.Client.StartSession();
            session.StartTransaction();

            db.registerNewUserDB(admin);
            DBmanager checkDB = new DBmanager();
            Assert.AreEqual("guti", checkDB.getUser(admin.Id).UserName);
            Assert.AreEqual(1, checkDB.getProductInCartquntity(admin.Id,store.Id,p1.Id));
            Assert.AreEqual(true, checkDB.isOwnerDB(store.Id,admin.Id));

            session.AbortTransaction();
        }
    }
}
