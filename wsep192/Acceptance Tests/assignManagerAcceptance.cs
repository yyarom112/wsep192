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
        String idOwner;
        String ownerUser;
        String passwordUser;
        String idManager;
        String managerUser;
        String managerPassword;
        List<String> permision;
        String idGuest;
        String guestUser;
        String password;

        public void setUp()
        {
            service = new ServiceLayer();
            service.init("Admin", "2323");
            idOwner = service.initUser();
            ownerUser = "Seifan";
            passwordUser = "2345";
            service.register(ownerUser, passwordUser, idOwner);
            service.signIn(ownerUser, passwordUser);

            idManager = service.initUser();
            managerUser = "Yuval";
            managerPassword = "2323";
            service.register(managerUser, managerPassword, idManager);
            service.signIn(managerUser, managerPassword);
            permision = new List<String>() {"AddDiscountPolicy"};


            service.openStore("adidas", ownerUser);

            idGuest = service.initUser();
            guestUser = "bla";
            password = "1212";
            service.register(guestUser, password, idGuest);
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
            service.assignManager(ownerUser, managerUser, "adidas", permision);
            Assert.AreEqual(false, service.assignManager(ownerUser, managerUser, "adidas", permision));
        }

        [TestMethod]
        public void TestMethod1_fail_assignNotSignedInUser_scenario()
        {
            setUp();
            Assert.AreEqual(false, service.assignManager(ownerUser, guestUser, "adidas", permision));
        }
    }
}
