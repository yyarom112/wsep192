using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;

namespace UnitTests
{
    [TestClass]
    public class RemoveProductsFromCartTests1
    {
        

           TradingSystem system;
            User user;
            Store store;
            Product product;
            List<KeyValuePair<int, int>> productsToRemove;


            public void setup()
            {
                system = new TradingSystem(null, null);
                user = new User(1, "user", "1234", false, false);
                store = new Store(1, "store", 0, null, null);
                product = new Product(1, "product", null, null, -1);
            }


            private void successsetup()
            {
                system.Users.Add(user.Id, user);
                system.Stores.Add(store.Id, store);
                user.Basket.ShoppingCarts.Add(store.Id, new ShoppingCart(store.Id, store));
                ProductInCart pc = new ProductInCart(2, user.Basket.ShoppingCarts[store.Id], product);
                user.Basket.ShoppingCarts[store.Id].Products.Add(product.Id, pc);
                productsToRemove = new List<KeyValuePair<int, int>>();
                KeyValuePair<int, int> pair = new KeyValuePair<int, int>(product.Id, 1);
                productsToRemove.Add(pair);


            }




            [TestMethod]
            public void TestMethod1_testmethod_failure_remove()
            {
                setup();

                //failure system remove
                Assert.AreEqual(false, system.removeProductsFromCart(null, store.Id, user.Id));
                system.Users.Add(user.Id, user);
                Assert.AreEqual(false, system.removeProductsFromCart(null, store.Id, user.Id));
                system.Users.Remove(user.Id);
                system.Stores.Add(store.Id, store);
                Assert.AreEqual(false, system.removeProductsFromCart(null, store.Id, user.Id));

                //failure basket remove
                Assert.AreEqual(false, user.Basket.removeProductsFromCart(null, store.Id));

                //failure cart remove
                user.Basket.ShoppingCarts.Add(store.Id, new ShoppingCart(store.Id, store));
                productsToRemove = new List<KeyValuePair<int, int>>();
                KeyValuePair<int, int> pair = new KeyValuePair<int, int>(product.Id, 5);
                productsToRemove.Add(pair);
                Assert.AreEqual(false, user.Basket.ShoppingCarts[store.Id].removeProductsFromCart(productsToRemove)); //empty list of products in cart
                ProductInCart pc = new ProductInCart(2, user.Basket.ShoppingCarts[store.Id], product);
                user.Basket.ShoppingCarts[store.Id].Products.Add(product.Id, pc);
                Assert.AreEqual(false, user.Basket.ShoppingCarts[store.Id].removeProductsFromCart(productsToRemove)); //too many to remove




            }


            [TestMethod]
            public void testmethod_cart_success_remove()
            {
                setup();
                successsetup();
                Assert.AreEqual(true, user.Basket.ShoppingCarts[store.Id].removeProductsFromCart(productsToRemove));
                Assert.AreEqual(1, user.Basket.ShoppingCarts[store.Id].Products[product.Id].Quantity);
            }

            [TestMethod]
            public void testmethod_basket_success_remove()
            {
                setup();
                successsetup();
                Assert.AreEqual(true, user.Basket.removeProductsFromCart(productsToRemove, store.Id));
                Assert.AreEqual(1, user.Basket.ShoppingCarts[store.Id].Products[product.Id].Quantity);
            }

            [TestMethod]
            public void testmethod_system_success_remove()
            {
                setup();
                successsetup();
                Assert.AreEqual(true, system.removeProductsFromCart(productsToRemove, store.Id, user.Id));
                Assert.AreEqual(1, user.Basket.ShoppingCarts[store.Id].Products[product.Id].Quantity);
            }


    }
}
