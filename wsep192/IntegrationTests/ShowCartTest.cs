using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;
using System.Collections.Generic;

namespace IntegrationTests
{
    [TestClass]
    public class ShowCartTest
    {
        TradingSystem system;
        User user;
        Store store;

        public void setUp()
        {
            system = new TradingSystem(null, null);
            user = new User(1, "user", "1234", false, false);
            store = new Store(1, "store");

        }

        [TestMethod]
        public void TestMethod_failure()
        {
            setUp();

            //failure system showCart
            Assert.AreEqual(null, system.showCart(user.Id, store.Id));
            system.Users.Add(user.Id, user);
            Assert.AreEqual(null, system.showCart(user.Id, store.Id));
            system.Users.Remove(user.Id);
            system.Stores.Add(store.Id, store);
            Assert.AreEqual(null, system.showCart(user.Id, store.Id));

            //failure basket showCart
            Assert.AreEqual(null, user.Basket.showCart(store.Id));

        }

        [TestMethod]
        public void TestMethod_success()
        {
            setUp();
            successSetUp();
            //empty cart
           List<KeyValuePair<string,int>> result = system.showCart(user.Id, store.Id);
            Assert.AreEqual(0, result.Count);
            //non-empty cart
            addProducts();
            List<KeyValuePair<string, int>> l = new List<KeyValuePair<string, int>>();
            l.Add(new KeyValuePair<string, int>("p1", 3));
            result = system.showCart(user.Id, store.Id);
            Assert.IsTrue(l[0].Equals(result[0]));
           

        }

        private void addProducts()
        {
            Product p1 = new Product(1, "p1", null, null, -1);
            ProductInCart pc1 = new ProductInCart(3, user.Basket.ShoppingCarts[store.Id], p1);
            user.Basket.ShoppingCarts[store.Id].Products.Add(1, pc1);
        }

        private void successSetUp()
        {
            system.Users.Add(user.Id, user);
            system.Stores.Add(store.Id, store);
            user.Basket.ShoppingCarts.Add(store.Id, new ShoppingCart(store.Id, store));
        }
    }
}
