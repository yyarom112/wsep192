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
        String guestUser;
        String password;

        public void setUp()
        {
            service = new ServiceLayer();
            service.init("Admin", "2323");
            service.initUser();
            service.initUser();
            service.initUser();
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

            guestUser = "bla";
            password = "1212";
            service.register(guestUser, password,"tmpuser3");

        }

        [TestMethod]
        public void TestMethod1_success_scenario()
        {
            setUp();
            Assert.AreEqual(true, service.assignManager(ownerUser, managerUser, "adidas", permision));
        }

        [TestMethod]
        public void TestMethod1_fail_assignByUser_scenario()
        {
            setUp();
            Assert.AreEqual(false, service.assignManager(guestUser, managerUser, "adidas", permision));
        }

        [TestMethod]
        public void TestMethod1_fail_assignOwnerByOwner_scenario()
        {
            setUp();
            Assert.AreEqual(false, service.assignManager(ownerUser, ownerUser, "adidas", permision));
        }

        [TestMethod]
        public void TestMethod1_fail_assignExistManager_scenario()
        {
            setUp();
            service.assignManager(ownerUser, managerUser, "store", permision);
            Assert.AreEqual(false, service.assignManager(ownerUser, managerUser, "adidas", permision));
        }

        [TestMethod]
        public void TestMethod1_fail_assignNotRegisterUser_scenario()
        {
            setUp();
            Assert.AreEqual(false, service.assignManager(ownerUser, guestUser, "adidas", permision));
        }
    }
}
