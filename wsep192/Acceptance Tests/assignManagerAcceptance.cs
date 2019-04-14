using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;

namespace Acceptance_Tests
{
    [TestClass]
    public class assignManagerAcceptance
    {
        ServiceLayer service;
        String ownerUser;
        String passwordUser;
        String managerUser;
        String managerPassword;
        List<String> permision;
        String user1;
        String password;

        public void setUp()
        {
            service = new ServiceLayer();
            service.init("Admin", "2323");
            service.initUser("tmpuser1");
            service.initUser("tmpuser2");
            service.initUser("tmpuser3");
            ownerUser = "Seifan";
            passwordUser = "2345";
            service.register(ownerUser, passwordUser, "tmpuser1");
            service.signIn(ownerUser, passwordUser);

            managerUser = "Yuval";
            managerPassword = "2323";
            service.register(managerUser, managerPassword, "tmpuser2");
            service.signIn(managerUser, managerPassword);
            permision = new List<String>() {"AddDiscountPolicy"};

            service.openStore("adidas", ownerUser);
           
            user1 = "bla";
            password = "1212";
            service.register(user1, password,"tmpuser3");

        }

        [TestMethod]
        public void TestMethod1_success_scenario()
        {
            setUp();
            Assert.AreEqual(true, service.assignManager(ownerUser, managerUser, "store", permision));
        }

        [TestMethod]
        public void TestMethod1_fail_assignByUser_scenario()
        {
            setUp();
            Assert.AreEqual(true, service.assignManager(user1, managerUser, "store", permision));
        }

        [TestMethod]
        public void TestMethod1_fail_assignOwnerByOwner_scenario()
        {
            setUp();
            Assert.AreEqual(true, service.assignManager(ownerUser, ownerUser, "store", permision));
        }

        [TestMethod]
        public void TestMethod1_fail_assignExistManager_scenario()
        {
            setUp();
            service.assignManager(ownerUser, managerUser, "store", permision);
            Assert.AreEqual(true, service.assignManager(ownerUser, managerUser, "store", permision));
        }
    }
}
