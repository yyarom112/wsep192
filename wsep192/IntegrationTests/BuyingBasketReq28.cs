using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;

namespace IntegrationTests
{
    [TestClass]
    public class BuyingBasketReq28
    {

        private TradingSystem sys;
        private Encryption encrypt;

        private User admin;
        private ShoppingBasket basket_admin;

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


        public void setUp()
        {
            admin = new User(0, "admin", "123456", true, true);
            basket_admin = admin.Basket;
            user = new User(1, null, null, false, false);
            basket_user = user.Basket;

            store = new Store(-1, "store", 0, null, null);

            p1 = new Product(0, "first", null, "", 5000, 0);
            p2 = new Product(1, "second", null, "", 5000, 0);
            p3 = new Product(2, "third", null, "", 5000, 0);
            p4 = new Product(3, "fourth", null, "", 5000, 0);
            pis1 = new ProductInStore(10000000, store, p1);
            pis2 = new ProductInStore(10000000, store, p2);
            pis3 = new ProductInStore(10000000, store, p3);
            pis4 = new ProductInStore(10000000, store, p4);
            store.Products.Add(p1.Id, pis1);
            store.Products.Add(p2.Id, pis2);
            store.Products.Add(p3.Id, pis3);
            store.Products.Add(p4.Id, pis4);
            sys = new TradingSystem(null, null);
            sys.StoreCounter = 1;
            sys.ProductCounter = 4;
            sys.UserCounter = 2;
            sys.Stores.Add(store.Id, store);
            sys.Users.Add(admin.Id, admin);
            sys.Users.Add(user.Id, user);


        }

        //----------------------------@@ Checkout Path @@----------------------------
        [TestMethod]
        public void TestMethod1_confirmPurchasePolicy_PolicyDictionryEmpty()
        {
            setUp();
            ShoppingCart cart = new ShoppingCart(store.Id, store, new Dictionary<int, ProductInCart>());

            cart.Products.Add(p1.Id, new ProductInCart(1, cart, p1));
            cart.Products.Add(p2.Id, new ProductInCart(1, cart, p2));
            cart.Products.Add(p3.Id, new ProductInCart(1, cart, p3));
            cart.Products.Add(p4.Id, new ProductInCart(1, cart, p4));


            Assert.AreEqual(true, store.confirmPurchasePolicy(cart.Products));

        }


        [TestMethod]
        public void TestMethod1_calculateDiscountPolicy_DiscountDictionryEmpty()
        {
            setUp();
            ShoppingCart cart = new ShoppingCart(store.Id, store, new Dictionary<int, ProductInCart>());

            cart.Products.Add(p1.Id, new ProductInCart(1, cart, p1));
            cart.Products.Add(p2.Id, new ProductInCart(1, cart, p2));
            cart.Products.Add(p3.Id, new ProductInCart(1, cart, p3));
            cart.Products.Add(p4.Id, new ProductInCart(1, cart, p4));


            Assert.AreEqual(0, store.calculateDiscountPolicy(cart.Products));

        }


        [TestMethod]
        public void TestMethod1_cartCheckout_without_discount_in_policy()
        {
            setUp();

            ShoppingCart cart = new ShoppingCart(store.Id, store, new Dictionary<int, ProductInCart>());

            cart.Products.Add(p1.Id, new ProductInCart(1, cart, p1));
            cart.Products.Add(p2.Id, new ProductInCart(1, cart, p2));
            cart.Products.Add(p3.Id, new ProductInCart(1, cart, p3));
            cart.Products.Add(p4.Id, new ProductInCart(1, cart, p4));


            Assert.AreEqual(p1.Price + p2.Price + p3.Price + p4.Price, cart.cartCheckout());

        }

        //TODO-There is no implementation policy for this version. Add the test below.
        //[TestMethod]
        //public void TestMethod1_cartCheckout_without_discount_no_in_policy()
        //{
        //    setUp();
        //    ShoppingCart cart = new ShoppingCart(store.Id, store, new Dictionary<int, ProductInCart>());

