using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class addProductsToChart
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
            store = new Store(0, "blabla", 0, new User());
            user = new User(1, null,null, null, state.visitor, true, false, new ShoppingBasket(), new Dictionary<int, Role>());
            basket_user = user.Basket;
            admin = new User(0, "admin", "1234", "ashdod", state.signedIn, true, false, new ShoppingBasket(), new Dictionary<int, Role>());
            basket_admin = admin.Basket;
            p1 = new Product(0, "first", null, "", 5000, 0);
            p2 = new Product(1, "second", null, "", 5000, 0);
            p3 = new Product(2, "third", null, "", 5000, 0);
            p4 = new Product(3, "fourth", null, "", 5000, 0);
            pis1 = new ProductInStore(10000000, store, p1);
            pis2 = new ProductInStore(10000000, store, p2);
            pis3 = new ProductInStore(10000000, store, p3);
            pis4 = new ProductInStore(10000000, store, p4);
            store.Products.Add(p1.Id, pis1);
            store.Products.Add(p2.Id, pis2);
            store.Products.Add(p3.Id, pis3);
            store.Products.Add(p4.Id, pis4);
            sys = new TradingSystem();
            sys.StoreCounter = 1;
            sys.ProductCounter = 4;
            sys.UserCounter = 2;
            sys.Stores.Add(store.Id, store);
            sys.Users.Add(admin.Id, admin);
            sys.Users.Add(user.Id, user);


        }
        [TestMethod]
        public void TestMethod1_cart_successSenrio()
        {
            setUp();
            ShoppingCart cart = new ShoppingCart(store.Id,store);
            LinkedList<KeyValuePair<Product, int>> toInsert = new LinkedList<KeyValuePair<Product, int>>();
            Assert.AreEqual(0, cart.Products.Count);
            cart.addProducts(toInsert);
            Assert.AreEqual(0, cart.Products.Count);

            toInsert.AddLast(new KeyValuePair<Product, int>(this.p1, 10000000));

            cart.addProducts(toInsert);
            Assert.AreEqual(1, cart.Products.Count);
            Assert.AreEqual(true, cart.Products.ContainsKey(-1));
        }

        [TestMethod]
        public void TestMethod1_cart_updateSenrio()
        {
            setUp();
            ShoppingCart cart = new ShoppingCart(store.Id, store);

            LinkedList<KeyValuePair<Product, int>> toInsert = new LinkedList<KeyValuePair<Product, int>>();
            Assert.AreEqual(0, cart.Products.Count);
            cart.addProducts(toInsert);
            Assert.AreEqual(0, cart.Products.Count);

            toInsert.AddLast(new KeyValuePair<Product, int>(this.p1, 10000000));

            cart.addProducts(toInsert);
            Assert.AreEqual(1, cart.Products.Count);
            Assert.AreEqual(true, cart.Products.ContainsKey(-1));

            cart.addProducts(toInsert);
            Assert.AreEqual(1, cart.Products.Count);
            cart.addProducts(toInsert);
            Assert.AreEqual(30000000, cart.Products[-1].Quantity);
        }

        [TestMethod]
        public void TestMethod1_basket_successSenrio()
        {
            setUp();
            Assert.AreEqual(0, basket_user.ShoppingCarts.Count);
            LinkedList<KeyValuePair<Product, int>> toInsert = new LinkedList<KeyValuePair<Product, int>>();
            Assert.AreEqual(null, basket_user.addProductsToCart(toInsert,store.Id));
            Assert.AreEqual(0, basket_user.ShoppingCarts.Count);
            Assert.AreEqual(store.Id, basket_user.addProductsToCart(toInsert, store.Id).StoreId);
            Assert.AreEqual(1, basket_user.ShoppingCarts.Count);
        }


        [TestMethod]
        public void TestMethod1_basket_updateSenrio()
        {
            setUp();
            Assert.AreEqual(0, basket_user.ShoppingCarts.Count);
            LinkedList<KeyValuePair<Product, int>> toInsert = new LinkedList<KeyValuePair<Product, int>>();
            Assert.AreEqual(null, basket_user.addProductsToCart(toInsert, store.Id));
            Assert.AreEqual(0, basket_user.ShoppingCarts.Count);
            Assert.AreEqual(store.Id, basket_user.addProductsToCart(toInsert, store.Id).StoreId);
            Assert.AreEqual(1, basket_user.ShoppingCarts.Count);

        }
    }
}
