using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;
using System.Collections.Generic;

namespace Acceptance_Tests
{
    [TestClass]
    public class AddProductsInStore
    {

        private ServiceLayer service;

        public void setUp()
        {
            service = new ServiceLayer();
            service.init("admin", "1234");
            string owner = service.initUser();
            service.register("Rotem", "23&As2", owner);
            service.signIn("Rotem", "23&As2");
            service.openStore("ZARA", "Rotem");
            string user = service.initUser();
            service.register("Noy", "24!tr4", user);
            service.signIn("Noy", "24!tr4");
            service.createNewProductInStore("Top", "Tank tops", "Light blue", 89, "ZARA", "Rotem");
        }

        //The store owner adds quantity of an existing product- valid procedure.
        [TestMethod]
        public void addProductInStoreTest1()
        {
            setUp();
            List<KeyValuePair<String, int>> products = new List<KeyValuePair<String, int>>();
            products.Add(new KeyValuePair<string, int>("Top",7));
            bool x = service.addProductsInStore(products, "ZARA", "Rotem");
            Assert.IsTrue(x);
        }
    }
}
