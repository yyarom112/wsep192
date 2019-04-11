using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;

namespace IntegrationTests
{

    [TestClass]
    public class signInIntegration
    {
        private TradingSystem system;
        private User user1;

        public void setUp()
        {
            system = new TradingSystem(null, null);
            user1 = new User(4567, "Yuval", null, false, false);
            system.Users.Add(user1.Id, user1);
        }


        [TestMethod]
        public void TestMethod1_success_scenario()
        {
            setUp();
            String userName = user1.UserName;
            String password = "9898";
            int userId = user1.Id;
            system.register(userName, password, userId);
            Assert.AreEqual(true, system.signIn(userName, password, userId));
        }

        [TestMethod]
        public void TestMethod1_fail_password_scenario()
        {
            setUp();
            String userName = user1.UserName;
            String password = " ";
            int userId = user1.Id;
            system.register(userName, password, userId);
            Assert.AreEqual(false, system.signIn(userName, password, userId));
        }

        [TestMethod]
        public void TestMethod1_fail_userName_scenario()
        {
            setUp();
            String userName = "bla bla";
            String password = "9898";
            int userId = user1.Id;
            system.register(userName, password, userId);
            Assert.AreEqual(false, system.signIn(userName, password, userId));
        }

        [TestMethod]
        public void TestMethod1_fail_userName_password_scenario()
        {
            setUp();
            String userName = "bla bla";
            String password = "12 33";
            int userId = user1.Id;
            system.register(userName, password, userId);
            Assert.AreEqual(false, system.signIn(userName, password, userId));
        }

        [TestMethod]
        public void TestMethod1_fail_user_notRegister_scenario()
        {
            setUp();
            String userName = user1.UserName;
            String password = user1.Password;
            int userId = user1.Id;
            Assert.AreEqual(false, system.signIn(userName, password, userId));
        }
    }
}