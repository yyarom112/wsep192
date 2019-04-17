using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;
using System.Collections.Generic;

namespace IntegrationTests
{
    [TestClass]
    public class removeUserIntegration
    {
        private TradingSystem system;
        private User admin;
        private User notAdmin;
        private User toRemove;
        public void setUp()
        {
            system = new TradingSystem(null, null);
            admin = new User(201499712, "ShakedFerera", "384!@wsaQ", true, false);
            notAdmin = new User(201453992, "Lidoy", "21~U78op", false, false);
            toRemove = new User(203444912, "Astma", "46uW@1", false, false);
            system.Users.Add(admin.Id, admin);
            system.Users.Add(notAdmin.Id, notAdmin);
            system.Users.Add(toRemove.Id, toRemove);
        }
        [TestMethod]
        //The admin removes a user from the system-valid.
        public void removeUserTest1()
        {
            setUp();
            bool x= system.removeUser(admin.Id, toRemove.Id);
            Assert.IsTrue(x);
        }

        [TestMethod]
        //The admin removes a user from the system-valid.
        public void removeUserTest2()
        {
            setUp();
            bool x = system.removeUser(notAdmin.Id, toRemove.Id);
            Assert.IsFalse(x);
        }
    }
}
