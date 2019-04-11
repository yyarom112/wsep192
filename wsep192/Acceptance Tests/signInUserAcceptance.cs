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
            service.init("Admin","2323");
            service.initUser("tmpuser");
        }

        [TestMethod]
        public void TestMethod1_success_scenario()
        {
            setUp();
            String userName = "Seifan";
            String password = "2345";
            service.register(userName, password, "tmpuser");
            Assert.AreEqual(true, service.signIn(userName, password));
        }

        [TestMethod]
        public void TestMethod1_fail_password_scenario()
        {
            setUp();
            String userName = "Seifan";
            String password = " ";
            service.register(userName, password, "tmpuser");
            Assert.AreEqual(false, service.signIn(userName, password));
        }

        [TestMethod]
        public void TestMethod1_fail_userName_scenario()
        {
            setUp();
            String userName = "bla bla";
            String password = "2345";
            service.register(userName, password, "tmpuser");
            Assert.AreEqual(false, service.signIn(userName, password));
        }

        [TestMethod]
        public void TestMethod1_fail_userName_password_scenario()
        {
            setUp();
            String userName = "bla bla";
            String password = " ";
            service.register(userName, password, "tmpuser");
            Assert.AreEqual(false, service.signIn(userName, password));
        }

        [TestMethod]
        public void TestMethod1_fail_user_notRegister_scenario()
        {
            setUp();
            String userName = "Seifan";
            String password = "2345";
            Assert.AreEqual(false, service.signIn(userName, password));
        }
    }
}