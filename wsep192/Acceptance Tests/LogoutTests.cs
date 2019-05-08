using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;

namespace Acceptance_Tests
{
    [TestClass]
    public class LogoutTests
    {
        ServiceLayer service;
        string id;

        public void setUp()
        {
            service = ServiceLayer.getInstance();
            id =service.initUser();
        }


        [TestMethod]
        public void TestMethod1_success()
        {
            setUp();
            service.register("user", "password", id);
            service.signIn("user", "password");
            Assert.AreEqual(true, service.signOut("user"));
            service.shutDown();
        }
    }
}