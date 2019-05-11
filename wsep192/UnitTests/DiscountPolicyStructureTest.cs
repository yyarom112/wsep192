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
        private ProductInStore pis1;

        private Product p2;
        private ProductInStore pis2;

        private Store store;
        private LeafCondition lc;

        public void sutup()
        {
            p1 = new Product(0, "p1", null, null, 20);
            store = new Store(7, "a");
            pis1 = new ProductInStore(20, store, p1);

            p2 = new Product(1, "p2", null, null, 20);
            pis2 = new ProductInStore(20, store, p2);


            Dictionary<int, KeyValuePair<ProductInStore, int>> productList = new Dictionary<int, KeyValuePair<ProductInStore, int>>();
            productList.Add(p1.Id, new KeyValuePair<ProductInStore, int>(pis1, 1));
            rd = new RevealedDiscount(1, 0.5, productList, new DateTime(2222, 1, 1), DuplicatePolicy.WithMultiplication);


            Dictionary<int, KeyValuePair<ProductInStore, int>> relatedProducts = new Dictionary<int, KeyValuePair<ProductInStore, int>>();
            relatedProducts.Add(p1.Id, new KeyValuePair<ProductInStore, int>(pis1, 1));
            relatedProducts.Add(p2.Id, new KeyValuePair<ProductInStore, int>(pis2, 2));
            lc = new LeafCondition(relatedProducts);
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
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis1, 1));
            Assert.AreEqual(10, rd.calculate(productToBuy));

        }

        //----------------------@@ Leaf Condition @@--------------------------
        [TestMethod]
        public void LeafCondition_checkCondition()
        {
            sutup();
            List<KeyValuePair<ProductInStore, int>> productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis1, 1));
            Assert.AreEqual(false, lc.checkCondition(productToBuy), "Missing products related to the discount");
            productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis1, 1));
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis2, 1));
            Assert.AreEqual(false, lc.checkCondition(productToBuy), "pis2 does not exceed the minimum amount");
            productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis1, 1));
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis2, 2));
            Assert.AreEqual(true, lc.checkCondition(productToBuy), "The success scenario");


        }

    }
}
