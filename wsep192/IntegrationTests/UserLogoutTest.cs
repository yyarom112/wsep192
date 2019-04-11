using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;

namespace IntegrationTests
{

    [TestClass]
    public class UserLogoutTest
    {

        [TestClass]
        public class ManagerPermissionTest
        {
            private TradingSystem system;
            private User user;


            public void setUp()
            {

                system = new TradingSystem(null, null);
                user = new User(1, "user", "1234" , false, true);
            }
            //Testing User class method
            [TestMethod]
            public void TestMethod_succses()
            {
                setUp();
                user.State = state.signedIn;
                Assert.AreEqual(true, user.signOut());
            }

            [TestMethod]
            public void TestMethod_failure()
            {
                setUp();
                Assert.AreEqual(false, user.signOut());
            }

            //Testing TradingSystem class method
            [TestMethod]
            public void TestMethod2_succses()
            {
                setUp();
                system.Users.Add(1, user);
                user.State = state.signedIn;
                Assert.AreEqual(true, system.signOut(1));
            }

            [TestMethod]
            public void TestMethod2_failure()
            {
                setUp();
                //user not added to the users list
                Assert.AreEqual(false, system.signOut(1));

            }


        }
    }
}
