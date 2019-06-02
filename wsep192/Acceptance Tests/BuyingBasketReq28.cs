using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;

namespace Acceptance_Tests
{
    [TestClass]
    public class BuyingBasketReq28
    {
        ServiceLayer service;

        public void Setup()
        {
            service = ServiceLayer.getInstance();
            service.register("raul", "1234", service.initUser());
            service.signIn("admin", "1234");
            service.signIn("raul", "1234");

            service.openStore("store", "admin");
            service.openStore("adidas", "raul");

            service.createNewProductInStore("p1", "", "", 10, "store", "admin");
            service.createNewProductInStore("p1", "", "", 10, "adidas", "raul");

            List<KeyValuePair<string, int>> p1 = new List<KeyValuePair<string, int>>();
            List<KeyValuePair<string, int>> toInsert = new List<KeyValuePair<string, int>>();
            toInsert.Add(new KeyValuePair<string, int>("p1", 100));
            service.addProductsInStore(toInsert, "store", "admin");
            service.addProductsInStore(toInsert, "adidas", "raul");

        }


        [TestMethod]
        public void TestMethod_AnAttemptToPurchaseAnEmptyBasket()
        {
            Setup();
            Assert.AreEqual(0, service.basketCheckout("telaviv", "raul"));
            List<KeyValuePair<string, int>> toInsert = new List<KeyValuePair<string, int>>();
            Assert.AreEqual(true, compare_List(toInsert, service.payForBasket(1, new DateTime(1990, 1, 1), "raul")));
            service.shutDown();
        }

        [TestMethod]
        public void TestMethod_PurchaseAProductThatIsNotInStock()
        {
            Setup();
            List<KeyValuePair<string, int>> toInsert = new List<KeyValuePair<string, int>>();
            toInsert.Add(new KeyValuePair<string, int>("p1", 1000));
            service.addProductsToCart(toInsert, "store", "raul");
            service.editProductQuantityInCart("p1", 10, "store", "raul");
            Assert.AreEqual(150, service.basketCheckout("telaviv", "raul"));
            toInsert[0] = new KeyValuePair<string, int>("p1", 10);
            Assert.AreEqual(true, compare_List(toInsert, service.payForBasket(1, new DateTime(1990, 1, 1), "raul")));
            service.shutDown();
        }
        //You need to add another test in this frame when there is a buying policy in the next version
        [TestMethod]
        public void TestMethod_PurchaseOfAProductIsNotInAccordanceWithThePurchasingPolicy_negativePurches()
        {
            Setup();
            try
            {
                List<KeyValuePair<string, int>> toInsert = new List<KeyValuePair<string, int>>();
                toInsert.Add(new KeyValuePair<string, int>("p1", 1));
                if (service.addProductsToCart(toInsert, "store", "raul") && service.editProductQuantityInCart("p1", -1, "store", "raul"))
                    Assert.AreEqual(false, true);
            }
            catch (Exception e) { }
            List<KeyValuePair<string, int>> list = service.showCart("store", "raul");
            bool neg = containsNeg(list);


            if (neg)
            {
                List<KeyValuePair<string, int>> toInsert = new List<KeyValuePair<string, int>>();
                toInsert.Add(new KeyValuePair<string, int>("Error: invalid raul", -1));
                Assert.AreEqual(-1, service.basketCheckout("telaviv", "raul"));
                Assert.AreEqual(true, compare_List(toInsert, service.payForBasket(1, new DateTime(1990, 1, 1), "raul")));
            }
            Assert.AreEqual(true, true);
            service.shutDown();

        }

        [TestMethod]
        public void TestMethod_PurchaseOfAProductIsNotInAccordanceWithThePurchasingPolicy()
        {
            Setup();
            service.signIn("raul", "1234");
            service.addComplexPurchasePolicy("(0,2,0,10,0)", "adidas", "raul");
            List<KeyValuePair<string, int>> toInsert = new List<KeyValuePair<string, int>>();
            toInsert.Add(new KeyValuePair<string, int>("p1", 11));
            service.addProductsToCart(toInsert, "adidas", "raul");
            Assert.AreEqual(-1, service.basketCheckout("Tel Aviv", "raul"));
            service.shutDown();

        }

