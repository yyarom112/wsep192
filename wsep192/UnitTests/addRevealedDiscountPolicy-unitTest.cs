using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;

namespace UnitTests
{
    [TestClass]
    public class addRevealedDiscountPolicy_unitTest
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


            system = new TradingSystem(null, null);
            system.Stores.Add(store.Id, store);
            system.Users.Add(admin.Id, admin);
            system.Users.Add(ownerUser.Id, ownerUser);
        }


        [TestMethod]
        public void addRevealedDiscountPolicy_store_succ()
        {
            setUp();
            DateTime date1 = new DateTime(2019, 10, 1);
            DuplicatePolicy logic = DuplicatePolicy.WithMultiplication;
            Assert.AreEqual(1, store.addRevealedDiscountPolicy(products, 20, date1, 1, logic));
        }

        [TestMethod]
        public void addRevealedDiscountPolicy_role_succ()
        {
            setUp();
            DateTime date1 = new DateTime(2019, 10, 1);
            DuplicatePolicy logic = DuplicatePolicy.WithMultiplication;
            int ans = ownerRole.addRevealedDiscountPolicy(products, 50, date1, 2, logic);
            Assert.AreEqual(2, ans);
        }

        [TestMethod]
        public void addRevealedDiscountPolicy_user_succ()
        {
            setUp();
            DateTime date1 = new DateTime(2019, 10, 1);
            DuplicatePolicy logic = DuplicatePolicy.WithMultiplication;
            StubStore sStore = new StubStore(3456, "nike", null, null, 1);
            StubRole sRole = new StubRole(sStore, ownerUser, 1);
            ownerUser.Roles.Add(sStore.Id,sRole);
            int ans = ownerUser.addRevealedDiscountPolicy(products, 50, sStore.Id, 20, 1, logic);
            Assert.AreEqual(1, ans);
        }

        [TestMethod]
        public void addRevealedDiscountPolicy_user_fail()
        {
            setUp();
            DateTime date1 = new DateTime(2019, 10, 1);
            DuplicatePolicy logic = DuplicatePolicy.WithMultiplication;
            StubStore sStore = new StubStore(3456, "nike", null, null, 1);
            StubRole sRole = new StubRole(sStore, admin, 1);
            admin.Roles.Add(sStore.Id, sRole);
            int ans = admin.addRevealedDiscountPolicy(products, 50, sStore.Id, 20, 1, logic);
            Assert.AreEqual(-1, ans);
        }

        [TestMethod]
        public void addRevealedDiscountPolicy_tradingSystem_succ()
        {
            setUp();
            DateTime date1 = new DateTime(2019, 10, 1);
            DuplicatePolicy logic = DuplicatePolicy.WithMultiplication;
            StubStore sStore = new StubStore(3456, "nike", null, null, 1);
            StubUser tmpUser = new StubUser(2222, "owner", "7878", false, true, 1);
            system.Users.Add(tmpUser.Id, tmpUser);
            int ans = system.addRevealedDiscountPolicy(products, 20, tmpUser.Id, sStore.Id, 10, logic);
            Assert.AreEqual(1, ans);
        }


        /*------------------------stub-classes------------------------------------*/

        class StubStore : Store
        {
            int retVal;
            public StubStore(int id, string name, List<PurchasePolicy> purchasePolicy, List<DiscountPolicy> discountPolicy, int ret) : base(id, name)
            {
                this.retVal = ret;
            }

            public override int addRevealedDiscountPolicy(Dictionary<int, KeyValuePair<ProductInStore, int>> products, double discountPrecentage, DateTime expiredDate, int discountId, DuplicatePolicy logic)
            {
                return 1;
            }
        }

        class StubRole : Role
        {
            int retVal;
            public StubRole(Store store, User user, int ret) : base(store, user)
            {
                this.retVal = ret;
            }

            public override int addRevealedDiscountPolicy(Dictionary<int, KeyValuePair<ProductInStore, int>> products, double discountPrecentage, DateTime expiredDate, int discountId, DuplicatePolicy logic)
            {
                return 1;
            }
        }

        class StubUser : User
        {
            int retVal;
            public StubUser(int id, string userName, string password, bool isAdmin, bool isRegistered, int ret) : base(id, userName, password, isAdmin, isRegistered)
            {
                this.retVal = ret;
            }

            public override int addRevealedDiscountPolicy(Dictionary<int, KeyValuePair<ProductInStore, int>> products, double discountPrecentage, int storeID, int expiredDiscountDate, int discountId, DuplicatePolicy logic)
            {
                return 1;
            }
        }
    }
}