        //    cart.Products.Add(p1.Id, new ProductInCart(1, cart, p1));
        //    cart.Products.Add(p2.Id, new ProductInCart(1, cart, p2));
        //    cart.Products.Add(p3.Id, new ProductInCart(1, cart, p3));
        //    cart.Products.Add(p4.Id, new ProductInCart(1, cart, p4));


        //    Assert.AreEqual(-1, cart.cartCheckout());

        //}

        //TODO-Discounts are not used in this version. Add the test below.
        //[TestMethod]
        //public void TestMethod1_cartCheckout_with_discount_in_policy()
        //{
        //    setUp();
        //    ShoppingCart cart = new ShoppingCart(store.Id, store, new Dictionary<int, ProductInCart>());

        //    cart.Products.Add(p1.Id, new ProductInCart(1, cart, p1));
        //    cart.Products.Add(p2.Id, new ProductInCart(1, cart, p2));
        //    cart.Products.Add(p3.Id, new ProductInCart(1, cart, p3));
        //    cart.Products.Add(p4.Id, new ProductInCart(1, cart, p4));


        //    Assert.AreEqual(p2.Price + p3.Price + p4.Price, cart.cartCheckout());

        //}




        [TestMethod]
        public void TestMethod1_ShoppingBasket_basketCheckout_succ()
        {
            setUp();
            ShoppingCart cart = new ShoppingCart(store.Id, store, new Dictionary<int, ProductInCart>());
            cart.Products.Add(p1.Id, new ProductInCart(1, cart, p1));
            cart.Products.Add(p2.Id, new ProductInCart(2, cart, p2));
            cart.Products.Add(p3.Id, new ProductInCart(3, cart, p3));
            basket_user.ShoppingCarts.Add(store.Id, cart);
            Assert.AreEqual(p1.Price+(p2.Price*2)+(p3.Price*3), basket_user.basketCheckout());
        }

        //TODO-There is no implementation policy for this version. Add the test below.
        //[TestMethod]
        //public void TestMethod1_ShoppingBasket_basketCheckout_fail()
        //{
        //    setUp();
        //    ShoppingCart cart = new ShoppingCart(store.Id, store, new Dictionary<int, ProductInCart>());
        //    cart.Products.Add(p1.Id, new ProductInCart(1, cart, p1));

        //    basket_user.ShoppingCarts.Add(store.Id, cart);
        //    Assert.AreEqual(retval, basket_user.basketCheckout());

        //}

        [TestMethod]
        public void TestMethod1_User_basketCheckout()
        {
            setUp();
            user.Basket = basket_user;
            ShoppingCart cart = new ShoppingCart(store.Id, store, new Dictionary<int, ProductInCart>());
            cart.Products.Add(p1.Id, new ProductInCart(1, cart, p1));
            cart.Products.Add(p2.Id, new ProductInCart(2, cart, p2));
            cart.Products.Add(p3.Id, new ProductInCart(3, cart, p3));
            basket_user.ShoppingCarts.Add(store.Id, cart);
            Assert.AreEqual(p1.Price + (p2.Price * 2) + (p3.Price * 3) + 50, user.basketCheckout("telaviv"));
            }

        [TestMethod]
        public void TestMethod1_TradingSystem_basketCheckout_succ()
        {
            setUp();
            ShoppingCart cart = new ShoppingCart(store.Id, store, new Dictionary<int, ProductInCart>());
            cart.Products.Add(p1.Id, new ProductInCart(1, cart, p1));
            cart.Products.Add(p2.Id, new ProductInCart(2, cart, p2));
            cart.Products.Add(p3.Id, new ProductInCart(3, cart, p3));
            basket_user.ShoppingCarts.Add(store.Id, cart);
            Assert.AreEqual(p1.Price + (p2.Price * 2) + (p3.Price * 3) + 50, sys.basketCheckout("telaviv", user.Id));
        }

        [TestMethod]
        public void TestMethod1_TradingSystem_basketCheckout_fail()
        {
            setUp();
            Assert.AreEqual(-1, sys.basketCheckout("telaviv", 2));
        }

