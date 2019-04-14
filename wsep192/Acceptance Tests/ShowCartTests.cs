using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;

namespace Acceptance_Tests
{
    [TestClass]
    public class ShowCartTests
    {
        ServiceLayer service;
        List<KeyValuePair<string, int>> list;

        [TestMethod]
        public void setUp()
        {
            service = new ServiceLayer();
            service.init("admin", "1234");
            service.initUser("tmpuser");
            service.register("user", "password", "tmpuser");
            service.signIn("user", "password");
            service.openStore("store", "user");
            KeyValuePair<string, int> p1 = new KeyValuePair<string, int>("p1", 1);
            list = new List<KeyValuePair<string, int>>();
            list.Add(p1);
            service.createNewProductInStore("p1", "category", "details", 20, "store", "user");
            service.addProductsInStore(list, "store", "user");
            service.addProductsToCart(list, "store", "user");
        }



        [TestMethod]
        public void TestMethod1_success_full()
        {
            setUp();
            Assert.AreEqual("Store Name: store\nProduct Name\t\t\tQuantity\n" +
                "1. p1\t\t\t1\n", service.showCart("store", "user"));
        }


        [TestMethod]
        public void TestMethod1_success_empty()
        {
            setUp();
            service.removeProductsFromCart(list, "store", "user");
            Assert.AreEqual("Store Name: store\nCart is empty\n", service.showCart("store", "user"));
        }

    }
}