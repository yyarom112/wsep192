using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;
using System.Collections.Generic;

namespace Acceptance_Tests
{
    [TestClass]
    public class assignOwner
    {
        private ServiceLayer service;
        private string owner;
        private string user;
        private string manager;

        public void setUp()
        {
            service = new ServiceLayer();
            service.init("admin", "1234");
            owner = service.initUser();
            service.register("Rotem", "23&As2", owner);
            service.signIn("Rotem", "23&As2");
            service.openStore("ZARA", "Rotem");
            user = service.initUser();
            service.register("Keren", "63aQr!2", user);
            service.signIn("Keren", "63aQr!2");
        }

        [TestMethod]
        //The owner assigning a not manager user to be an owner - valid
        public void assignOwnerTest1()
        {
            setUp();
            bool x = service.assignOwner("Rotem", "Keren", "ZARA");
            Assert.IsTrue(x);
        }

        [TestMethod]
        //The owner assigning a manager to be an owner - invalid.
        public void assignOwnerTest2()
        {
            setUp();
            manager = service.initUser();
            service.register("Noy", "24!tr4", manager);
            service.signIn("Noy", "24!tr4");
            List<string> permissions = new List<String>() { "AddDiscountPolicy" };
            service.assignManager("Noy", "ZARA", permissions, "Rotem");
            bool x = service.assignOwner("Rotem", "Noy", "ZARA");
            Assert.IsFalse(x);

        }
    }
}

