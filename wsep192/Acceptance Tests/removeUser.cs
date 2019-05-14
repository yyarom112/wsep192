using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;

namespace Acceptance_Tests
{
    [TestClass]
    public class removeUser
    {
        ServiceLayer service;
        String idOwner;
        String ownerUser;
        String passwordUser;

        public void setUp()
        {
            service = ServiceLayer.getInstance();
            idOwner = service.initUser();
            ownerUser = "Rotem";
            passwordUser = "2eRt58!@";
            service.register(ownerUser, passwordUser, idOwner);
            service.signIn(ownerUser, passwordUser);
        }

        [TestMethod]
        //The admin removes an existing user - valid
        public void removeUserTest1()
        {
            setUp();
            bool x = service.removeUser("Rotem","admin");
            Assert.IsTrue(x);
            service.shutDown();
        }

        [TestMethod]
        //Not the admin removes an existing user - invalid
        public void removeUserTest2()
        {
            setUp();
            bool x = service.removeUser("Admin", "Rotem");
            Assert.IsFalse(x);
            service.shutDown();
        }

        //The admin removes a non existing user - invalid
        public void removeUserTest3()
        {
            setUp();
            bool x = service.removeUser("Lior", "Admin");
            Assert.IsFalse(x);
            service.shutDown();
        }
    }
}
