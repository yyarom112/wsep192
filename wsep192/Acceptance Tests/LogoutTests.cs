using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Acceptance_Tests
{
    [TestClass]
    public class LogoutTests
    {
        ServiceLayer service;

        public void setUp()
        {
            service = new ServiceLayer();
            service.initUser("tmpuser");
        }


        [TestMethod]
        public void TestMethod1_failure1()
        {
            setUp();
            service.register("user", "password", "tmpuser");
            Assert.AreEqual(false, service.signout("user"));
        }

        [TestMethod]
        public void TestMethod1_failure2()
        {
            setUp();
            service.signIn("user", "password");
            Assert.AreEqual(false, service.signout("user"));
        }

        [TestMethod]
        public void TestMethod1_failure3()
        {
            setUp();
            Assert.AreEqual(false, service.signout("user"));
        }

        [TestMethod]
        public void TestMethod1_success()
        {
            setUp();
            service.register("user","password","tmpuser");
            service.signIn("user", "password");
            Assert.AreEqual(true, service.signout("user"));
        }
    }
}
