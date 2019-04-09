using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;

namespace Acceptance_Tests
{
    [TestClass]
    public class addProductToCart
    {

        


        }
        [TestMethod]
        public void TestMethod1_succAddProductsToCartWithSave()
        {
            setUp();

            List<KeyValuePair<int, int>> toInsert = new List<KeyValuePair<int, int>>();

            toInsert.Add(new KeyValuePair<int, int>(p1.Id, 1));
            toInsert.Add(new KeyValuePair<int, int>(p2.Id, 1));
            toInsert.Add(new KeyValuePair<int, int>(p3.Id, 1));

            Assert.AreEqual(true, sys.addProductsToCart(toInsert, store.Id, user.Id));

            toInsert = new List<KeyValuePair<int, int>>();
            toInsert.Add(new KeyValuePair<int, int>(p4.Id, 1));

            Assert.AreEqual(true, sys.addProductsToCart(toInsert, store.Id, user.Id));


        }

        [TestMethod]
        public void TestMethod1_succAddProductsToCartWitoutSave()
        {
            
        }
    }
}
