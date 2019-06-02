using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;

namespace IntegrationTests
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
            int ans = ownerRole.addConditionalDiscuntPolicy(products, "", 20, date1, 1, duplicate, logic);
            Assert.AreEqual(1, ans);
        }

        [TestMethod]
        public void addRevealedDiscountPolicy_user_succ()
        {
            setUp();
            int ans = ownerUser.addConditionalDiscuntPolicy(products, "", 20, 40, duplicate, logic, 0, store.Id);
            Assert.AreEqual(0, ans);
        }

        [TestMethod]
        public void addRevealedDiscountPolicy_user_fail()
        {
            setUp();
            int ans = admin.addConditionalDiscuntPolicy(products, "", 20, 40, duplicate, logic, 0, store.Id);
            Assert.AreEqual(-1, ans);
        }

        [TestMethod]
        public void addRevealedDiscountPolicy_tradingSystem_succ()
        {
            setUp();
            int ans = system.addConditionalDiscuntPolicy(products, "", 20, 40, 0, 0, ownerUser.Id, store.Id);
            Assert.AreEqual(0, ans);
        }
    }
}
