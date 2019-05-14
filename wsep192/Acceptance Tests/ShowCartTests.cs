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

        
        public void setUp()
        {
            service = ServiceLayer.getInstance();
            string tmp = service.initUser();
            service.register("user", "password",tmp);
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
            var res = service.showCart("store", "user")[0];
            Assert.IsTrue((list[0]).Equals(res));
            service.shutDown();
        }


        [TestMethod]
        public void TestMethod1_success_empty()
        {
            setUp();
            List<string> toRemove = new List<string>();
            toRemove.Add("p1");
            service.removeProductsFromCart(toRemove, "store", "user");
            Assert.AreEqual((new List<KeyValuePair<string, int>>()).Count, service.showCart("store", "user").Count);
            service.shutDown();
        }

    }
}