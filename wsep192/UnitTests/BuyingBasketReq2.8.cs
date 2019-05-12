using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;

namespace UnitTests
{
    [TestClass]
    public class BuyingBasketReq2
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

        private RevealedDiscount rd_min;
        private RevealedDiscount rd_mid;
        private RevealedDiscount rd_max;

        private RevealedDiscount rd_max_without;
        private RevealedDiscount rd_mid_without;



        private Store store;


        public void setUp()
        {
            admin = new User(0, "admin", "123456", true, true);
            basket_admin = admin.Basket;
            user = new User(1, null, null, false, false);
            basket_user = user.Basket;

            store = new Store(-1, "store");

            p1 = new Product(0, "first", "", "", 5000);
            p2 = new Product(1, "second", "", "", 5000);
            p3 = new Product(2, "third", "", "", 5000);
            p4 = new Product(3, "fourth", "", "", 5000);
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

            Dictionary<int, KeyValuePair<ProductInStore, int>> discountProduct = new Dictionary<int, KeyValuePair<ProductInStore, int>>();
            discountProduct.Add(p1.Id, new KeyValuePair<ProductInStore, int>(pis1, 0));
            rd_min = new RevealedDiscount(sys.DiscountPolicyCounter++, 0.2, discountProduct, new DateTime(2222, 1, 1), DuplicatePolicy.WithMultiplication);
            rd_mid = new RevealedDiscount(sys.DiscountPolicyCounter++, 0.25, discountProduct, new DateTime(2222, 1, 1), DuplicatePolicy.WithMultiplication);
            rd_max = new RevealedDiscount(sys.DiscountPolicyCounter++, 0.3, discountProduct, new DateTime(2222, 1, 1), DuplicatePolicy.WithMultiplication);


            rd_mid_without = new RevealedDiscount(sys.DiscountPolicyCounter++, 0.35, discountProduct, new DateTime(2222, 1, 1), DuplicatePolicy.WithoutMultiplication);
            rd_max_without = new RevealedDiscount(sys.DiscountPolicyCounter++, 1, discountProduct, new DateTime(2222, 1, 1), DuplicatePolicy.WithoutMultiplication);

        }

        //----------------------------@@ Checkout Path @@----------------------------
        [TestMethod]
        public void TestMethod1_cartCheckout_without_discount_in_policy()
        {
            setUp();
            store = new StubStore(-1, "store", 0, true, 0);
            store.Products.Add(p1.Id, pis1);
            store.Products.Add(p2.Id, pis2);
            store.Products.Add(p3.Id, pis3);
            store.Products.Add(p4.Id, pis4);
            ShoppingCart cart = new ShoppingCart(store.Id, store);

            cart.Products.Add(p1.Id, new ProductInCart(1, cart, p1));
            cart.Products.Add(p2.Id, new ProductInCart(1, cart, p2));
            cart.Products.Add(p3.Id, new ProductInCart(1, cart, p3));
            cart.Products.Add(p4.Id, new ProductInCart(1, cart, p4));


            Assert.AreEqual(p1.Price + p2.Price + p3.Price + p4.Price, cart.cartCheckout(new src.Domain.Dataclass.UserDetailes(null, false)));

        }

        [TestMethod]
        public void TestMethod1_cartCheckout_without_discount_no_in_policy()
        {
            setUp();
            store = new StubStore(-1, "store", 0, false, 0);
            store.Products.Add(p1.Id, pis1);
            store.Products.Add(p2.Id, pis2);
            store.Products.Add(p3.Id, pis3);
            store.Products.Add(p4.Id, pis4);
            ShoppingCart cart = new ShoppingCart(store.Id, store);

            cart.Products.Add(p1.Id, new ProductInCart(1, cart, p1));
            cart.Products.Add(p2.Id, new ProductInCart(1, cart, p2));
            cart.Products.Add(p3.Id, new ProductInCart(1, cart, p3));
            cart.Products.Add(p4.Id, new ProductInCart(1, cart, p4));


            Assert.AreEqual(-1, cart.cartCheckout(new src.Domain.Dataclass.UserDetailes(null, false)));

        }


