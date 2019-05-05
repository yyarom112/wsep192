using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;

namespace UnitTests
{
    [TestClass]
    public class PurchasePolicyTest
    {
        private Store store;
        private User admin;
        private Role OwnerSotre;
        private Product p1;
        private ProductInStore ps1;
        private ProductConditionPolicy pcp;


        public void setup()
        {
            admin = new User(0, "admin", "1234", true, true);
            admin.State = state.signedIn;
            store = new Store(0, "store", new List<PurchasePolicy>(), null);
            OwnerSotre = new Owner(store, admin);
            store.RolesDictionary.Add(admin.Id, store.Roles.AddChild(OwnerSotre));

            p1 = new Product(1, "p1", null, null, 1);
            ps1 = new ProductInStore(100, store, p1);

            pcp = new ProductConditionPolicy(0, 1, 0, 10, LogicalConnections.and);
        }
        [TestMethod]
        public void ProductConditionPolicy_CheckCondition()
        {
            setup();
            List<KeyValuePair<ProductInStore, int>> cart = new List<KeyValuePair<ProductInStore, int>>();
            cart.Add(new KeyValuePair<ProductInStore, int>(ps1, 1));
            Assert.AreEqual(true, pcp.CheckCondition(cart));

        }
    }
}
