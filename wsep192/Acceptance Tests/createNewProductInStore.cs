using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;

namespace Acceptance_Tests
{
    [TestClass]
    class createNewProductInStore
    {
        private ServiceLayer service;


        public void setUp()
        {
            service = new ServiceLayer();
            service.init("admin", "1234");
            service.initUser();
            service.openStore("ZARA","Rotem");
            //service.assignManager("Noy",)
           
        }
        //The store owner creats a new product in store - valid procedure.
        [TestMethod]
        public void createNewProductInStoreTest1()
        {
            setUp();
            bool x = service.createNewProductInStore("Top", "Tank tops", "Light blue", 89, 2, 205600191);
            Assert.IsTrue(x);
        }

        //The store manager creats a new product in store and he has premmision to do so - valid procedure.
        [TestMethod]
        public void createNewProductInStoreTest2()
        {
            setUp();
            bool x = service.createNewProductInStore("T-shirt", "Shirts", "White", 69, 2, 203114469);
            Assert.IsTrue(x);
        }

        //Not the store owner creats a new product in store - invalid procedure.
        [TestMethod]
        public void createNewProductInStoreTest3()
        {
            setUp();
            bool x = service.createNewProductInStore("Mini skirt", "Skirts", "Black", 129, 2, 201119304);
            Assert.IsFalse(x);
        }

        //Store Manager with no premission creats a new product in store - invalid procedure.
        [TestMethod]
        public void createNewProductInStoreTest4()
        {
            setUp();
            bool x = service.createNewProductInStore("Mini skirt", "Skirts", "Black", 129, 2, 201119304);
            Assert.IsFalse(x);
        }

    }
}
