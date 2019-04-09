using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;

namespace Acceptance_Tests
{
    [TestClass]
    public class registerUserAcceptance
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
            Assert.AreEqual(true, service.register("Seifan", "2356", "1414"));
        }

        [TestMethod]
        public void TestMethod1_fail_password_scenario()
        {
            setUp();
            Assert.AreEqual(false, service.register("Seifan", " ", "1414"));
        }

        [TestMethod]
        public void TestMethod1_fail_userName_scenario()
        {
            setUp();
            Assert.AreEqual(false, service.register("blabla", "2356", "1414"));
        }

        [TestMethod]
        public void TestMethod1_fail_userName_password_scenario()
        {
            setUp();
            Assert.AreEqual(false, service.register("blabla", "6868", "1414"));
        }
    }
}
