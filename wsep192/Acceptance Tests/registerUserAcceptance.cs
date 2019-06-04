using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;
using src.DataLayer;

namespace Acceptance_Tests
{
    [TestClass]
    public class registerUserAcceptance
    {
        ServiceLayer service;
        string id;

        public void setUp()
        {
            service = ServiceLayer.getInstance();
            id = service.initUser();

        }

        [TestMethod]
        public void TestMethod1_success_scenario()
        {
            setUp();
            String userName = "Seifan";
            String password = "2345";
            DBmanager db = new DBmanager();
            db.removeUser(3);
            Assert.AreEqual(true, service.register(userName, password, id));
            service.shutDown();
        }

        [TestMethod]
        public void TestMethod1_fail_password_scenario()
        {
            setUp();
            String userName = "Seifan";
            String password = " ";
            Assert.AreEqual(false, service.register(userName, password, id));
            service.shutDown();
        }

        [TestMethod]
        public void TestMethod1_fail_userName_scenario()
        {
            setUp();
            String userName = "bla bla";
            String password = "2345";
            Assert.AreEqual(false, service.register(userName, password, id));
            service.shutDown();
        }

        [TestMethod]
        public void TestMethod1_fail_userName_password_scenario()
        {
            setUp();
            String userName = "blabla";
            String password = " ";
            Assert.AreEqual(false, service.register(userName, password, id));
            service.shutDown();
        }
    }
}