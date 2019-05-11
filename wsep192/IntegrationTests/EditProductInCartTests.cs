using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;
using System.Collections.Generic;

namespace IntegrationTests
{
    [TestClass]
    public class EditProductInCartTests
    {
        TradingSystem system;
        User user;
        Store store;
        Product product;


        public void setUp()
        {
            system = new TradingSystem(null, null);
            user = new User(1, "user", "1234", false, false);
            store = new Store(1, "store");
            product = new Product(1, "product", null, null, -1);
        }


        private void successSetUp()
        {
            system.Users.Add(user.Id, user);
            system.Stores.Add(store.Id, store);
            user.Basket.ShoppingCarts.Add(store.Id, new ShoppingCart(store.Id, store));
            ProductInCart pc = new ProductInCart(2, user.Basket.ShoppingCarts[store.Id], product);
            user.Basket.ShoppingCarts[store.Id].Products.Add(product.Id, pc);
        }


        [TestMethod]
        public void TestMethod_failure_edit()
        {
            setUp();

            //failure system edit
            Assert.AreEqual(false, system.editProductQuantityInCart(product.Id, 3, store.Id, user.Id));
            system.Users.Add(user.Id, user);
            Assert.AreEqual(false, system.editProductQuantityInCart(product.Id, 3, store.Id, user.Id));
            system.Users.Remove(user.Id);
            system.Stores.Add(store.Id, store);
            Assert.AreEqual(false, system.editProductQuantityInCart(product.Id, 3, store.Id, user.Id));

            //failure basket edit
            Assert.AreEqual(false, user.Basket.editProductQuantityInCart(product.Id, 3, store.Id));

            //failure cart edit
            user.Basket.ShoppingCarts.Add(store.Id, new ShoppingCart(store.Id, store));
            ProductInCart pc = new ProductInCart(2, user.Basket.ShoppingCarts[store.Id], product);
            user.Basket.ShoppingCarts[store.Id].Products.Add(product.Id, pc);

            Assert.AreEqual(true, user.Basket.ShoppingCarts[store.Id].editProductQuantityInCart(product.Id, 3));

        }




        [TestMethod]
        public void TestMethod_cart_success_edit()
        {
            setUp();
            successSetUp();
            Assert.AreEqual(true, user.Basket.ShoppingCarts[store.Id].editProductQuantityInCart(product.Id, 3));
            Assert.AreEqual(3, user.Basket.ShoppingCarts[store.Id].Products[product.Id].Quantity);
        }

        [TestMethod]
        public void TestMethod_basket_success_edit()
        {
            setUp();
            successSetUp();
            Assert.AreEqual(true, user.Basket.editProductQuantityInCart(product.Id, 3, store.Id));
            Assert.AreEqual(3, user.Basket.ShoppingCarts[store.Id].Products[product.Id].Quantity);
        }

        [TestMethod]
        public void TestMethod_system_success_edit()
        {
            setUp();
            successSetUp();
            Assert.AreEqual(true, system.editProductQuantityInCart(product.Id, 3, store.Id, user.Id));
            Assert.AreEqual(3, user.Basket.ShoppingCarts[store.Id].Products[product.Id].Quantity);
        }
    }
}
