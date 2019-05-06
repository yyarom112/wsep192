using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;

namespace UnitTests
{
    [TestClass]
    public class Req3_2
    {
        [TestMethod]
        //Add a role to a user in the system.
        public void testAddRole()
        {
            int userID = 205111704;
            string userName = "Rotem Tetuani";
            string userPassword = "$)11ft";
            int storeID = 1;
            string storeName = "ZARA";
            List<DiscountPolicy> discountPolicies = new List<DiscountPolicy>();
            List<PurchasePolicy> purchasePolicies = new List<PurchasePolicy>();
            Store store = new Store(storeID, storeName);
            User user = new User(userID, userName, userPassword, false, false);
            Owner owner = new Owner(store, user);
            user.addRole(owner);
            Assert.AreEqual(user.Roles[storeID], owner);
        }

        [TestMethod]
        public void testInitOwner()
        {
            int userID = 203455214;
            string userName = "Keren Krispil";
            string userPassword = "(!@frt4";
            int storeID = 2;
            string storeName = "Pull&Bear";
            List<DiscountPolicy> discountPolicies = new List<DiscountPolicy>();
            List<PurchasePolicy> purchasePolicies = new List<PurchasePolicy>();
            Store store = new Store(storeID, storeName);
            User user = new User(userID, userName, userPassword, false, false);
            Role role = store.initOwner(user);
            Assert.AreEqual(role, store.RolesDictionary[userID].Data);
        }
    }
}