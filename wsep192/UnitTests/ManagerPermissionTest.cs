using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;

namespace UnitTests
{
    [TestClass]
    public class ManagerPermissionTest
    {
        private TradingSystem system;
        private User user;
        private Store store;
        private Manager manager;

        public void setUp()
        {
            system = new TradingSystem(null, null);
            user = new User(1,"user","1234",null,state.signedIn,false,true);
            store = new Store(1, "store", 0, new List<PurchasePolicy>(), new List<DiscountPolicy>());
            manager = new Manager(store,user,new List<int>());
            
        }

        [TestMethod]
        public void TestMethod()
        {
            setUp();
            Assert.AreEqual(false, manager.validatePermission(1));
            List<int> permission = new List<int>();
            permission.Add(1);
            manager = new Manager(store, user, permission);
            Assert.AreEqual(true, manager.validatePermission(1));
            permission.Add(2);
            manager = new Manager(store, user, permission);
            Assert.AreEqual(true, manager.validatePermission(2));
            Assert.AreEqual(true, manager.validatePermission(1));
            Assert.AreEqual(false, manager.validatePermission(3));
        }

 

        
    }
}
