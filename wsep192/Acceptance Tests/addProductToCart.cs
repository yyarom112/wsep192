using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;

namespace Acceptance_Tests
{
    [TestClass]
    public class addProductToCart
    {

        private TradingSystem sys;
        private Encryption encrypt;

        private User admin;
        private ShoppingBasket basket_admin;

        private User user;
        private ShoppingBasket basket_user;


        private Product p1;
        private Product p2;
        private Product p3;
        private Product p4;
        private ProductInStore pis1;
        private ProductInStore pis2;
        private ProductInStore pis3;
        private ProductInStore pis4;

        private Store store;


        public void setUp()
        {
            admin = new User(0, "admin", "123456", true, true);
            basket_admin = admin.Basket;
            user = new User(1, null, null, false, false);
            basket_user = user.Basket;

            store = new Store(-1, "store", 0, null, null);

            p1 = new Product(0, "first", null, "", 5000);
            p2 = new Product(1, "second", null, "", 5000);
            p3 = new Product(2, "third", null, "", 5000);
            p4 = new Product(3, "fourth", null, "", 5000);
            pis1 = new ProductInStore(10000000, store, p1);
            pis2 = new ProductInStore(10000000, store, p2);
            pis3 = new ProductInStore(10000000, store, p3);
            pis4 = new ProductInStore(10000000, store, p4);
            store.Products.Add(p1.Id, pis1);
            store.Products.Add(p2.Id, pis2);
            store.Products.Add(p3.Id, pis3);
            store.Products.Add(p4.Id, pis4);
            sys = new TradingSystem(null, null);
            sys.StoreCounter = 1;
            sys.ProductCounter = 4;
            sys.UserCounter = 2;
            sys.Stores.Add(store.Id, store);
            sys.Users.Add(admin.Id, admin);
            sys.Users.Add(user.Id, user);


        }
        /*
        [TestMethod]
        public void TestMethod1_succAddProductsToCartWithSave()
        {
            setUp();

            List<KeyValuePair<int, int>> toInsert = new List<KeyValuePair<int, int>>();

            toInsert.Add(new KeyValuePair<int, int>(p1.Id, 1));
            toInsert.Add(new KeyValuePair<int, int>(p2.Id, 1));
            toInsert.Add(new KeyValuePair<int, int>(p3.Id, 1));

            Assert.AreEqual(true, sys.addProductsToCart(toInsert, store.Id, user.Id));

            toInsert = new List<KeyValuePair<int, int>>();
            toInsert.Add(new KeyValuePair<int, int>(p4.Id, 1));

            Assert.AreEqual(true, sys.addProductsToCart(toInsert, store.Id, user.Id));


        }
        */
        [TestMethod]
        public void TestMethod1_succAddProductsToCartWitoutSave()
        {
            
        }
    }
}
