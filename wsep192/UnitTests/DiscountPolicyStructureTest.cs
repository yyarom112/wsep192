using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;

namespace UnitTests
{
    [TestClass]
    public class DiscountPolicyStructureTest
    {
        private RevealedDiscount rd;

        private Product p1;
        private Store store;
        private ProductInStore pis1;

        public void sutup()
        {
            p1 = new Product(0, "p1", null, null, 1);
            store = new Store(7, "a");
            pis1 = new ProductInStore(20, store, p1);
            Dictionary<int, KeyValuePair<ProductInStore, int>> productList = new Dictionary<int, KeyValuePair<ProductInStore, int>>();
            productList.Add(p1.Id, new KeyValuePair<ProductInStore, int>(pis1, 1));
            rd = new RevealedDiscount(1, 0.5, productList, new DateTime(2222, 1, 1), DuplicatePolicy.WithMultiplication);
        }

        //----------------------@@ Revealed Discount @@--------------------------
        [TestMethod]
        public void RevealedDiscount_checkCondition()
        {
            sutup();
            List<KeyValuePair<ProductInStore, int>> productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(new ProductInStore(5, store, new Product(-5, null, null, null, -15)), 2));
            Assert.AreEqual(false, rd.checkCondition(productToBuy),"check not relevant product");

            productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis1, 2));
            Assert.AreEqual(true, rd.checkCondition(productToBuy));

        }

        [TestMethod]
        public void RevealedDiscount_calculate()
        {
            sutup();
            List<KeyValuePair<ProductInStore, int>> productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(new ProductInStore(5, store, new Product(-5, null, null, null, -15)), 2));
            Assert.AreEqual(0, rd.calculate(productToBuy), "check not relevant product");

            productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis1, 2));
            Assert.AreEqual(10, rd.calculate(productToBuy));

        }
    }
}
