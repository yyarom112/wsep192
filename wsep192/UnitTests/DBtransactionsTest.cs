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
            var session = db.Db.Client.StartSession();
            session.StartTransaction();

            db.registerNewUserDB(admin);
            DBmanager checkDB = new DBmanager();
            Assert.AreEqual("guti", checkDB.getUser(admin.Id).UserName);
            Assert.AreEqual(1, checkDB.getProductInCartquntity(admin.Id,store.Id,p1.Id));

            session.AbortTransaction();
        }


        [TestMethod]
        public void TestMethod_OpenStoreDB()
        {
            Setup();
            var session = db.Db.Client.StartSession();
            session.StartTransaction();

            db.OpenStoreDB(store,this.adminOwner);
            DBmanager checkDB = new DBmanager();
            Assert.AreEqual(store.Name, checkDB.getStore(store.Id).Name);
            Assert.AreEqual(10, checkDB.getProductInStoreQuntity(store.Id, p1.Id));
            Assert.AreEqual(true, checkDB.isOwnerDB(store.Id, admin.Id));

            session.AbortTransaction();

        }


        [TestMethod]
        public void TestMethod_AddProductToCart()
        {
            Setup();
            var session = db.Db.Client.StartSession();
            session.StartTransaction();

            db.AddProductToCart(admin.Basket.ShoppingCarts[store.Id].Products,admin.Id);
            DBmanager checkDB = new DBmanager();
            Assert.AreEqual(1, checkDB.getProductInCartquntity(admin.Id, store.Id, p1.Id));


            session.AbortTransaction();

        }

        [TestMethod]
        public void TestMethod_removeProductsFromCart()
        {
            Setup();
            var session = db.Db.Client.StartSession();
            session.StartTransaction();
            List<int> productToremove = new List<int>();
            productToremove.Add(p1.Id);

            db.AddProductToCart(admin.Basket.ShoppingCarts[store.Id].Products, admin.Id);
            DBmanager checkDB = new DBmanager();
            Assert.AreEqual(1, checkDB.getProductInCartquntity(admin.Id, store.Id, p1.Id));

            db.removeProductsFromCart(productToremove,store.Id,admin.Id);
            Assert.AreEqual(-1, checkDB.getProductInCartquntity(admin.Id, store.Id, p1.Id));
            session.AbortTransaction();

        }

        [TestMethod]
        public void TestMethod_EditProductQuantityInCart()
        {
            Setup();
            var session = db.Db.Client.StartSession();
            session.StartTransaction();
            List<int> productToremove = new List<int>();
            productToremove.Add(p1.Id);

            db.AddProductToCart(admin.Basket.ShoppingCarts[store.Id].Products, admin.Id);
            DBmanager checkDB = new DBmanager();
            Assert.AreEqual(1, checkDB.getProductInCartquntity(admin.Id, store.Id, p1.Id));

            admin.Basket.ShoppingCarts[store.Id].Products[p1.Id].Quantity = 7;

            db.EditProductQuantityInCart(p1.Id, store.Id, admin.Id, 7);
            Assert.AreEqual(7, checkDB.getProductInCartquntity(admin.Id, store.Id, p1.Id));
            session.AbortTransaction();

        }

        [TestMethod]
        public void TestMethod_AddProductInstore()
        {
            Setup();
            var session = db.Db.Client.StartSession();
            session.StartTransaction();

            Dictionary<int, ProductInStore> products = new Dictionary<int, ProductInStore>();
            products.Add(10, pis1);
            db.addProductInstore(products, admin.Id);
            DBmanager checkDB = new DBmanager();
            Assert.AreEqual(10, checkDB.getProductInStoreQuntity(store.Id, pis1.Product.Id));

            session.AbortTransaction();
        }

        [TestMethod]
        public void TestMethod_removeProductsInSrore()
        {
            Setup();
            var session = db.Db.Client.StartSession();
            session.StartTransaction();

            List<int> productToremove = new List<int>();
            productToremove.Add(pis1.Product.Id);

            Dictionary<int, ProductInStore> products = new Dictionary<int, ProductInStore>();
            products.Add(10, pis1);
            db.addProductInstore(products, admin.Id);
            DBmanager checkDB = new DBmanager();
            Assert.AreEqual(10, checkDB.getProductInStoreQuntity(store.Id, pis1.Product.Id));

            db.removeProductInStore(productToremove, store.Id, admin.Id);
            Assert.AreEqual(-1, checkDB.getProductInStoreQuntity(store.Id, pis1.Product.Id));
            session.AbortTransaction();
        }

        [TestMethod]
        public void TestMethod_EditProductQuantityInStore()
        {
            Setup();
            var session = db.Db.Client.StartSession();
            session.StartTransaction();

            Dictionary<int, ProductInStore> products = new Dictionary<int, ProductInStore>();
            products.Add(10, pis1);
            db.addProductInstore(products, admin.Id);
            DBmanager checkDB = new DBmanager();
            Assert.AreEqual(10, checkDB.getProductInStoreQuntity(store.Id, pis1.Product.Id));

            store.Products[pis1.Product.Id].Quantity = 5;

            db.editProductInStore(pis1.Product.Id, store.Id, admin.Id, 5);
            Assert.AreEqual(5, checkDB.getProductInStoreQuntity(store.Id, pis1.Product.Id));
            session.AbortTransaction();
        }
    }
}
