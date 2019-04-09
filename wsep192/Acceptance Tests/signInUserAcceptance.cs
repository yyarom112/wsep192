using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;

namespace Acceptance_Tests
{
   
    [TestClass]
    public class signInUserAcceptance
    {

        ServiceLayer service;

        public void setUp()
        {
            service = new ServiceLayer();
            service.initUser("tmpuser");
        }

        [TestMethod]
        public void TestMethod1_success_scenario()
        {
            setUp();
            service.register("Seifan", "5656", "1313");
            Assert.AreEqual(true, service.signIn("Seifan", "5656", "1313"));
        }

        [TestMethod]
        public void TestMethod1_fail_password_scenario()
        {
            setUp();
            service.register("Seifan", "5656", "1313");
            Assert.AreEqual(false, service.signIn("Seifan", "4343", "1313"));
        }

        [TestMethod]
        public void TestMethod1_fail_userName_scenario()
        {
            setUp();
            service.register("Seifan", "5656", "1313");
            Assert.AreEqual(false, service.signIn("blabla", "5656", "1313"));
        }

        [TestMethod]
        public void TestMethod1_fail_userName_password_scenario()
        {
            setUp();
            service.register("Seifan", "5656", "1313");
            Assert.AreEqual(false, service.signIn("blabla", "4343", "1313"));
        }

        [TestMethod]
        public void TestMethod1_fail_user_notRegister_scenario()
        {
            setUp();
            Assert.AreEqual(false, service.signIn("Seifan", "5656", "1313"));
        }
    }
}
