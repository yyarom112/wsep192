using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;

namespace UnitTests
{
    
    [TestClass]
    public class Req2
    {
        private TradingSystem sys;
        private Encryption encrypt;
        private User admin;
        private User user;
        private ShoppingBasket basket_user;
        private Product p1;
        private Product p2;
        private Product p3;
        private Product p4;
        private ProductInStore pis1;
        private ProductInStore pis2;
        private ProductInStore pis3;
        private ProductInStore pis4;
        private Store store;
        private List<DiscountPolicy> discountPolicies;
        private List<PurchasePolicy> purchasePolicies;
        public void setUp()
        {
            discountPolicies = new List<DiscountPolicy>();
            purchasePolicies = new List<PurchasePolicy>();
            user = new User(1, "aviv", "123", false, false);
            store = new Store(0, "blabla",purchasePolicies,discountPolicies);
            Owner owner = new Owner(store, user);
            store.Roles.AddChild(owner);
            basket_user = user.Basket;
            admin = new User(0, "admin", "1234", true, false);
            basket_user = user.Basket;
            p1 = new Product(0, "first", "cat", "key", 80);
            p2 = new Product(1, "second", null, "", 3000);
            p3 = new Product(2, "third", null, "", 2000);
            p4 = new Product(3, "fourth", null, "", 5000);
            pis1 = new ProductInStore(10000000, store, p1);
            pis2 = new ProductInStore(10000000, store, p2);
            pis3 = new ProductInStore(10000000, store, p3);
            pis4 = new ProductInStore(10000000, store, p4);
            store.Products.Add(p1.Id, pis1);
            store.Products.Add(p2.Id, pis2);
            store.Products.Add(p3.Id, pis3);
            store.Products.Add(p4.Id, pis4);
            sys = new TradingSystem(null,null);
            sys.StoreCounter = 1;
            sys.ProductCounter = 4;
            sys.UserCounter = 2;
            sys.Stores.Add(store.Id, store);
            sys.Users.Add(admin.Id, admin);
            sys.Users.Add(user.Id, user);
        }
        [TestMethod]
        public void testSearchProductNotFound()
        {
            setUp();
            String expected = "";
            string details1 = "bla,bla,bla,10,100,40,30";
            expected = sys.searchProduct(details1);
            Assert.AreEqual("",expected);
        }
        [TestMethod]
        public void testSearchProductFound()
        {
            setUp();
            String expected = "name0=first&store0=blabla&quantity0=10000000";
            String details1 = "first,cat,key,10,100,0,0";
            String result = sys.searchProduct(details1);
            Assert.AreEqual(expected,result );
        }
    }
}
