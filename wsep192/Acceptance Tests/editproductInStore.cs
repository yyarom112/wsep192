using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;
using System.Collections.Generic;

namespace Acceptance_Tests
{
    [TestClass]
    public class editproductInStore
    {

        ServiceLayer service;
        private List<KeyValuePair<String, int>> products;
        public void setUp()
        {
            service = ServiceLayer.getInstance();
            string owner = service.initUser();
            service.register("Rotem", "23&As2", owner);
            service.signIn("Rotem", "23&As2");
            service.openStore("ZARA", "Rotem");
            string user = service.initUser();
            service.register("Noy", "24!tr4", user);
            service.signIn("Noy", "24!tr4");
            service.createNewProductInStore("Top", "Tank tops", "Light blue", 89, "ZARA", "Rotem");
            products = new List<KeyValuePair<String, int>>();
            products.Add(new KeyValuePair<string, int>("Top", 7));
            service.addProductsInStore(products, "ZARA", "Rotem");
        }

        //The owner will edit a product who does exists in store - valid procedure.
        [TestMethod]
        public void editProductInStoreTest1()
        {
            setUp();
            bool x = service.editProductInStore("Top","Top", "Tank tops", "Light blue", 90, "ZARA", "Rotem");
            Assert.IsTrue(x);
            service.shutDown();
        }

        //The owner will edit a product who does not exists in store - invalid procedure.
        [TestMethod]
        public void editProductInStoreTest2()
        {
            setUp();
            bool x = service.editProductInStore("Skinnt jeans","Skinnt jeans", "Jeans", "Light blue", 179, "ZARA", "Rotem");
            Assert.IsFalse(x);
            service.shutDown();
        }

        //Not the owner will remove product who does exists on stote - invalid procedure.
        [TestMethod]
        public void editProductInStoreTest3()
        {
            setUp();
            bool x = service.editProductInStore("Top", "Top", "Tank tops", "Light blue", 90, "ZARA", "Noy");
            Assert.IsFalse(x);
            service.shutDown();
        }
    }
}