        ////----------------------------@@ PayForBasket Path @@----------------------------

        [TestMethod]
        public void TestMethod1_updateCart_minus()
        {
            setUp();
            ShoppingCart cart = new ShoppingCart(store.Id, store, new Dictionary<int, ProductInCart>());

            cart.Products.Add(p1.Id, new ProductInCart(1, cart, p1));

            store.updateCart(cart, "-");

            Assert.AreEqual(9999999, store.Products[p1.Id].Quantity);

        }

        [TestMethod]
        public void TestMethod1_updateCart_plus()
        {
            setUp();
            ShoppingCart cart = new ShoppingCart(store.Id, store, new Dictionary<int, ProductInCart>());

            cart.Products.Add(p1.Id, new ProductInCart(1, cart, p1));

            store.updateCart(cart, "+");

            Assert.AreEqual(10000001, store.Products[p1.Id].Quantity);

        }


        
        [TestMethod]
        public void TestMethod1_payForBasket_succ()
        {
            setUp();
            sys.FinancialSystem = new FinancialSystemImpl();
            sys.SupplySystem = new ProductSupplySystemImpl();

            ShoppingCart cart = new ShoppingCart(store.Id, store, new Dictionary<int, ProductInCart>());

            cart.Products.Add(p1.Id, new ProductInCart(1, cart, p1));
            cart.Products.Add(p2.Id, new ProductInCart(1, cart, p2));
            cart.Products.Add(p3.Id, new ProductInCart(1, cart, p3));
            cart.Products.Add(p4.Id, new ProductInCart(1, cart, p4));

            user.Basket.ShoppingCarts.Add(cart.Store.Id, cart);

            ShoppingBasket check = user.Basket;

            Assert.AreEqual(check, sys.payForBasket(0, new DateTime(1990, 1, 1), user.Id));

        }

        //TODO-The money collection system is not very well connected, so this test will be added later on
        //[TestMethod]
        //public void TestMethod1_payForBasket_without_FinancialSystem()
        //{
        //    setUp();
        //    sys.FinancialSystem = new StubFinancialSystem(false);
        //    sys.SupplySystem = new StubProductSupplySystem(true);

        //    ShoppingCart cart = new ShoppingCart(store.Id, store, new Dictionary<int, ProductInCart>());

        //    cart.Products.Add(p1.Id, new ProductInCart(1, cart, p1));
        //    cart.Products.Add(p2.Id, new ProductInCart(1, cart, p2));
        //    cart.Products.Add(p3.Id, new ProductInCart(1, cart, p3));
        //    cart.Products.Add(p4.Id, new ProductInCart(1, cart, p4));

        //    user.Basket.ShoppingCarts.Add(cart.Store.Id, cart);

        //    ShoppingBasket check = user.Basket;

        //    Assert.AreEqual(null, sys.payForBasket(0, new DateTime(1990, 1, 1), user.Id));

        //}

        //TODO-The Supply System is not very well connected, so this test will be added later on
        //[TestMethod]
        //public void TestMethod1_payForBasket_without_SupplySystem()
        //{
        //    setUp();
        //    sys.FinancialSystem = new StubFinancialSystem(true);
        //    sys.SupplySystem = new StubProductSupplySystem(false);

        //    ShoppingCart cart = new ShoppingCart(store.Id, store, new Dictionary<int, ProductInCart>());

        //    cart.Products.Add(p1.Id, new ProductInCart(1, cart, p1));
        //    cart.Products.Add(p2.Id, new ProductInCart(1, cart, p2));
        //    cart.Products.Add(p3.Id, new ProductInCart(1, cart, p3));
        //    cart.Products.Add(p4.Id, new ProductInCart(1, cart, p4));

        //    user.Basket.ShoppingCarts.Add(cart.Store.Id, cart);

        //    ShoppingBasket check = user.Basket;

        //    Assert.AreEqual(null, sys.payForBasket(0, new DateTime(1990, 1, 1), user.Id));

        //}




    }
}
