using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;
using System.Collections.Generic;

namespace Acceptance_Tests
{
    [TestClass]
    public class RemoveProductinStore
    {

        ServiceLayer service;
        private List<KeyValuePair<String, int>> products;
        private List<KeyValuePair<String, int>> toManyProducts;
        private List<KeyValuePair<String, int>> notExistProduct;
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

        //The owner will remove product quanitity if he has enough of it - valid procedure.
        [TestMethod]
        public void removeProductInStoreTest1()
        {
            setUp();
            bool x=service.removeProductsInStore(products, "ZARA", "Rotem");
            Assert.IsTrue(x);
            service.shutDown();
        }

        //The owner will remove product quanitity if he does not has enough of it - invalid procedure.
        [TestMethod]
        public void removeProductInStoreTest2()
        {
            setUp();
            toManyProducts = new List<KeyValuePair<String, int>>();
            toManyProducts.Add(new KeyValuePair<string, int>("Top", 30));
            bool x = service.removeProductsInStore(toManyProducts,"ZARA","Rotem");
            Assert.IsFalse(x);
            service.shutDown();
        }

        //The owner will remove product who does not exists on stote - invalid procedure.
        [TestMethod]
        public void removeProductInStoreTest3()
        {
            setUp();
            notExistProduct = new List<KeyValuePair<String, int>>();
            notExistProduct.Add(new KeyValuePair<string, int>("Jeans", 30));
            bool x = service.removeProductsInStore(notExistProduct, "ZARA", "Rotem");
            Assert.IsFalse(x);
            service.shutDown();
        }
    }
}
