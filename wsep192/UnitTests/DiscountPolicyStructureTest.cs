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
        private ProductInCart pic1;

        public void sutup()
        {
            p1 = new Product(0, "p1", null, null, 1);
            store = new Store(7, "a");
            pis1 = new ProductInStore(20, store, p1);
            Dictionary<int, KeyValuePair<ProductInStore, int>> productList = new Dictionary<int, KeyValuePair<ProductInStore, int>>();
            productList.Add(p1.Id, new KeyValuePair<ProductInStore, int>(pis1, 1));
            rd =new RevealedDiscount(1,0.5, productList,new DateTime(2222,1,1),)
        }

        //----------------------@@ Revealed Discount @@--------------------------
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
