﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;
using src.ServiceLayer;

namespace Acceptance_Tests
{
    [TestClass]
    public class openStore
    {
        private ServiceLayer service;
        private TradingSystem sys;
        private User admin;
        private User user;
        private ShoppingBasket basket_user;
        private Product p1;
        private Product p2;
        private Product p3;
        private ProductInStore pis1;
        private ProductInStore pis2;
        private ProductInStore pis3;
        private Store store;
        private List<DiscountPolicy> discountPolicies;
        private List<PurchasePolicy> purchasePolicies;

        public void setUp()
        {
            service = new ServiceLayer();
            service.init("admin", "1234");
            service.initUser("tmpuser");
            discountPolicies = new List<DiscountPolicy>();
            purchasePolicies = new List<PurchasePolicy>();
            user = new User(202113609, "Yonit Levy", "23&As2", false, false);
            store = new Store(3, "Bershka", purchasePolicies, discountPolicies);
            Owner owner = new Owner(store, user);
            store.Roles.AddChild(owner);
            admin = new User(201655309, "Tzukit Levi", "2Rt6!)", true, false);
            basket_user = user.Basket;
            p1 = new Product(0, "dotted skirt", "Trousers", "black dots", 80);
            p2 = new Product(1, "Mom jeans", "jeans", "black", 600);
            p3 = new Product(2, "top", "shirts", "light blue", 90);
            pis1 = new ProductInStore(64, store, p1);
            pis2 = new ProductInStore(31, store, p2);
            pis3 = new ProductInStore(17, store, p3);
            store.Products.Add(p1.Id, pis1);
            store.Products.Add(p2.Id, pis2);
            store.Products.Add(p3.Id, pis3);
            sys = new TradingSystem(null, null);
            sys.StoreCounter = 1;
            sys.ProductCounter = 3;
            sys.UserCounter = 2;
            sys.Users.Add(admin.Id, admin);
            sys.Users.Add(user.Id, user);
        }

        [TestMethod]
        //an unregisterd user tries to preform open store.
        public void testOpenStore1()
        {
            setUp();
            bool x = service.openStore("Bershka", "Yonit Levy");
            Assert.IsFalse(x);
        }

        [TestMethod]
        //a registerd user tries to preform open store.
        public void testOpenStore2()
        {
            setUp();
            service.register("Yonit Levy", "23&As2", "tmpuser");
            bool x = service.openStore("Bershka", "Yonit Levy");
            Assert.IsTrue(x);
        }
    }
}
