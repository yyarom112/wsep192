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
        List<String> permissions;
        String idGuest;
        String guestUser;
        String password;

        public void setUp()
        {
            service = ServiceLayer.getInstance();
            idOwner = service.initUser();
            ownerUser = "Seifan";
            passwordUser = "2345";
            service.register(ownerUser, passwordUser, idOwner);
            service.signIn(ownerUser, passwordUser);

            idManager = service.initUser();
            managerUser = "Yuval";
            managerPassword = "2323";
            service.register(managerUser, managerPassword, idManager);
            permissions = new List<String>() { "AddDiscountPolicy" };


            service.openStore("adidas", ownerUser);

            idGuest = service.initUser();
            guestUser = "bla";
            password = "1212";
        }

        [TestMethod]
        public void TestMethod1_success_scenario()
        {
            setUp();
            Assert.AreEqual(true, service.assignManager( managerUser, "adidas", permissions, ownerUser));
            service.shutDown();
        }

        [TestMethod]
        public void TestMethod1_fail_assignByUser_scenario()
        {
            setUp();
            Assert.AreEqual(false, service.assignManager( managerUser, "adidas", permissions, guestUser));
            service.shutDown();
        }

        [TestMethod]
        public void TestMethod1_fail_assignOwnerByOwner_scenario()
        {
            setUp();
            Assert.AreEqual(false, service.assignManager( ownerUser, "adidas", permissions, ownerUser));
            service.shutDown();
        }

        [TestMethod]
        public void TestMethod1_fail_assignExistManager_scenario()
        {
            setUp();
            service.assignManager( managerUser, "adidas", permissions, ownerUser);
            Assert.AreEqual(false, service.assignManager( managerUser, "adidas", permissions, ownerUser));
            service.shutDown();
        }

        [TestMethod]
        public void TestMethod1_fail_assignNotRegisterUser_scenario()
        {
            setUp();
            Assert.AreEqual(false, service.assignManager( guestUser, "adidas", permissions, ownerUser));
            service.shutDown();
        }
    }
}
