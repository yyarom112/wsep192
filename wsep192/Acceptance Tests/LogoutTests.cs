using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;

namespace Acceptance_Tests
{
    [TestClass]
    public class LogoutTests
    {
        ServiceLayer service;

        public void setUp()
        {
            service = new ServiceLayer();
            //service.init();
            service.initUser("tmpuser");
        }

        
        [TestMethod]
        public void TestMethod1_success()
        {
            setUp();
            service.register("user","password","tmpuser");
            service.signIn("user", "password","tmpuser");
            Assert.AreEqual(true, service.signOut("user"));
        }
    }
}
