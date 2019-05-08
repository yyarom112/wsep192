using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;

namespace Acceptance_Tests
{
    [TestClass]
    public class RemoveManager_Req46
    {
        ServiceLayer service;

        public void setUp()
        {
            service = ServiceLayer.getInstance();
            service.register("user", "1234", service.initUser());
            service.signIn("admin", "admin");
            service.openStore("store", "admin");
            service.createNewProductInStore("p1", "", "", 10, "store", "admin");
            List<KeyValuePair<string, int>> toInsert = new List<KeyValuePair<string, int>>();
            toInsert.Add(new KeyValuePair<string, int>("p1", 100));
            service.addProductsInStore(toInsert, "store", "admin");

            service.register("manager", "1234", service.initUser());
            service.assignManager("manager", "store", new List<string>(), "admin");


        }

        //A store owner subscription removes a store manager subscription
        [TestMethod]
        public void TestMethod_StoreOwnerSubscriptionRemovesStoreManagerSubscription()
        {
            setUp();
            Assert.AreEqual(true, service.removeManager("manager","store","admin"));
            service.shutDown();
        }

        //A store owner is trying to unsubscribe a subscription that is not a store manager
        [TestMethod]
        public void TestMethod_StoreOwnerTryingUnsubscribeSubscriptionThatIsNotStoreManager()
        {
            setUp();
            Assert.AreEqual(false, service.removeManager("user", "store", "admin"));
            service.shutDown();
        }

        //A non-store owner is trying to unsubscribe from another store manager subscription
        [TestMethod]
        public void TestMethod_NonStoreOwnerTryingUnsubscribeFromAnotherStoreManagerSubscription()
        {
            setUp();
            Assert.AreEqual(false, service.removeManager("manager", "store", "user"));
            service.shutDown();
        }
    }
}
