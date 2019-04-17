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
            service = new ServiceLayer();
            service.init("Admin", "2323");
            idOwner = service.initUser();
            ownerUser = "Seifan";
            passwordUser = "2345";
            service.register(ownerUser, passwordUser, idOwner);
            service.signIn(ownerUser, passwordUser);
        }

        [TestMethod]
        //The admin removes an existing user - valid
        public void removeUserTest1()
        {
            setUp();
            bool x=service.removeUser("2323", "2345");
            Assert.IsTrue(x);
        }

        [TestMethod]
        //Not the admin removes an existing user - invalid
        public void removeUserTest2()
        {
            setUp();
            bool x = service.removeUser("2323", "2345");
            Assert.IsFalse(x);
        }

        //The admin removes a non existing user - invalid
        public void removeUserTest3()
        {
            setUp();
            bool x = service.removeUser("2323", "2345");
            Assert.IsFalse(x);
        }
    }
}
