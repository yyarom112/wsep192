using System;
using src.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class registerUser
    {
        private TradingSystem system;
        private User user1;
        private User user2;
        private User user3;

        public void setUp()
        {
            system = new TradingSystem(null, null);
            user1 = new User(1234, "Seifan", "2457", "Ashdod", false, false);
            user2 = new User(4567, "Aviv", "1357", "Beer-Sheva", false, false);
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
    }
}
