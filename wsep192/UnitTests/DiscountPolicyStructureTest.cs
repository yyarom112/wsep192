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

        private LeafCondition lc1;
        private LeafCondition lc2;
        LogicalCondition logcAnd;
        LogicalCondition logcOr;
        LogicalCondition logcXor;




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
            Dictionary<int, ProductInStore> discountproducts = new Dictionary<int, ProductInStore>();
            discountproducts.Add(p1.Id, pis1);
            discountproducts.Add(p2.Id, pis2);
            lc = new LeafCondition(relatedProducts,10,0.5, discountproducts, new DateTime(2222,1,1),DuplicatePolicy.WithMultiplication);
            relatedProducts = new Dictionary<int, KeyValuePair<ProductInStore, int>>();
            relatedProducts.Add(p1.Id, new KeyValuePair<ProductInStore, int>(pis1, 1));
            lc1 = new LeafCondition(relatedProducts, 10, 0.5, discountproducts, new DateTime(2222, 1, 1), DuplicatePolicy.WithMultiplication);

            relatedProducts = new Dictionary<int, KeyValuePair<ProductInStore, int>>();
            relatedProducts.Add(p2.Id, new KeyValuePair<ProductInStore, int>(pis2, 2));
            lc2 = new LeafCondition(relatedProducts, 10, 0.5, discountproducts, new DateTime(2222, 1, 1), DuplicatePolicy.WithMultiplication);

            logcAnd = new LogicalCondition(10, 0.5, discountproducts, new DateTime(2222, 1, 1), DuplicatePolicy.WithMultiplication, LogicalConnections.and);
            logcAnd.addChild(0, lc1);
            logcAnd.addChild(1, lc2);

            logcOr = new LogicalCondition(10, 0.5, discountproducts, new DateTime(2222, 1, 1), DuplicatePolicy.WithMultiplication, LogicalConnections.or);
            logcOr.addChild(0, lc1);
            logcOr.addChild(1, lc2);

            logcXor = new LogicalCondition(10, 0.5, discountproducts, new DateTime(2222, 1, 1), DuplicatePolicy.WithMultiplication, LogicalConnections.xor);
            logcXor.addChild(0, lc1);
            logcXor.addChild(1, lc2);

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


        [TestMethod]
        public void RevealedDiscount_UpdateProductPrice()
        {
            sutup();
            List<KeyValuePair<ProductInStore, int>> productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(new ProductInStore(5, store, new Product(-5, null, null, null, -15)), 2));
            Assert.AreEqual(0, rd.calculate(productToBuy), "check not relevant product");

            productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis1, 1));
            rd.UpdateProductPrice(productToBuy);
            Assert.AreEqual(10, pis1.Product.Price);

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
        //----------------------@@ Logical Condition @@--------------------------

        [TestMethod]
        public void LogicalCondition_checkCondition_and()
        {
            sutup();
            List<KeyValuePair<ProductInStore, int>> productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            Assert.AreEqual(false,logcAnd.checkCondition(productToBuy), "No one is satisfied");

            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis1, 1));
            Assert.AreEqual(false, logcAnd.checkCondition(productToBuy), "first satisfied");

            productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis2, 2));
            Assert.AreEqual(false, logcAnd.checkCondition(productToBuy), "second satisfied");

            productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis1, 1));
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis2, 2));
            Assert.AreEqual(true, logcAnd.checkCondition(productToBuy), "second satisfied");
        }

        [TestMethod]
        public void LogicalCondition_checkCondition_or()
        {
            sutup();
            List<KeyValuePair<ProductInStore, int>> productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            Assert.AreEqual(false, logcOr.checkCondition(productToBuy), "No one is satisfied");

            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis1, 1));
            Assert.AreEqual(true, logcOr.checkCondition(productToBuy), "first satisfied");

            productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis2, 2));
            Assert.AreEqual(true, logcOr.checkCondition(productToBuy), "second satisfied");

            productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis1, 1));
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis2, 2));
            Assert.AreEqual(true, logcOr.checkCondition(productToBuy), "second satisfied");
        }

        [TestMethod]
        public void LogicalCondition_checkCondition_xor()
        {
            sutup();
            List<KeyValuePair<ProductInStore, int>> productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            Assert.AreEqual(false, logcXor.checkCondition(productToBuy), "No one is satisfied");

            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis1, 1));
            Assert.AreEqual(true, logcXor.checkCondition(productToBuy), "first satisfied");

            productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis2, 2));
            Assert.AreEqual(true, logcXor.checkCondition(productToBuy), "second satisfied");

            productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis1, 1));
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis2, 2));
            Assert.AreEqual(false, logcXor.checkCondition(productToBuy), "second satisfied");
        }


        //----------------------@@ Conditional Discount @@--------------------------

        [TestMethod]
        public void ConditionalDiscount_calculate()
        {
            sutup();
            List<KeyValuePair<ProductInStore, int>> productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            Assert.AreEqual(0, logcAnd.calculate(productToBuy), "empty list");
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis1, 1));
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis2, 2));
            Assert.AreEqual(30, logcAnd.calculate(productToBuy), "get discount");

        }

        [TestMethod]
        public void ConditionalDiscount_calculate_fail()
        {
            sutup();
            List<KeyValuePair<ProductInStore, int>> productToBuy = new List<KeyValuePair<ProductInStore, int>>();
            Assert.AreEqual(0, logcAnd.calculate(productToBuy), "empty list");
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis1, 1));
            productToBuy.Add(new KeyValuePair<ProductInStore, int>(pis2, 0));
            Assert.AreEqual(20, logcAnd.calculate(productToBuy), "get discount");

        }


    }
}
