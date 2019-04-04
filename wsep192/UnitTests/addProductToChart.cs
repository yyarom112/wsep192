using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    class addProductToChart
    {
        private ShoppingCart cart;
        private Store store;
        private Product p1;
        private Product p2;
        private Product p3;
        private Product p4;
        private ProductInStore pis1;
        private ProductInStore pis2;
        private ProductInStore pis3;
        private ProductInStore pis4;

        public void setUp()
        {
            store = new Store(1314,"blabla",0,new User());
            cart = new ShoppingCart(store.Id, store);
            cart.Store = store;
            p1 = new Product(-1, "first", null, "", 5000, 0);
            p2 = new Product(-2, "second", null, "", 5000, 0);
            p3 = new Product(-3, "third", null, "", 5000, 0);
            p4 = new Product(-4, "fourth", null, "", 5000, 0);
            pis1 = new ProductInStore(10000000, store, p1);
            pis2 = new ProductInStore(10000000, store, p2);
            pis3 = new ProductInStore(10000000, store, p3);
            pis4 = new ProductInStore(10000000, store, p4);
            store.Products.Add(p1.Id, pis1);
            store.Products.Add(p2.Id, pis2);
            store.Products.Add(p3.Id, pis3);
            store.Products.Add(p4.Id, pis4);
        }


        [TestMethod]
        //insert product to chart in succseess senario
        public void ShoppingCart_addProducts_succ()
        {
            setUp();
            LinkedList<KeyValuePair<Product, int>> toInsert = new LinkedList<KeyValuePair<Product, int>>();
            Assert.AreEqual(0, cart.Products.Count);
            cart.addProducts(toInsert);
            Assert.AreEqual(0, cart.Products.Count);

            toInsert.AddLast(new KeyValuePair<Product, int>(this.p1, 10000000 ));

            cart.addProducts(toInsert);
            Assert.AreEqual(1, cart.Products.Count);
            Assert.AreEqual(true, cart.Products.ContainsKey(-1));
        }
    }
}
