using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;
using src.Domain.Dataclass;

namespace UnitTests
{
    [TestClass]
    public class PurchasePolicyTest
    {
        private Store store;
        private User admin;
        private Role OwnerSotre;
        private Product p1;
        private Product p2;
        private ProductInStore ps1;
        private ProductInStore ps2;
        private ProductConditionPolicy pcp;
        private inventoryConditionPolicy icp;
        private BuyConditionPolicy bcp;
        private UserConditionPolicy ucp;


        public void setup()
        {
            admin = new User(0, "admin", "1234", true, true);
            admin.State = state.signedIn;
            store = new Store(0, "store", new List<PurchasePolicy>(), null);
            OwnerSotre = new Owner(store, admin);
            store.RolesDictionary.Add(admin.Id, store.Roles.AddChild(OwnerSotre));
            p1 = new Product(1, "p1", null, null, 1);
            ps1 = new ProductInStore(10, store, p1);
            p2 = new Product(2, "p2", null, null, 10);
            ps2 = new ProductInStore(10, store, p2);
            pcp = new ProductConditionPolicy(0, 1, 0, 10, LogicalConnections.and);
            icp = new inventoryConditionPolicy(1, 1, 5, LogicalConnections.and);
            bcp = new BuyConditionPolicy(2, 2, 5, 10, 20);
            ucp = new UserConditionPolicy("Tel Aviv", true);
        }
        [TestMethod]
        public void ProductConditionPolicy_CheckCondition()
        {
            setup();
            List<KeyValuePair<ProductInStore, int>> cart = new List<KeyValuePair<ProductInStore, int>>();
            cart.Add(new KeyValuePair<ProductInStore, int>(ps2, 1));
            Assert.AreEqual(true, pcp.CheckCondition(cart,null));
            cart = new List<KeyValuePair<ProductInStore, int>>();
            cart.Add(new KeyValuePair<ProductInStore, int>(ps1, 1));
            Assert.AreEqual(true, pcp.CheckCondition(cart, null));
            cart = new List<KeyValuePair<ProductInStore, int>>();
            cart.Add(new KeyValuePair<ProductInStore, int>(ps1, -1));
            Assert.AreEqual(false, pcp.CheckCondition(cart, null));
            cart = new List<KeyValuePair<ProductInStore, int>>();
            cart.Add(new KeyValuePair<ProductInStore, int>(ps1, 11));
            Assert.AreEqual(false, pcp.CheckCondition(cart, null));
        }

        [TestMethod]
        public void inventoryConditionPolicy_CheckCondition()
        {
            setup();
            List<KeyValuePair<ProductInStore, int>> cart = new List<KeyValuePair<ProductInStore, int>>();
            cart.Add(new KeyValuePair<ProductInStore, int>(ps2, 1));
            Assert.AreEqual(true, pcp.CheckCondition(cart, null));
            cart = new List<KeyValuePair<ProductInStore, int>>();
            cart.Add(new KeyValuePair<ProductInStore, int>(ps1, 5));
            Assert.AreEqual(true, icp.CheckCondition(cart, null));
            cart = new List<KeyValuePair<ProductInStore, int>>();
            cart.Add(new KeyValuePair<ProductInStore, int>(ps1, 6));
            Assert.AreEqual(false, icp.CheckCondition(cart, null));

        }

        [TestMethod]
        public void BuyConditionPolicy_CheckCondition()
        {
            setup();
            List<KeyValuePair<ProductInStore, int>> cart = new List<KeyValuePair<ProductInStore, int>>();
            cart.Add(new KeyValuePair<ProductInStore, int>(ps1, 1));
            Assert.AreEqual(false, bcp.CheckCondition(cart, null)); // Too few products

            cart = new List<KeyValuePair<ProductInStore, int>>();
            cart.Add(new KeyValuePair<ProductInStore, int>(ps1, 6));
            Assert.AreEqual(false, bcp.CheckCondition(cart, null)); // Too much products

            cart = new List<KeyValuePair<ProductInStore, int>>();
            cart.Add(new KeyValuePair<ProductInStore, int>(ps1, 2));
            Assert.AreEqual(false, bcp.CheckCondition(cart, null)); // Too cheap

            cart = new List<KeyValuePair<ProductInStore, int>>();
            cart.Add(new KeyValuePair<ProductInStore, int>(ps2, 3));
            Assert.AreEqual(false, bcp.CheckCondition(cart, null)); // too expensive

            cart = new List<KeyValuePair<ProductInStore, int>>();
            cart.Add(new KeyValuePair<ProductInStore, int>(ps1, 2));
            cart.Add(new KeyValuePair<ProductInStore, int>(ps2, 1));
            Assert.AreEqual(true, bcp.CheckCondition(cart, null)); // proper



        }

        [TestMethod]
        public void UserConditionPolicy_CheckCondition()
        {
            setup();
            UserDetailes user = new UserDetailes("", false);
            Assert.AreEqual(false, ucp.CheckCondition(null, user));

            user.Isregister = true;
            Assert.AreEqual(false, ucp.CheckCondition(null, user));

            user.Isregister = false;
            user.Adress = "Tel Aviv";
            Assert.AreEqual(false, ucp.CheckCondition(null, user));

            user.Isregister = true;
            Assert.AreEqual(true, ucp.CheckCondition(null, user));

        }


    }
}
