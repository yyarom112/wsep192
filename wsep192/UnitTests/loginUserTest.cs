using System;
using src.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{

    [TestClass]
    public class loginUserTest
    {
        private TradingSystem system;
        private User user1;

        public void setUp()
        {
            system = new TradingSystem(null, null);
            user1 = new User(4567, "Yuval", "3399", false, false);
            system.Users.Add(user1.Id, user1);
        }

        [TestMethod]
        public void TestMethod1_success_scenario()
        {
            setUp();
            String userName = user1.UserName;
            String password = user1.Password;
            int userId = user1.Id;
            system.register(userName, password, userId.ToString());
            system.signIn(userName, password, userId.ToString());
            Assert.AreEqual(true, system.signIn(userName, password, userId.ToString()));

        }

        [TestMethod]
        public void TestMethod1_fail_password_scenario()
        {
            setUp();
            String userName = user1.UserName;
            String password = "1111";
            int userId = user1.Id;
            system.register(userName, password, userId.ToString());
            Assert.AreEqual(false, system.signIn(userName, password, userId.ToString()));
        }

        [TestMethod]
        public void TestMethod1_fail_userName_scenario()
        {
            setUp();
            String userName = "blabla";
            String password = user1.Password;
            int userId = user1.Id;
            system.register(userName, password, userId.ToString());
            Assert.AreEqual(false, system.signIn(userName, password, userId.ToString()));
        }

        [TestMethod]
        public void TestMethod1_fail_userName_password_scenario()
        {
            setUp();
            String userName = "blabla";
            String password = "7777";
            int userId = user1.Id;
            system.register(userName, password, userId.ToString());
            Assert.AreEqual(false, system.signIn(userName, password, userId.ToString()));
        }

        [TestMethod]
        public void TestMethod1_fail_user_notRegister_scenario()
        {
            setUp();
            String userName = user1.UserName;
            String password = user1.Password;
            int userId = user1.Id;
            Assert.AreEqual(false, system.signIn(userName, password, userId.ToString()));
        }
    }
}
