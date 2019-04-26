﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;


namespace Acceptance_Tests
{
    [TestClass]
    public class createNewProductInStore
    {
        private ServiceLayer service;

        public void setUp()
        {
            service = new ServiceLayer();
            service.init("admin", "1234");
            string owner=service.initUser();
            service.register("Rotem", "23&As2", owner);
            service.signIn("Rotem", "23&As2");
            service.openStore("ZARA", "Rotem");
            string user = service.initUser();
            service.register("Noy", "24!tr4", user);
            service.signIn("Noy", "24!tr4");
        }
        //The store owner creats a new product in store - valid procedure.
        [TestMethod]
        public void createNewProductInStoreTest1()
        {
            setUp();
            bool x = service.createNewProductInStore("Top", "Tank tops", "Light blue", 89, "ZARA", "Rotem");
            Assert.IsTrue(x);
        }

        //Userr creats a new product in store and he does not has premmision to do so - invalid procedure.
        [TestMethod]
        public void createNewProductInStoreTest2()
        {
            setUp();
            bool x = service.createNewProductInStore("Top", "Tank tops", "Light blue", 89, "ZARA", "Noy");
            Assert.IsFalse(x);
        }
    }
}