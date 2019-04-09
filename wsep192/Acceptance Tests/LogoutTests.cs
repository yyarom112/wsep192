using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;

namespace Acceptance_Tests
{
    [TestClass]
    public class LogoutTests
    {
        ServiceLayer service;
        [TestMethod]
        public void setUp()
        {
            service = new ServiceLayer();
            service.initUser("tmpuser");
            service.register("user", "password", "tmpuser");
            service.signIn("user", "password", "tmpuser");
        }


   
       // [TestMethod]
        public void TestMethod1_success()
        {
            setUp();
            Assert.AreEqual(true, service.signOut("user"));
        }
    }
}
