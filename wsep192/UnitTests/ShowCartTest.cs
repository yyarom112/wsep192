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
            Assert.AreEqual("Error : Invalid user or store", system.showCart(user.Id, store.Id));
            system.Users.Add(user.Id, null);
            Assert.AreEqual("Error : Invalid user or store", system.showCart(user.Id, store.Id));
            system.Users.Remove(user.Id);
            system.Stores.Add(store.Id, null);
            Assert.AreEqual("Error : Invalid user or store", system.showCart(user.Id, store.Id));
        }

        [TestMethod]
        public void TestMethod_failure_basket()
        {
            setUp();
            ShoppingBasket basket = new ShoppingBasket();
            Assert.AreEqual("Error : Shopping basket does not contain this store",
                basket.showCart(store.Id));

        }

        //[TestMethod]
        public void TestMethod_success_system()
        {
            setUp();
            user = new StubUser2(1,null,null,false,false, "Store Name: store\nCart is empty\n");
            system.Users.Add(user.Id, user);
            system.Stores.Add(store.Id, store);
            //empty cart
            Assert.AreEqual("Store Name: store\nCart is empty\n", system.showCart(user.Id, store.Id));

            user = new StubUser2(1, null, null, false, false, "Store Name: store\nProduct Name\t\t\tQuantity\n" +
                "1. p1\t\t\t3\n2. p2\t\t\t1\n");
            //non-empty cart
            Assert.AreEqual("Store Name: store\nProduct Name\t\t\tQuantity\n" +
                "1. p1\t\t\t3\n2. p2\t\t\t1\n",
                system.showCart(user.Id, store.Id));

        }

        [TestMethod]
        public void TestMethod_success_basket()
        {
            setUp();
            ShoppingBasket basket = new ShoppingBasket();
            basket.ShoppingCarts.Add(store.Id, new StubCart3(store.Id, null, "Store Name: store\nCart is empty\n"));
            
 
            //empty cart
            Assert.AreEqual("Store Name: store\nCart is empty\n", basket.showCart(store.Id));

            basket.ShoppingCarts.Remove(store.Id);
            basket.ShoppingCarts.Add(store.Id, new StubCart3(store.Id, null, "Store Name: store\nProduct Name\t\t\tQuantity\n" +
                "1. p1\t\t\t3\n2. p2\t\t\t1\n"));
            //non-empty cart
            Assert.AreEqual("Store Name: store\nProduct Name\t\t\tQuantity\n" +
                "1. p1\t\t\t3\n2. p2\t\t\t1\n",
                basket.showCart(store.Id));

        }

        [TestMethod]
        public void TestMethod_success_cart()
        {
            setUp();
            ShoppingCart cart = new ShoppingCart(store.Id,store);
            //empty cart
            Assert.AreEqual("Store Name: store\nCart is empty\n", cart.showCart());

            //non-empty cart
            Product p = new Product(1, "p", null, null, -1);
            ProductInCart pc = new ProductInCart(3, cart, p);
            cart.Products.Add(p.Id,pc);
            Assert.AreEqual("Store Name: store\nProduct Name\t\t\tQuantity\n" +
                "1. p\t\t\t3\n",
                cart.showCart());

        }

    }

    internal class StubCart3 : ShoppingCart
    {
        string retVal;

        public StubCart3(int storeId, Store store, string ret) : base(storeId, store)
        {
            retVal = ret;
        }

        internal override string showCart()
        {
            return retVal;
        }
    }

    internal class StubUser2 : User
    {
        string retVal;


        public StubUser2(int id, string userName, string password, bool isAdmin, bool isRegistered, string ret) : base(id, userName, password, isAdmin, isRegistered)
        {
            retVal = ret;
        }

        internal override string showCart(int storeId)
        {
            return retVal;
        }
    }
}
