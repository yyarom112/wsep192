using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;

namespace IntegrationTests
{

    [TestClass]
    public class registerUserIntegration
    {
        TradingSystem system;
        User user1;

        public void setUp()
        {
            system = new TradingSystem(null, null);
            user1 = new User(1234, "Seifan", null, false, false);
            system.Users.Add(user1.Id, user1);
        }

        [TestMethod]
        public void TestMethod1_success_scenario()
        {
            setUp();
            String userName = user1.UserName;
            String password = "9898";
            int userId = user1.Id;
            user1.db.IsTest = true;
            Assert.AreEqual(true, system.register(userName, password, userId));
        }

        [TestMethod]
        public void TestMethod1_fail_password_scenario()
        {
            setUp();
            String userName = user1.UserName;
            String password = " ";
            int userId = user1.Id;
            Assert.AreEqual(false, system.register(userName, password, userId));
        }

        [TestMethod]
        public void TestMethod1_fail_userName_scenario()
        {
            setUp();
            String userName = "bla bla";
            String password = "9898";
            int userId = user1.Id;
            Assert.AreEqual(false, system.register(userName, password, userId));
        }

        [TestMethod]
        public void TestMethod1_fail_userName_password_scenario()
        {
            setUp();
            String userName = "bla bla";
            String password = "99 99";
            int userId = user1.Id;
            Assert.AreEqual(false, system.register(userName, password, userId));
        }
    }
}