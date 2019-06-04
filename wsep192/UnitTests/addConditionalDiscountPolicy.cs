using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;

namespace UnitTests
{
    [TestClass]
    public class addConditionalDiscountPolicy
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

        List<String> products;
        LogicalConnections logic;
        DuplicatePolicy duplicate;
        DateTime date1;

        LogicalCondition lc;

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

            products = new List<string>();
            products.Add("first");
            products.Add("second");
            products.Add("third");
            products.Add("fourth");


            system = new TradingSystem(null, null);
            system.Stores.Add(store.Id, store);
            system.Users.Add(admin.Id, admin);
            system.Users.Add(ownerUser.Id, ownerUser);
            logic = LogicalConnections.and;
            duplicate = DuplicatePolicy.WithMultiplication;
            date1 = new DateTime(2019, 10, 1);

            lc = new LogicalCondition(0, 0.5, null, new DateTime(2222,1,1), DuplicatePolicy.WithMultiplication, LogicalConnections.and);

        }

        [TestMethod]
        public void addRevealedDiscountPolicy_store_succ()
        {
            setUp();
            Assert.AreEqual(1, store.addConditionalDiscuntPolicy(1, products, "", 20, date1, duplicate, logic));
        }

        [TestMethod]
        public void addRevealedDiscountPolicy_role_succ()
        {
            setUp();
            StubStore sStore = new StubStore(1234, "nike", null, null, 1);
            Role ownerStub = new Role(sStore, ownerUser);
            ownerUser.Roles.Add(sStore.Id, ownerRole);
            int ans = ownerStub.addConditionalDiscuntPolicy(products, "", 20, date1, 1, duplicate, logic);
            Console.WriteLine(ans);
            Assert.AreEqual(1, ans);
        }

        [TestMethod]
        public void addRevealedDiscountPolicy_user_succ()
        {
            setUp();
            StubStore sStore = new StubStore(3456, "nike", null, null, 1);
            StubRole sRole = new StubRole(sStore, ownerUser, 1);
            ownerUser.Roles.Add(sStore.Id, sRole);
            int ans = ownerUser.addConditionalDiscuntPolicy(products, "", 20, 40, duplicate, logic, 0, sStore.Id);
            Assert.AreEqual(1, ans);
        }

        [TestMethod]
        public void addRevealedDiscountPolicy_user_fail()
        {
            setUp();
            StubStore sStore = new StubStore(3456, "nike", null, null, 1);
            StubRole sRole = new StubRole(sStore, admin, 1);
            admin.Roles.Add(sStore.Id, sRole);
            int ans = admin.addConditionalDiscuntPolicy(products, "", 20, 40, duplicate, logic, 0, sStore.Id);
            Assert.AreEqual(-1, ans);
        }

        [TestMethod]
        public void addRevealedDiscountPolicy_tradingSystem_succ()
        {
            setUp();
            StubStore sStore = new StubStore(3456, "nike", null, null, 1);
            StubUser tmpUser = new StubUser(2222, "owner", "7878", false, true, 1);
            system.Users.Add(tmpUser.Id, tmpUser);
            int ans = system.addConditionalDiscuntPolicy(products, "", 20, 40, 0, 0, tmpUser.Id, sStore.Id);
            Assert.AreEqual(1, ans);
        }


        /*------------------------Condition Convert------------------------------------*/

        [TestMethod]
        public void store_ConvertProductNameToProductInStore()
        {
            setUp();
            Assert.AreEqual(pis1, store.ConvertProductNameToProductInStore("first"));
            Assert.AreEqual(null, store.ConvertProductNameToProductInStore("p1"));

        }

        [TestMethod]
        public void store_conditionConvert_simple_only_leaf()
        {
            setUp();
            String details = "( first , 10 )";
            store.conditionConvert(lc, 0, details.Split(',').Length, details.Split(','), 0);
            List<KeyValuePair<ProductInStore, int>> productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis1, 9));
            Assert.AreEqual(false, lc.getChild(0).checkCondition(productToBuy));
            productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis1, 11));
            Assert.AreEqual(true, lc.getChild(0).checkCondition(productToBuy));

        }

        [TestMethod]
        public void store_conditionConvert_simple_only_and()
        {
            setUp();
            String details = "(+,((first, 10 ),(second,10)))";
            store.conditionConvert(lc, 0, details.Split(',').Length, details.Split(','), 0);
            List<KeyValuePair<ProductInStore, int>> productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis1, 9));
            Assert.AreEqual(false, lc.checkCondition(productToBuy));
            productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis1, 11));
            Assert.AreEqual(false, lc.checkCondition(productToBuy));

            productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis1, 11));
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis2, 11));

            Assert.AreEqual(true, lc.checkCondition(productToBuy));
        }

        [TestMethod]
        public void store_conditionConvert_simple_or()
        {
            setUp();
            String details = "(-,((first, 10 ),(second,10)))";
            store.conditionConvert(lc, 0, details.Split(',').Length, details.Split(','), 0);
            List<KeyValuePair<ProductInStore, int>> productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis1, 9));
            Assert.AreEqual(false, lc.getChild(1).checkCondition(productToBuy));
            productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis1, 11));
            Assert.AreEqual(true, lc.getChild(1).checkCondition(productToBuy));

            productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis1, 11));
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis2, 11));

            Assert.AreEqual(true, lc.checkCondition(productToBuy));
        }

        [TestMethod]
        public void store_conditionConvert_simple_xor()
        {
            setUp();
            String details = "(#,((first, 10 ),(second,10)))";
            store.conditionConvert(lc, 0, details.Split(',').Length, details.Split(','), 0);
            List<KeyValuePair<ProductInStore, int>> productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis1, 9));
            Assert.AreEqual(false, lc.getChild(1).checkCondition(productToBuy));
            productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis1, 11));
            Assert.AreEqual(true, lc.getChild(1).checkCondition(productToBuy));

            productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis1, 11));
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis2, 11));

            Assert.AreEqual(false, lc.checkCondition(productToBuy));
        }

        /*------------------------stub-classes------------------------------------*/

        class StubStore : Store
        {
            int retVal;
            public StubStore(int id, string name, List<PurchasePolicy> purchasePolicy, List<DiscountPolicy> discountPolicy, int ret) : base(id, name)
            {
                this.retVal = ret;
            }

            public override int addConditionalDiscuntPolicy(int discountId, List<String> productsList, String condition, double discountPrecentage, DateTime expiredDiscountDate, DuplicatePolicy dup, LogicalConnections logic)
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

            public override int addConditionalDiscuntPolicy(List<String> products, String condition, double discountPrecentage, DateTime expiredDate, int discountId, DuplicatePolicy duplicate, LogicalConnections logic)
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

            public override int addConditionalDiscuntPolicy(List<String> products, String condition, double discountPrecentage, int expiredDiscountDate, DuplicatePolicy duplicate, LogicalConnections logic, int discountId, int storeId)
            {
                return 1;
            }
        }
    }
}