        [TestMethod]
        public void TestMethod1_cartCheckout_with_discount_in_policy()
        {
            setUp();
            store = new StubStore(-1, "store", 0, true, p1.Price);
            store.Products.Add(p1.Id, pis1);
            store.Products.Add(p2.Id, pis2);
            store.Products.Add(p3.Id, pis3);
            store.Products.Add(p4.Id, pis4);
            ShoppingCart cart = new ShoppingCart(store.Id, store);

            cart.Products.Add(p1.Id, new ProductInCart(1, cart, p1));
            cart.Products.Add(p2.Id, new ProductInCart(1, cart, p2));
            cart.Products.Add(p3.Id, new ProductInCart(1, cart, p3));
            cart.Products.Add(p4.Id, new ProductInCart(1, cart, p4));


            Assert.AreEqual(p1.Price+p2.Price + p3.Price + p4.Price, cart.cartCheckout(new src.Domain.Dataclass.UserDetailes(null, false)));

        }

        [TestMethod]
        public void TestMethod1_confirmPurchasePolicy_PolicyDictionryEmpty()
        {
            setUp();
            ShoppingCart cart = new ShoppingCart(store.Id, store);

            cart.Products.Add(p1.Id, new ProductInCart(1, cart, p1));
            cart.Products.Add(p2.Id, new ProductInCart(1, cart, p2));
            cart.Products.Add(p3.Id, new ProductInCart(1, cart, p3));
            cart.Products.Add(p4.Id, new ProductInCart(1, cart, p4));


            Assert.AreEqual(true, store.confirmPurchasePolicy(cart.Products, new src.Domain.Dataclass.UserDetailes(null, false)));

        }


        [TestMethod]
        public void TestMethod1_calculateDiscountPolicy_DiscountDictionryEmpty()
        {
            setUp();
            ShoppingCart cart = new ShoppingCart(store.Id, store);

            cart.Products.Add(p1.Id, new ProductInCart(1, cart, p1));
            cart.Products.Add(p2.Id, new ProductInCart(1, cart, p2));
            cart.Products.Add(p3.Id, new ProductInCart(1, cart, p3));
            cart.Products.Add(p4.Id, new ProductInCart(1, cart, p4));


            Assert.AreEqual(0, store.calculateDiscountPolicy(cart.Products));

        }

        [TestMethod]
        public void TestMethod1_calculateDiscountPolicy_DiscountDictionrywithDiscounts()
        {
            setUp();
            ShoppingCart cart = new ShoppingCart(store.Id, store);
            cart.Products.Add(p1.Id, new ProductInCart(1, cart, p1));

            store.DiscountPolicy.Add(rd_min);
            store.DiscountPolicy.Add(rd_mid);
            store.DiscountPolicy.Add(rd_max);

            Assert.AreEqual(2900, store.calculateDiscountPolicy(cart.Products),"all and");

            setUp();
            cart = new ShoppingCart(store.Id, store);
            cart.Products.Add(p1.Id, new ProductInCart(1, cart, p1));

            store.DiscountPolicy.Add(rd_min);
            store.DiscountPolicy.Add(rd_mid_without);
            store.DiscountPolicy.Add(rd_max);

            Assert.AreEqual(2200, store.calculateDiscountPolicy(cart.Products), "one  bad without");

            setUp();
            store.DiscountPolicy.Add(rd_min);
            store.DiscountPolicy.Add(rd_mid);
            store.DiscountPolicy.Add(rd_max_without);

            Assert.AreEqual(5000, store.calculateDiscountPolicy(cart.Products), "one  good without");

        }

        [TestMethod]
        public void TestMethod1_checkQuntity_succ()
        {
            setUp();
            ShoppingCart cart_tocheck = new ShoppingCart(store.Id, store);
            ShoppingCart cart = new ShoppingCart(store.Id, store);


            cart.Products.Add(p1.Id, new ProductInCart(1, cart, p1));
            cart.Products.Add(p2.Id, new ProductInCart(1, cart, p2));
            cart.Products.Add(p3.Id, new ProductInCart(1, cart, p3));
            cart.Products.Add(p4.Id, new ProductInCart(1, cart, p4));


            cart_tocheck.Products.Add(p1.Id, new ProductInCart(1, cart, p1));
            cart_tocheck.Products.Add(p2.Id, new ProductInCart(1, cart, p2));
            cart_tocheck.Products.Add(p3.Id, new ProductInCart(1, cart, p3));
            cart_tocheck.Products.Add(p4.Id, new ProductInCart(1, cart, p4));

            bool check = true;
            foreach (ProductInCart p in cart.Products.Values)
            {
                if (!cart_tocheck.Products.ContainsKey(p.Product.Id) || cart_tocheck.Products[p.Product.Id].Quantity != p.Quantity)
                    check = false;
            }

            Assert.AreEqual(true, check);

        }

