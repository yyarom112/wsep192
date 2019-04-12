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
            service.openStore("store", owner);
            
        }


        public void failure_setUp() {
            permissions = new List<string>();
            service.assignManager("user", "owner", "store", permissions);
            string tmp_user = service.initUser();
            service.register("tmp", "123", tmp_user);
        }

        public void success_setUp()
        {
            permissions = new List<string>();
            permissions.Add("AddProductToStore");
            service.assignManager("user", "owner", "store", permissions);
            KeyValuePair<string, int> p1 = new KeyValuePair<string, int>("p1", 1);
            list = new List<KeyValuePair<string, int>>();
            list.Add(p1);
        }

        [TestMethod]
        public void TestMethod1_failure()
        {
            setUp();
            failure_setUp();
            Assert.AreEqual(false,service.assignManager("tmp","user","store",permissions));
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
