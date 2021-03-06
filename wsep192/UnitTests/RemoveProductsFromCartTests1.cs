﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.DataLayer;
using src.Domain;

namespace UnitTests
{
    [TestClass]
    public class RemoveProductsFromCartTests1
    {


        TradingSystem system;
        User user;
        Store store;
        Product product;
        List<int> productsToRemove;


        public void setup()
        {
            system = new TradingSystem(null, null);
            user = new User(1, "user", "1234", false, false);
            store = new Store(1, "store");
            product = new Product(1, "product", null, null, -1);
        }


        [TestMethod]
        public void TestMethod1_testmethod_failure_remove_cart()
        {
            setup();
            productsToRemove = new List<int>();
            productsToRemove.Add(product.Id);
            ShoppingCart cart = new ShoppingCart(store.Id, null);
            Assert.AreEqual(false, cart.removeProductsFromCart(productsToRemove)); //empty list of products in cart
        }

        [TestMethod]
        public void TestMethod1_testmethod_failure_remove_basket()
        {
            setup();
            ShoppingBasket basket = new ShoppingBasket();
            DBtransactions.getInstance(true);
            Assert.AreEqual(false, basket.removeProductsFromCart(null, store.Id,1));

        }



        [TestMethod]
        public void TestMethod1_testmethod_failure_remove_system()
        {
            setup();
            Assert.AreEqual(false, system.removeProductsFromCart(null, store.Id, user.Id));
            system.Users.Add(user.Id, null);
            Assert.AreEqual(false, system.removeProductsFromCart(null, store.Id, user.Id));
            system.Users.Remove(user.Id);
            system.Stores.Add(store.Id, null);
            Assert.AreEqual(false, system.removeProductsFromCart(null, store.Id, user.Id));
        }


        [TestMethod]
        public void testmethod_cart_success_remove()
        {
            setup();
            productsToRemove = new List<int>();
            productsToRemove.Add(product.Id);
            ShoppingCart cart = new ShoppingCart(store.Id, null);
            ProductInCart pc = new ProductInCart(2, cart, null);
            cart.Products.Add(product.Id, pc);
            Assert.AreEqual(true, cart.removeProductsFromCart(productsToRemove));
        }

        [TestMethod]
        public void testmethod_basket_success_remove()
        {
            setup();
            productsToRemove = new List<int>();
            productsToRemove.Add(product.Id);
            ShoppingBasket basket = new ShoppingBasket();
            ShoppingCart cart = new StubCart(store.Id, null, true);
            basket.ShoppingCarts.Add(store.Id, cart);
            DBtransactions.getInstance(true);
            Assert.AreEqual(true, basket.removeProductsFromCart(productsToRemove, store.Id,-1));
        }

        [TestMethod]
        public void testmethod_system_success_remove()
        {
            setup();
            user = new StubUser(1,null, null,false,false,true);
            system.Users.Add(user.Id, user);
            system.Stores.Add(store.Id, null);
            Assert.AreEqual(true, system.removeProductsFromCart(productsToRemove, store.Id, user.Id));
        }

        internal class StubCart : ShoppingCart
        {
            bool retVal;

            public StubCart(int storeId, Store store, bool ret) : base(storeId, store)
            {
                retVal = ret;
            }

            internal override bool removeProductsFromCart(List<int> productsToRemove)
            {
                return retVal;
            }
        }

        internal class StubUser : User
        {
            bool retVal;


            public StubUser(int id, string userName, string password, bool isAdmin, bool isRegistered,bool ret) : base(id, userName, password, isAdmin, isRegistered)
            {
                retVal=ret;
            }

            internal override bool removeProductsFromCart(List<int> productsToRemove, int storeId)
            {
                return retVal;
            }
        }


    }
}