        [TestMethod]
        public void TestMethod1_checkQuntity_fail()
        {
            setUp();
            ShoppingCart cart_tocheck = new ShoppingCart(store.Id, store);
            ShoppingCart cart = new ShoppingCart(store.Id, store);


            cart.Products.Add(p1.Id, new ProductInCart(99999999, cart, p1));
            cart.Products.Add(p2.Id, new ProductInCart(1, cart, p2));
            cart.Products.Add(p3.Id, new ProductInCart(1, cart, p3));
            cart.Products.Add(p4.Id, new ProductInCart(1, cart, p4));


            cart_tocheck.Products.Add(p1.Id, new ProductInCart(1, cart, p1));
            cart_tocheck.Products.Add(p2.Id, new ProductInCart(1, cart, p2));
            cart_tocheck.Products.Add(p3.Id, new ProductInCart(1, cart, p3));
            cart_tocheck.Products.Add(p4.Id, new ProductInCart(1, cart, p4));

            bool check = true;
            foreach (ProductInCart p in cart.Products.Values)
            {
                if (!cart_tocheck.Products.ContainsKey(p.Product.Id) || cart_tocheck.Products[p.Product.Id].Quantity != p.Quantity)
                    check = false;
            }

            Assert.AreEqual(false, check);

        }

        [TestMethod]
        public void TestMethod1_ShoppingBasket_basketCheckout_succ()
        {
            setUp();
            int retval = 10;
            ShoppingCart cart = new Stubcart(store.Id, store, new Dictionary<int, ProductInCart>(), retval);
            basket_user.ShoppingCarts.Add(store.Id, cart);
            Assert.AreEqual(retval, basket_user.basketCheckout(new src.Domain.Dataclass.UserDetailes(null, false)));

        }

        [TestMethod]
        public void TestMethod1_ShoppingBasket_basketCheckout_fail()
        {
            setUp();
            int retval = -1;
            ShoppingCart cart = new Stubcart(store.Id, store, new Dictionary<int, ProductInCart>(), retval);
            basket_user.ShoppingCarts.Add(store.Id, cart);
            Assert.AreEqual(retval, basket_user.basketCheckout(new src.Domain.Dataclass.UserDetailes(null, false)));

        }

        [TestMethod]
        public void TestMethod1_User_basketCheckout_emptyBasket()
        {
            setUp();
            int retval = 10;
            basket_user = new StubBasket(retval);
            user.Basket = basket_user;
            Assert.AreEqual(0, user.basketCheckout("telaviv"));
        }

        [TestMethod]
        public void TestMethod1_TradingSystem_basketCheckout_succ()
        {
            setUp();
            bool retval = true;
            StubUser user = new StubUser(2, null, null, false, false, retval);
            sys.Users.Add(user.Id, user);
            Assert.AreEqual(1, sys.basketCheckout("telaviv", 2));


        }

        [TestMethod]
        public void TestMethod1_TradingSystem_basketCheckout_fail()
        {
            setUp();
            bool retval = true;
            StubUser user = new StubUser(2, null, null, false, false, retval);
            Assert.AreEqual(-1, sys.basketCheckout("telaviv", 2));
        }



        //----------------------------@@ PayForBasket Path @@----------------------------

        [TestMethod]
        public void TestMethod1_updateCart_minus()
        {
            setUp();
            ShoppingCart cart = new ShoppingCart(store.Id, store);

            cart.Products.Add(p1.Id, new ProductInCart(1, cart, p1));

            store.updateCart(cart, "-");

            Assert.AreEqual(9999999, store.Products[p1.Id].Quantity);

        }

        [TestMethod]
        public void TestMethod1_updateCart_plus()
        {
            setUp();
            ShoppingCart cart = new ShoppingCart(store.Id, store);

            cart.Products.Add(p1.Id, new ProductInCart(1, cart, p1));

            store.updateCart(cart, "+");

            Assert.AreEqual(10000001, store.Products[p1.Id].Quantity);

        }



        [TestMethod]
        public void TestMethod1_payForBasket_succ()
        {
            setUp();
            sys.FinancialSystem = new StubFinancialSystem(true);
            sys.SupplySystem = new StubProductSupplySystem(true);

            ShoppingCart cart = new StubCart(store.Id, store, 10);

            cart.Products.Add(p1.Id, new ProductInCart(1, cart, p1));
            cart.Products.Add(p2.Id, new ProductInCart(1, cart, p2));
            cart.Products.Add(p3.Id, new ProductInCart(1, cart, p3));
            cart.Products.Add(p4.Id, new ProductInCart(1, cart, p4));
            cart.Store = new StubStore(1, "", 0, true, 0);
            user.Basket = new StubBasket(13);
            sys.Stores.Add(1, cart.Store);
            user.Basket.ShoppingCarts.Add(1, cart);
            List<String[]> check = sys.cartToString(cart);


            Assert.AreEqual(expected: check.Count, actual: sys.payForBasket(0, new DateTime(1990, 1, 1), user.Id).Count);

        }