        private bool containsNeg(List<KeyValuePair<string, int>> list)
        {
            foreach (KeyValuePair<string, int> p in list)
            {
                if (p.Value == -1)
                {
                    return true;
                }
            }
            return false;
        }

        //Not relevant right now because there are no discounts
        [TestMethod]
        public void TestMethod_PurchasePproductWithDiscountInStockAndIsSuitableForPurchasePolicy()
        {
            Setup();
            service.signIn("raul", "1234");
            List<KeyValuePair<string, int>> productsForDiscounts = new List<KeyValuePair<string, int>>();
            productsForDiscounts.Add(new KeyValuePair<string, int>("p1", 0));
            service.addRevealedDiscountPolicy(productsForDiscounts, "0.5", "30", "0", "raul", "adidas");
            List<KeyValuePair<string, int>> toInsert = new List<KeyValuePair<string, int>>();
            toInsert.Add(new KeyValuePair<string, int>("p1", 11));
            service.addProductsToCart(toInsert, "adidas", "raul");
            Assert.AreEqual(155, service.basketCheckout("Tel Aviv", "raul"));
            service.shutDown();
        }

        [TestMethod]
        public void TestMethod_PurchaseOfProduct_succ()
        {
            Setup();
            List<KeyValuePair<string, int>> toInsert = new List<KeyValuePair<string, int>>();
            toInsert.Add(new KeyValuePair<string, int>("p1", 1));
            if (!service.addProductsToCart(toInsert, "store", "raul") || !service.editProductQuantityInCart("p1", 1, "store", "raul"))
                Assert.AreEqual(false, true);
            Assert.AreEqual(60, service.basketCheckout("telaviv", "raul"));
            Assert.AreEqual(true, compare_List(toInsert, service.payForBasket(1, new DateTime(1990, 1, 1), "raul")));
            service.shutDown();
        }


        [TestMethod]
        public void TestMethod_SuccessfulPurchaseWithProblematicAddress()
        {
            Setup();
            List<KeyValuePair<string, int>> toInsert = new List<KeyValuePair<string, int>>();
            toInsert.Add(new KeyValuePair<string, int>("p1", 1));
            if (!service.addProductsToCart(toInsert, "store", "raul") || !service.editProductQuantityInCart("p1", 1, "store", "raul"))
                Assert.AreEqual(false, true);
            Assert.AreEqual(110, service.basketCheckout("ramat Gan", "raul"));
            Assert.AreEqual(true, compare_List(toInsert, service.payForBasket(1, new DateTime(1990, 1, 1), "raul")));
            service.shutDown();
        }

        [TestMethod]
        public void TestMethod_PurchaseOfProduct_PurchaseProductWithQuantityZero()
        {
            Setup();
            List<KeyValuePair<string, int>> toInsert = new List<KeyValuePair<string, int>>();
            toInsert.Add(new KeyValuePair<string, int>("p1", 0));

            Assert.AreEqual(0, service.basketCheckout("telaviv", "raul"));
            Assert.AreEqual(true, compare_List(new List<KeyValuePair<string, int>>(), service.payForBasket(1, new DateTime(1990, 1, 1), "raul")));
            service.shutDown();
        }

        public Boolean compare_List(List<KeyValuePair<string, int>> exepted, List<String[]> actuale)
        {
            if (actuale.Count == 1 && actuale[0][0].Equals("Error: invalid raul") && exepted.Count == 1)
                return true;
            if (exepted.Count != actuale.Count)
            {
                Console.WriteLine("Exepted to get list with " + exepted.Capacity + " members but got " + actuale.Capacity + "\n");
                return false;

            }
            int i = 0;
            while (i < exepted.Count)
            {
                if (actuale[i].Length != 5)
                {
                    Console.WriteLine("Exepted to get memebers in list with " + exepted.Capacity + " members but got " + actuale.Capacity + "\n");
                    return false;
                }
                Assert.AreEqual(actuale[i][0], exepted[i].Key);
                Assert.AreEqual(int.Parse(actuale[i][2]), exepted[i].Value);

                i++;
            }
            service.shutDown();
            return true;
        }
    }
}
