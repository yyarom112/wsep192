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
        public void TestMethod1_success_user_scenario()
        {
            setUp();
            String userName = user1.UserName;
            String password = user1.Password;
            Assert.AreEqual(true, user1.signIn(userName, password));
        }

        [TestMethod]
        public void TestMethod1_fail_user_scenario()
        {
            setUp();
            String userName = user1.UserName;
            String password = null;
            user1.IsRegistered = true;
            Assert.AreEqual(false, user1.signIn(userName, password));
        }

        /*[TestMethod]
        public void TestMethod1_success_scenario()
        {
            setUp();
            StubUser tmpUser = new StubUser(123, "yuval", "4567", false, false, true);
            String userName = tmpUser.UserName;
            String password = tmpUser.Password;
            int userId = tmpUser.Id;
            system.Users.Add(tmpUser.Id, tmpUser);
            //system.register(userName, password, userId);
            tmpUser.IsRegistered = true;
            Assert.AreEqual(true, system.signIn(userName, password, userId));
        }*/

        [TestMethod]
        public void TestMethod1_fail_password_scenario()
        {
            setUp();
            StubUser tmpUser = new StubUser(123, "yuval", "4567", false, false, true);
            String userName = tmpUser.UserName;
            String password = "1111";
            int userId = tmpUser.Id;
            system.Users.Add(tmpUser.Id, tmpUser);
            system.register(userName, password, userId);
            Assert.AreEqual(false, system.signIn(userName, password, userId));
        }

        [TestMethod]
        public void TestMethod1_fail_userName_scenario()
        {
            setUp();
            StubUser tmpUser = new StubUser(123, "yuval", "4567", false, false, true);
            String userName = "blabla";
            String password = tmpUser.Password;
            int userId = tmpUser.Id;
            system.Users.Add(tmpUser.Id, tmpUser);
            system.register(userName, password, userId);
            Assert.AreEqual(false, system.signIn(userName, password, userId));
        }

        [TestMethod]
        public void TestMethod1_fail_userName_password_scenario()
        {
            setUp();
            StubUser tmpUser = new StubUser(123, "yuval", "4567", false, false, true);
            String userName = "blabla";
            String password = "7777";
            int userId = tmpUser.Id;
            system.Users.Add(tmpUser.Id, tmpUser);
            system.register(userName, password, userId);
            Assert.AreEqual(false, system.signIn(userName, password, userId));
        }

        [TestMethod]
        public void TestMethod1_fail_user_notRegister_scenario()
        {
            setUp();
            StubUser tmpUser = new StubUser(123, "yuval", "4567", false, false, true);
            String userName = tmpUser.UserName;
            String password = tmpUser.Password;
            int userId = tmpUser.Id;
            system.Users.Add(tmpUser.Id, tmpUser);
            Assert.AreEqual(false, system.signIn(userName, password, userId));
        }

        /*------------------------stub-classes------------------------------------*/

        class StubUser : User
        {
            bool retVal;
            public StubUser(int id, string userName, string password, bool isAdmin, bool isRegistered, bool ret) : base(id, userName, password, isAdmin, isRegistered)
            {
                this.retVal = ret;
            }

            public override bool register(string userName, string password)
            {
                return retVal;
            }

            public override bool signIn(string userName, string password)
            {
                return retVal;
            }
        }
    }
}
