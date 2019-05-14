using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;
using System.Collections.Generic;

namespace UnitTests
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

        [TestMethod]
        public void TestMethod_failure_edit_system()
        {
            setUp();
            Assert.AreEqual(false, system.editProductQuantityInCart(product.Id, 3, store.Id, user.Id));
            system.Users.Add(user.Id, null);
            Assert.AreEqual(false, system.editProductQuantityInCart(product.Id, 3, store.Id, user.Id));
            system.Users.Remove(user.Id);
            system.Stores.Add(store.Id, null);
            Assert.AreEqual(false, system.editProductQuantityInCart(product.Id, 3, store.Id, user.Id));

        }
        [TestMethod]
        public void TestMethod_failure_edit_basket()
        {
            setUp();
            ShoppingBasket basket = new ShoppingBasket();
            Assert.AreEqual(false, basket.editProductQuantityInCart(product.Id, 3, store.Id));
        }

        [TestMethod]
        public void TestMethod_failure_edit_cart()
        {
            setUp();
            ShoppingCart cart =  new ShoppingCart(store.Id, null);
            Assert.AreEqual(false, cart.editProductQuantityInCart(product.Id, 3));
        }




        [TestMethod]
        public void TestMethod_cart_success_edit()
        {
            setUp();
            ShoppingCart cart = new ShoppingCart(store.Id, null);
            ProductInCart pc = new ProductInCart(2, cart, null);
            cart.Products.Add(product.Id, pc);
            Assert.AreEqual(true, cart.editProductQuantityInCart(product.Id, 3));
            Assert.AreEqual(3, cart.Products[product.Id].Quantity);
        }

        [TestMethod]
        public void TestMethod_basket_success_edit()
        {
            setUp();
            ShoppingBasket basket = new ShoppingBasket();
            ShoppingCart cart = new StubCart2(store.Id, null, true);
            basket.ShoppingCarts.Add(store.Id, cart);
            Assert.AreEqual(true, basket.editProductQuantityInCart(product.Id, 3, store.Id));
            }

        [TestMethod]
        public void TestMethod_system_success_edit()
        {
            setUp();
            user = new stubUser2(1, null, null, false, false, true);
            system.Users.Add(user.Id, user);
            system.Stores.Add(store.Id, null);
            Assert.AreEqual(true, system.editProductQuantityInCart(product.Id, 3, store.Id, user.Id));
        }
    }

    internal class stubUser2 : User
    {
        bool retVal;


        public stubUser2(int id, string userName, string password, bool isAdmin, bool isRegistered, bool ret) : base(id, userName, password, isAdmin, isRegistered)
        {
            retVal = ret;
        }

        internal override bool editProductQuantityInCart(int product,int quantity, int storeId)
        {
            return retVal;
        }

    }

    internal class StubCart2 : ShoppingCart
    {
        bool retVal;

        public StubCart2(int storeId, Store store, bool ret) : base(storeId, store)
        {
            retVal = ret;
        }

        internal override bool editProductQuantityInCart(int product, int quantity)
        {
            return retVal;
        }
    }


}
