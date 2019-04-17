using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;

namespace Acceptance_Tests
{
    [TestClass]
    public class ManagerPremissionTests
    {
        ServiceLayer service;
        string owner;
        string user;
        List<string> permissions;
        List<KeyValuePair<string, int>> list;

        public void setUp() {
            service = new ServiceLayer();
            service.init("admin","1212");
            owner = service.initUser();
            user = service.initUser();
            service.register("owner", "123", owner);
            service.register("user", "123", user);
            service.signIn("owner", "123");
            service.signIn("user", "123");
            service.openStore("store", "owner");
            
        }


        public void failure_setUp() {
            permissions = new List<string>();
            service.assignManager("user", "store", permissions, "owner");

        }

        public void success_setUp()
        {
            service.createNewProductInStore("p1", "category", "details", 100, "store", "owner");
            permissions = new List<string>();
            permissions.Add("AddProductsInStore");
            service.assignManager("user", "store", permissions, "owner");
            KeyValuePair<string, int> p1 = new KeyValuePair<string, int>("p1", 1);
            list = new List<KeyValuePair<string, int>>();
            list.Add(p1);
        }

        [TestMethod]
        public void TestMethod1_failure()
        {
            setUp();
            failure_setUp();
            Assert.AreEqual(false, service.createNewProductInStore("p1", "category", "details", 100, "store", "user"));
        }

        [TestMethod]
        public void TestMethod1_succsess()
        {
            setUp();
            success_setUp();
            Assert.AreEqual(true, service.addProductsInStore(list, "store", "user"));
        }

    }
}
