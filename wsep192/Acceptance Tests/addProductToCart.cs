using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;

namespace Acceptance_Tests
{
    [TestClass]
    public class addProductToCart
    {
        ServiceLayer service;

        public void setUp()
        {
            service = new ServiceLayer();
            service.initUser("tmpuser");
            service.initUser("managerStore");
            service.openStore("blabla", "managerStore");
            List<KeyValuePair<string, int>> products = new List<KeyValuePair<string, int>>();
            products.Add(new KeyValuePair<string, int>("milk", 10));
            service.addProductsInStore(products, "blabla", "managerStore");

        }

        public void StoreProductsInCart()
        {
            setUp();
            
        }

    }
}
