using System;
using src.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class registerUserTest
    {
        private TradingSystem system;
        private User user1;
        /*
        public void setUp()
        {
            system = new TradingSystem(null,null);
            user1 = new User(1234, "Seifan", "2457", false, false);
            system.Users.Add(user1.Id, user1);
        }

        [TestMethod]
        public void TestMethod1_success_scenario()
        {
            setUp();
            String userName = user1.UserName;
            String password = user1.Password;
            int userId = user1.Id;
            Assert.AreEqual(true, system.register(userName, password, userId.ToString()));
        }

        [TestMethod]
        public void TestMethod1_fail_password_scenario()
        {
            setUp();
            String userName = user1.UserName;
            String password = " ";
            int userId = user1.Id;
            Assert.AreEqual(false, system.register(userName, password, userId.ToString()));
        }

        [TestMethod]
        public void TestMethod1_fail_userName_scenario()
        {
            setUp();
            String userName = "blabla";
            String password = user1.Password;
            int userId = user1.Id;
            Assert.AreEqual(false, system.register(userName, password, userId.ToString()));
        }

        [TestMethod]
        public void TestMethod1_fail_userName_password_scenario()
        {
            setUp();
            String userName = "blabla";
            String password = "7777";
            int userId = user1.Id;
            Assert.AreEqual(false, system.register(userName, password, userId.ToString()));
        }
*/
    }
}
