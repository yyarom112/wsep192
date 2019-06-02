using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;

namespace UnitTests
{

    [TestClass]
    public class removePurchasePolicy_UnitTest
    {
        private TradingSystem system;
        private User admin;
        private User ownerUser;
        private ShoppingBasket basket_admin;

        private Owner ownerRole;
        private Store store;

        private Product p1;
        private Product p2;
        private Product p3;
        private Product p4;
        private ProductInStore pis1;
        private ProductInStore pis2;
        private ProductInStore pis3;
        private ProductInStore pis4;

        ProductConditionPolicy pcp;

        Dictionary<int, KeyValuePair<ProductInStore, int>> products;

        public void setUp()
        {
            store = new Store(1111, "adidas");

            admin = new User(0, "admin", "1234", true, true);
            basket_admin = admin.Basket;
            ownerUser = new User(1234, "Seifan", "2457", false, false);
            ownerUser.register(ownerUser.UserName, ownerUser.Password);
            ownerUser.signIn(ownerUser.UserName, ownerUser.Password);
            ownerRole = new Owner(store, ownerUser);
            ownerUser.Roles.Add(store.Id, ownerRole);


            p1 = new Product(0, "first", "", "", 100);
            p2 = new Product(1, "second", "", "", 50);
            p3 = new Product(2, "third", "", "", 200);
            p4 = new Product(3, "fourth", "", "", 300);
            pis1 = new ProductInStore(20, store, p1);
            pis2 = new ProductInStore(20, store, p2);
            pis3 = new ProductInStore(20, store, p3);
            pis4 = new ProductInStore(20, store, p4);
            store.Products.Add(p1.Id, pis1);
            store.Products.Add(p2.Id, pis2);
            store.Products.Add(p3.Id, pis3);
            store.Products.Add(p4.Id, pis4);

            products = new Dictionary<int, KeyValuePair<ProductInStore, int>>();
            products.Add(p1.Id, new KeyValuePair<ProductInStore, int>(pis1, 2));
            products.Add(p2.Id, new KeyValuePair<ProductInStore, int>(pis1, 10));
            products.Add(p3.Id, new KeyValuePair<ProductInStore, int>(pis1, 5));
            products.Add(p4.Id, new KeyValuePair<ProductInStore, int>(pis1, 4));

            pcp = new ProductConditionPolicy(0, 1, 0, 10, LogicalConnections.and);
            store.PurchasePolicy.Add(pcp);

            system = new TradingSystem(null, null);
            system.Stores.Add(store.Id, store);
            system.Users.Add(admin.Id, admin);
            system.Users.Add(ownerUser.Id, ownerUser);
        }

        [TestMethod]
        public void removePurchasePolicy_store_succ()
        {
            setUp();
            int ans = store.removePurchasePolicy(pcp.getId());
            Assert.AreEqual(0, ans);         
        }

        [TestMethod]
        public void removePurchasePolicy_role_succ()
        {
            setUp();
            StubStore sStore = new StubStore(1234, "nike", null, null, 0);
            Role ownerStub = new Role(sStore, ownerUser);
            ownerUser.Roles.Add(sStore.Id, ownerRole);
            int ans = ownerStub.removePurchasePolicy(pcp.getId());
            Assert.AreEqual(0, ans);
        }

        [TestMethod]
        public void removePurchasePolicy_user_succ()
        {
            setUp();
            StubStore sStore = new StubStore(1234, "nike", null, null, 0);
            StubRole sRole = new StubRole(sStore, ownerUser, 0);
            ownerUser.Roles.Add(sStore.Id, sRole);
            int ans = ownerUser.removePurchasePolicy(pcp.getId(), sStore.Id);
            Assert.AreEqual(0, ans);
        }

        [TestMethod]
        public void removePurchasePolicy_user_fail()
        {
            setUp();
            StubStore sStore = new StubStore(1234, "nike", null, null, 0);
            int ans = admin.removePurchasePolicy(pcp.getId(), sStore.Id);
            Assert.AreEqual(-1, ans);
        }

        [TestMethod]
        public void removePurchasePolicy_tradingSystem_succ()
        {
            setUp();
            StubStore sStore = new StubStore(1234, "nike", null, null, 0);
            StubUser tmpUser = new StubUser(2222, "owner", "7878", false, true, 0);
            system.Users.Add(tmpUser.Id, tmpUser);
            int ans = system.removePurchasePolicy(pcp.getId(), sStore.Id, tmpUser.Id);
            Assert.AreEqual(0, ans);
        }


        /*------------------------stub-classes------------------------------------*/

        class StubStore : Store
        {
            int retVal;
            public StubStore(int id, string name, List<PurchasePolicy> purchasePolicy, List<DiscountPolicy> discountPolicy, int ret) : base(id, name)
            {
                this.retVal = ret;
            }

            public override int removePurchasePolicy(int purchaseId)
            {
                return 0;
            }
        }

        class StubRole : Role
        {
            int retVal;
            public StubRole(Store store, User user, int ret) : base(store, user)
            {
                this.retVal = ret;
            }

            public override int removePurchasePolicy(int purchaseId)
            {
                return 0;
            }
        }

        class StubUser : User
        {
            int retVal;
            public StubUser(int id, string userName, string password, bool isAdmin, bool isRegistered, int ret) : base(id, userName, password, isAdmin, isRegistered)
            {
                this.retVal = ret;
            }


        }
    }
}
