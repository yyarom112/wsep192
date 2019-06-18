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
            service = ServiceLayer.getInstance(false);
            id =service.initUser();
        }


        [TestMethod]
        public void TestMethod1_success()
        {
            setUp();
            service.register("raul", "password", id);
            service.signIn("raul", "password");
            Assert.AreEqual(true, service.signOut("raul"));
            service.shutDown();
        }
    }
}