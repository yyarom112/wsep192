using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;
using System.Collections.Generic;

namespace IntegrationTests
{
    [TestClass]
    public class openStoreIntegration
    {
        private TradingSystem system;
        private User owner;
        private User ownerWannaBe;
        private User user;

        public void setUp()
        {
            system = new TradingSystem(null, null);
            owner = new User(202511098, "Amit", "!#2ps4R", false, true);
            ownerWannaBe = new User(209344320, "Shahaf", "poQ#40~", false, true);
            user = new User(200981476, "Guy", "@1359pOl", false, false);
            system.Users.Add(owner.Id, owner);
            system.Users.Add(ownerWannaBe.Id, ownerWannaBe);
            system.Users.Add(user.Id, user);
        }

        //Registerd user wants to open a store - valid.
        [TestMethod]
        public void openStoreTest1()
        {
            setUp();
            bool x = system.openStore("UO", owner.Id, 1);
            Assert.IsTrue(x);
        }

        //Unregisterd user wants to open a store - invalid.
        [TestMethod]
        public void openStoreTest2()
        {
            setUp();
            bool x = system.openStore("Pull&Bear", user.Id, 2);
            Assert.IsFalse(x);
        }

        //Unregisterd user wants to open a store - invalid.
        [TestMethod]
        public void openStoreTest3()
        {
            setUp();
            system.openStore("TopShop", owner.Id, 3);
            bool x = system.openStore("TopShop", ownerWannaBe.Id, 3);
            Assert.IsFalse(x);
        }
    }
}