        //----------------------------@@ Validate test  @@----------------------------

        // You can not finish a purchase if a payment is unsuccessful
        [TestMethod]
        public void TestMethod1_payForBasket_without_FinancialSystem()
        {
            setUp();
            sys.FinancialSystem = new StubFinancialSystem(false);
            sys.SupplySystem = new StubProductSupplySystem(true);

            ShoppingCart cart = new ShoppingCart(store.Id, store);

            cart.Products.Add(p1.Id, new ProductInCart(1, cart, p1));
            cart.Products.Add(p2.Id, new ProductInCart(1, cart, p2));
            cart.Products.Add(p3.Id, new ProductInCart(1, cart, p3));
            cart.Products.Add(p4.Id, new ProductInCart(1, cart, p4));

            user.Basket.ShoppingCarts.Add(cart.Store.Id, cart);

            ShoppingBasket check = user.Basket;

            Assert.AreEqual(null, sys.payForBasket(0, new DateTime(1990, 1, 1), user.Id));

        }


        [TestMethod]
        public void TestMethod1_payForBasket_without_SupplySystem()
        {
            setUp();
            sys.FinancialSystem = new StubFinancialSystem(true);
            sys.SupplySystem = new StubProductSupplySystem(false);

            ShoppingCart cart = new ShoppingCart(store.Id, store);

            cart.Products.Add(p1.Id, new ProductInCart(1, cart, p1));
            cart.Products.Add(p2.Id, new ProductInCart(1, cart, p2));
            cart.Products.Add(p3.Id, new ProductInCart(1, cart, p3));
            cart.Products.Add(p4.Id, new ProductInCart(1, cart, p4));

            user.Basket.ShoppingCarts.Add(cart.Store.Id, cart);

            ShoppingBasket check = user.Basket;

            Assert.AreEqual(null, sys.payForBasket(0, new DateTime(1990, 1, 1), user.Id));

        }





    }




    //----------------------------@@stub class@@----------------------------


    class StubStore : Store
    {
        private bool policy;
        private double discount;


        public StubStore(int id, string name, int storeRate, bool policy, double discount) : base(id, name)
        {
            this.policy = policy;
            this.discount = discount;
        }


        public override bool confirmPurchasePolicy(Dictionary<int, ProductInCart> products, src.Domain.Dataclass.UserDetailes user)
        {
            return policy;
        }

        public virtual double calculateDiscountPolicy(Dictionary<int, ProductInCart> products)
        {
            return discount;
        }

        public override void updateCart(ShoppingCart cart, String opt)
        {
            if (!policy)
                cart.Products[1].Quantity--;
        }
    }


    class Stubcart : ShoppingCart
    {
        private int retVal;

        public Stubcart(int storeId, Store store, Dictionary<int, ProductInCart> products, int retval) : base(storeId, store)
        {
            this.retVal = retval;
            this.Products = products;
        }

        public override double cartCheckout(src.Domain.Dataclass.UserDetailes user)
        {
            return retVal;
        }



    }

    class StubBasket : ShoppingBasket
    {
        private int retVal;
        public StubBasket(int ret) : base()
        {
            this.retVal = ret;
        }

        public override double basketCheckout(src.Domain.Dataclass.UserDetailes user)
        {
            return retVal;
        }
    }




    class StubProductSupplySystem : ProductSupplySystem
    {
        private bool retVal;

        public StubProductSupplySystem(bool ret)
        {
            this.retVal = ret;
        }

        public bool connect()
        {
            return retVal;
        }

        public bool deliverToCustomer(string address, string packageDetails)
        {
            return retVal;
        }
    }

    class StubFinancialSystem : FinancialSystem
    {
        private bool retVal;

        public StubFinancialSystem(bool ret)
        {
            this.retVal = ret;
        }

        public bool Chargeback(long cardNumber, DateTime date, double amount)
        {
            return true;
        }

        public bool connect()
        {
            return retVal;
        }

        public bool payment(long cardNumber, DateTime date, int sum)
        {
            return retVal;
        }

        public bool payment(long cardNumber, DateTime date, double amount, int paymentTarget)
        {
            return retVal;
        }
    }




}
