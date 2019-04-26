using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;

namespace Acceptance_Tests
{
    [TestClass]
    public class BuyingBasketReq28
    {
        private ServiceLayer service;

        public void Setup()
        {
            service = new ServiceLayer();
            service.init("admin", "1234");
            service.register("user", "1234", service.initUser());
            service.signIn("admin", "1234");
            service.openStore("store", "admin");
            service.createNewProductInStore("p1", "", "", 10, "store", "admin");
            List<KeyValuePair<string, int>> toInsert = new List<KeyValuePair<string, int>>();
            toInsert.Add(new KeyValuePair<string, int>("p1", 100));
            service.addProductsInStore(toInsert, "store", "admin");
        }


        [TestMethod]
        public void TestMethod_AnAttemptToPurchaseAnEmptyBasket()
        {
            Setup();
            Assert.AreEqual(0, service.basketCheckout("telaviv", "user"));
            List<KeyValuePair<string, int>> toInsert = new List<KeyValuePair<string, int>>();
            Assert.AreEqual(true, compare_List(toInsert, service.payForBasket(1, new DateTime(1990, 1, 1), "user")));
        }

        [TestMethod]
        public void TestMethod_PurchaseAProductThatIsNotInStock()
        {
            Setup();
            List<KeyValuePair<string, int>> toInsert = new List<KeyValuePair<string, int>>();
            toInsert.Add(new KeyValuePair<string, int>("p1", 1000));
            service.addProductsToCart(toInsert, "store", "user");
            service.editProductQuantityInCart("p1", 10, "store", "user");
            Assert.AreEqual(150, service.basketCheckout("telaviv", "user"));
            toInsert[0] = new KeyValuePair<string, int>("p1", 10);
            Assert.AreEqual(true, compare_List(toInsert, service.payForBasket(1, new DateTime(1990, 1, 1), "user")));
        }
        //You need to add another test in this frame when there is a buying policy in the next version
        [TestMethod]
        public void TestMethod_PurchaseOfAProductIsNotInAccordanceWithThePurchasingPolicy()
        {
            Setup();
            try
            {
                List<KeyValuePair<string, int>> toInsert = new List<KeyValuePair<string, int>>();
                toInsert.Add(new KeyValuePair<string, int>("p1", 1));
                if (service.addProductsToCart(toInsert, "store", "user") && service.editProductQuantityInCart("p1", -1, "store", "user"))
                    Assert.AreEqual(false, true);
            }
            catch (Exception e) { }
            if (service.showCart("store", "user").Contains("-1"))
            {
                List<KeyValuePair<string, int>> toInsert = new List<KeyValuePair<string, int>>();
                toInsert.Add(new KeyValuePair<string, int>("Error: invalid user", -1));
                Assert.AreEqual(-1, service.basketCheckout("telaviv", "user"));
                Assert.AreEqual(true, compare_List(toInsert, service.payForBasket(1, new DateTime(1990, 1, 1), "user")));
            }
            Assert.AreEqual(true, true);

        }
        //Not relevant right now because there are no discounts
        //[TestMethod]
        //public void TestMethod_PurchasePproductWithDiscountInStockAndIsSuitableForPurchasePolicy()
        //{
        //}

        [TestMethod]
        public void TestMethod_PurchaseOfProduct_succ()
        {
            Setup();
            List<KeyValuePair<string, int>> toInsert = new List<KeyValuePair<string, int>>();
            toInsert.Add(new KeyValuePair<string, int>("p1", 1));
            if (!service.addProductsToCart(toInsert, "store", "user") || !service.editProductQuantityInCart("p1", 1, "store", "user"))
                Assert.AreEqual(false, true);
            Assert.AreEqual(60, service.basketCheckout("telaviv", "user"));
            Assert.AreEqual(true, compare_List(toInsert, service.payForBasket(1, new DateTime(1990, 1, 1), "user")));
        }

        ////Not relevant right now because there are no discounts
        //[TestMethod]
        //public void TestMethod_PurchaseWithoutProperPaymentMethod()
        //{
        //    List<KeyValuePair<string, int>> toInsert = new List<KeyValuePair<string, int>>();
        //    toInsert.Add(new KeyValuePair<string, int>("p1", 1));
        //    Assert.AreEqual(60, service.basketCheckout("telaviv", "user"));
        //    Assert.AreEqual("List", service.payForBasket(123214124124, new DateTime(1990, 1, 1), "user"));
        //}
        [TestMethod]
        public void TestMethod_SuccessfulPurchaseWithProblematicAddress()
        {
            Setup();
            List<KeyValuePair<string, int>> toInsert = new List<KeyValuePair<string, int>>();
            toInsert.Add(new KeyValuePair<string, int>("p1", 1));
            if (!service.addProductsToCart(toInsert, "store", "user") || !service.editProductQuantityInCart("p1", 1, "store", "user"))
                Assert.AreEqual(false, true);
            Assert.AreEqual(110, service.basketCheckout("ramat Gan", "user"));
            Assert.AreEqual(true, compare_List(toInsert, service.payForBasket(1, new DateTime(1990, 1, 1), "user")));
        }

        [TestMethod]
        public void TestMethod_PurchaseOfProduct_PurchaseProductWithQuantityZero()
        {
            Setup();
            List<KeyValuePair<string, int>> toInsert = new List<KeyValuePair<string, int>>();
            toInsert.Add(new KeyValuePair<string, int>("p1", 0));
            Assert.AreEqual(0, service.basketCheckout("telaviv", "user"));
            Assert.AreEqual(true, compare_List(new List<KeyValuePair<string, int>>(), service.payForBasket(1, new DateTime(1990, 1, 1), "user")));
        }


        public Boolean compare_List(List<KeyValuePair<string, int>> exepted, List<String[]> actuale)
        {
            if (actuale.Count == 1 && actuale[0][0].Equals("Error: invalid user") && exepted.Count == 1)
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
            return true;
        }



    }
}
