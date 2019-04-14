using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;

namespace Acceptance_Tests
{
    [TestClass]
    public class addToCartReq26
    {
        private ServiceLayer service;

        public void Setup()
        {
            service = new ServiceLayer();
            service.init("admin", "1234");
            service.register("user", "1234", service.initUser());
            service.signIn("admin", "1234");
            service.openStore("store", "admin");
            service.createNewProductInStore("p1", "", "", 10, "store", "admin");
            List<KeyValuePair<string, int>> toInsert = new List<KeyValuePair<string, int>>();
            toInsert.Add(new KeyValuePair<string, int>("p1", 100));
            service.addProductsInStore(toInsert, "store", "admin");
        }

        [TestMethod]
        public void TestMethod_SaveProductInChart_visitor()
        {
            Setup();
            List<KeyValuePair<string, int>> toInsert = new List<KeyValuePair<string, int>>();
            toInsert.Add(new KeyValuePair<string, int>("p1", 1));
            Assert.AreEqual(true,service.addProductsToCart(toInsert, "store", "user"));

        }

        [TestMethod]
        public void TestMethod_SaveProductInChart_registered()
        {
            Setup();
            service.signOut("admin");
            List<KeyValuePair<string, int>> toInsert = new List<KeyValuePair<string, int>>();
            toInsert.Add(new KeyValuePair<string, int>("p1", 1));
            Assert.AreEqual(true, service.addProductsToCart(toInsert, "store", "admin"));

        }

        [TestMethod]
        public void TestMethod_SaveProductInChart_signIn()
        {
            Setup();
            List<KeyValuePair<string, int>> toInsert = new List<KeyValuePair<string, int>>();
            toInsert.Add(new KeyValuePair<string, int>("p1", 1));
            Assert.AreEqual(true, service.addProductsToCart(toInsert, "store", "admin"));
        }

        [TestMethod]
        public void TestMethod_SaveProductInChart_notconnected()
        {
            Setup();
            List<KeyValuePair<string, int>> toInsert = new List<KeyValuePair<string, int>>();
            toInsert.Add(new KeyValuePair<string, int>("p1", 1));
            Assert.AreEqual(true, service.addProductsToCart(toInsert, "store", "Ali Baba"));
        }

        [TestMethod]
        public void TestMethod_SaveProductInChart_negativeAmount()
        {
            Setup();
            List<KeyValuePair<string, int>> toInsert = new List<KeyValuePair<string, int>>();
            toInsert.Add(new KeyValuePair<string, int>("p1", -1));
            Assert.AreEqual(false, service.addProductsToCart(toInsert, "store", "admin"));
        }

        [TestMethod]
        public void TestMethod_SaveProductInChart_zeroAmount()
        {
            Setup();
            List<KeyValuePair<string, int>> toInsert = new List<KeyValuePair<string, int>>();
            toInsert.Add(new KeyValuePair<string, int>("p1", 0));
            Assert.AreEqual(true, service.addProductsToCart(toInsert, "store", "admin"));
        }


    }
}
