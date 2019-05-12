﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;

namespace Acceptance_Tests
{
    [TestClass]
    public class RemoveProductsFromCartTests
    {
        ServiceLayer service;
        List<KeyValuePair<string, int>> list;
        List<string> toRemove;

        public void setUp()
        {
            service = ServiceLayer.getInstance();
            string tmp = service.initUser();
            service.register("user", "password", tmp);
            service.signIn("user", "password");
            service.openStore("store", "user");
            KeyValuePair<string, int> p1 = new KeyValuePair<string, int>("p1", 1);
            list = new List<KeyValuePair<string, int>>();
            toRemove = new List<string>();
            list.Add(p1);
            toRemove.Add("p1");
            service.createNewProductInStore("p1", "category", "details", 20, "store", "user");
            service.addProductsInStore(list, "store", "user");
            service.addProductsToCart(list, "store", "user");
            service.editProductQuantityInCart("p1", 0, "store", "user");
        }

        [TestMethod]
        public void TestMethod1_empty_success()
        {
            setUp();
            Assert.AreEqual(true, service.removeProductsFromCart(toRemove, "store", "user"));
            service.shutDown();
        }

        [TestMethod]
        public void TestMethod1_success()
        {
            setUp();
            service.editProductQuantityInCart("p1", 5, "store", "user");
            Assert.AreEqual(true, service.removeProductsFromCart(toRemove, "store", "user"));
            service.shutDown();
        }
    }
}