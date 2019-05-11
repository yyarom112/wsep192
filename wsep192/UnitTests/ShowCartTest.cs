using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;
using System.Collections.Generic;

namespace UnitTests
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
            store = new Store(1, "store", null, null);

        }

        [TestMethod]
        public void TestMethod_failure()
        {
            setUp();
            Assert.AreEqual(null, system.showCart(user.Id, store.Id));
            system.Users.Add(user.Id, null);
            Assert.AreEqual(null, system.showCart(user.Id, store.Id));
            system.Users.Remove(user.Id);
            system.Stores.Add(store.Id, null);
            Assert.AreEqual(null, system.showCart(user.Id, store.Id));
        }

        [TestMethod]
        public void TestMethod_failure_basket()
        {
            setUp();
            ShoppingBasket basket = new ShoppingBasket();
            Assert.AreEqual(null, basket.showCart(store.Id));

        }

        [TestMethod]
        public void TestMethod_success_system()
        {
            setUp();
            List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();
            user = new StubUser2(1,null,null,false,false, list);
            system.Users.Add(user.Id, user);
            system.Stores.Add(store.Id, store);
            //empty cart
            Assert.AreEqual(0, system.showCart(user.Id, store.Id).Count);
            list = new List<KeyValuePair<string, int>>();
            KeyValuePair<string, int> pair1 = new KeyValuePair<string, int>("p1", 3);
            list.Add(pair1);
            user = new StubUser2(1, null, null, false, false, list);
            //non-empty cart
            Assert.IsTrue(list[0].Equals(system.showCart(user.Id, store.Id)[0]));

        }

        [TestMethod]
        public void TestMethod_success_basket()
        {
            setUp();
            ShoppingBasket basket = new ShoppingBasket();
            basket.ShoppingCarts.Add(store.Id, new StubCart3(store.Id, null, new List<KeyValuePair<string, int>>()));
            //empty cart
            Assert.AreEqual(0, basket.showCart(store.Id).Count);
            basket.ShoppingCarts.Remove(store.Id);
            List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();
            list.Add(new KeyValuePair<string, int>("p", 3));
            basket.ShoppingCarts.Add(store.Id, new StubCart3(store.Id, null, list));
            //non-empty cart
            Assert.IsTrue(list[0].Equals(basket.showCart(store.Id)[0]));

        }

        [TestMethod]
        public void TestMethod_success_cart()
        {
            setUp();
            ShoppingCart cart = new ShoppingCart(store.Id,store);
            //empty cart
            Assert.AreEqual(0, cart.showCart().Count);

            //non-empty cart
            Product p = new Product(1, "p", null, null, -1);
            ProductInCart pc = new ProductInCart(3, cart, p);
            cart.Products.Add(p.Id,pc);
            List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();
            list.Add(new KeyValuePair<string, int>(p.ProductName,3));
            Assert.IsTrue(list[0].Equals(cart.showCart()[0]));

        }

    }

    internal class StubCart3 : ShoppingCart
    {
        List<KeyValuePair<string, int>> retVal;

        public StubCart3(int storeId, Store store, List<KeyValuePair<string, int>> ret) : base(storeId, store)
        {
            retVal = ret;
        }

        internal override List<KeyValuePair<string,int>> showCart()
        {
            return retVal;
        }
    }

    internal class StubUser2 : User
    {
        List<KeyValuePair<string, int>> retVal;


        public StubUser2(int id, string userName, string password, bool isAdmin, bool isRegistered, List<KeyValuePair<string, int>> ret) : base(id, userName, password, isAdmin, isRegistered)
        {
            retVal = ret;
        }

        internal override List<KeyValuePair<string, int>> showCart(int storeId)
        {
            return retVal;
        }
    }
}
