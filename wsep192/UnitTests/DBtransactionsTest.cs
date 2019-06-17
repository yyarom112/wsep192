using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.DataLayer;
using src.Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

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
            db.Db = new DBmanager(false);
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
        public void TestMethod_register()
        {
            Setup();
            db.registerNewUserDB(admin);
            DBmanager checkDB = db.Db;
            Assert.AreEqual("guti", checkDB.getUser(admin.Id).UserName);
            Assert.AreEqual(1, checkDB.getProductInCartquntity(admin.Id, store.Id, p1.Id));
        }


        [TestMethod]
        public void TestMethod_OpenStoreDB()
        {
            Setup();
            db.OpenStoreDB(store, this.adminOwner);
            DBmanager checkDB = new DBmanager(false);
            Assert.AreEqual(store.Name, checkDB.getStore(store.Id).Name);
            Assert.AreEqual(10, checkDB.getProductInStoreQuntity(store.Id, p1.Id));
            Assert.AreEqual(true, checkDB.isOwnerDB(store.Id, admin.Id));
        }
        [TestMethod]
        public void TestMethod_initsystem()
        {
            Setup();

            db.OpenStoreDB(store, this.adminOwner);
            TradingSystem sysTest = new TradingSystem(null, null);
            db.initSystem(sysTest);
            Assert.AreEqual(true, sysTest.Stores.ContainsKey(store.Id));


        }


        [TestMethod]
        public void TestMethod_signIn()
        {
            Setup();

            db.registerNewUserDB(admin);
            store.Roles = new TreeNode<Role>(new Role(store, admin));
            store.RolesDictionary = new Dictionary<int, TreeNode<Role>>();
            TradingSystem sysTest = new TradingSystem(null, null);
            sysTest.Stores.Add(store.Id, store);
            db.signIn(admin.Id, sysTest);
            Assert.AreEqual(true, sysTest.Users.ContainsKey(admin.Id));

        }

        [TestMethod]
        public void TestMethod_AddProductToCart()
        {
            Setup();

            db.AddProductToCart(admin.Basket.ShoppingCarts[store.Id].Products, admin.Id);
            DBmanager checkDB = new DBmanager(false);
            Assert.AreEqual(1, checkDB.getProductInCartquntity(admin.Id, store.Id, p1.Id));



        }

        [TestMethod]
        public void TestMethod_removeProductsFromCart()
        {
            Setup();

            List<int> productToremove = new List<int>();
            productToremove.Add(p1.Id);

            db.AddProductToCart(admin.Basket.ShoppingCarts[store.Id].Products, admin.Id);
            DBmanager checkDB = new DBmanager(false);
            Assert.AreEqual(7, checkDB.getProductInCartquntity(admin.Id, store.Id, p1.Id));

            db.removeProductsFromCart(productToremove, store.Id, admin.Id);
            Assert.AreEqual(-1, checkDB.getProductInCartquntity(admin.Id, store.Id, p1.Id));

        }

        [TestMethod]
        public void TestMethod_EditProductQuantityInCart()
        {
            Setup();

            List<int> productToremove = new List<int>();
            productToremove.Add(p1.Id);

            db.AddProductToCart(admin.Basket.ShoppingCarts[store.Id].Products, admin.Id);
            DBmanager checkDB = new DBmanager(false);
            Assert.AreEqual(1, checkDB.getProductInCartquntity(admin.Id, store.Id, p1.Id));

            admin.Basket.ShoppingCarts[store.Id].Products[p1.Id].Quantity = 7;

            db.EditProductQuantityInCart(p1.Id, store.Id, admin.Id, 7);
            Assert.AreEqual(7, checkDB.getProductInCartquntity(admin.Id, store.Id, p1.Id));
        }


        [TestMethod]
        public void TestMethod_transactionTest()
        {
            Setup();
            var session = db.Db.Client.StartSession();
            session.StartTransaction();

            db.Db.addNewProduct(p1);
            session.AbortTransaction();
            Assert.AreEqual(null, db.Db.getProduct(p1.Id));

        }
    }
}
