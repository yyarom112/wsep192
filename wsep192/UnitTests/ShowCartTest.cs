using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class ShowCartTest
    {
        TradingSystem system;
        User user;
        Store store;

        public void setUp()
        {
            system = new TradingSystem(null,null);
            user = new User(1, "user", "1234", false, false);
            store = new Store(1,"store",0,null,null);

            
           /* admin = new User(0, "admin", "1234", "ashdod", state.signedIn, true, false, new ShoppingBasket(), new Dictionary<int, Role>());
            basket_user = user.Basket;
            p1 = new Product(0, "first", null, "", 5000, 0);
            p2 = new Product(1, "second", null, "", 5000, 0);
            p3 = new Product(2, "third", null, "", 5000, 0);
            p4 = new Product(3, "fourth", null, "", 5000, 0);
            pis1 = new ProductInStore(10000000, store, p1);
            pis2 = new ProductInStore(10000000, store, p2);
            pis3 = new ProductInStore(10000000, store, p3);
            pis4 = new ProductInStore(10000000, store, p4);
            store.Products.Add(p1.Id, pis1);
            store.Products.Add(p2.Id, pis2);
            store.Products.Add(p3.Id, pis3);
            store.Products.Add(p4.Id, pis4);
            sys = new TradingSystem();
            sys.StoreCounter = 1;
            sys.ProductCounter = 4;
            sys.UserCounter = 2;
            sys.Stores.Add(store.Id, store);
            sys.Users.Add(admin.Id, admin);
            sys.Users.Add(user.Id, user);*/

        }

        [TestMethod]
        public void TestMethod_failure()
        {
            setUp();

            //failure system showCart
            Assert.AreEqual("Error : Invalid user or store",system.showCart(user.Id,store.Id));
            system.Users.Add(user.Id,user);
            Assert.AreEqual("Error : Invalid user or store", system.showCart(user.Id, store.Id));
            system.Users.Remove(user.Id);
            system.Stores.Add(store.Id, store);
            Assert.AreEqual("Error : Invalid user or store", system.showCart(user.Id, store.Id));

            //failure basket showCart
            Assert.AreEqual("Error : Shopping basket does not contain this store",
                user.Basket.showCart(store.Id));
            
        }

        [TestMethod]
        public void TestMethod_success()
        {
            setUp();
            successSetUp();
            //empty cart
            Assert.AreEqual("Store Name: store\nCart is empty\n", system.showCart(user.Id, store.Id));

            //non-empty cart
            addProducts();
            Assert.AreEqual("Store Name: store\nProduct Name\t\t\tQuantity\n"+
                "1. p1\t\t\t3\n2. p2\t\t\t1\n", 
                system.showCart(user.Id, store.Id));

        }

        private void addProducts()
        {
            Product p1 = new Product(1, "p1", null, null, -1, 0);
            Product p2 = new Product(2, "p2", null, null, -1, 0);
            ProductInCart pc1 = new ProductInCart(3, user.Basket.ShoppingCarts[store.Id], p1);
            ProductInCart pc2 = new ProductInCart(1, user.Basket.ShoppingCarts[store.Id], p2);
            user.Basket.ShoppingCarts[store.Id].Products.Add(1,pc1);
            user.Basket.ShoppingCarts[store.Id].Products.Add(2,pc2);
        }

        private void successSetUp()
        {
            system.Users.Add(user.Id, user);
            system.Stores.Add(store.Id, store);
            user.Basket.ShoppingCarts.Add(store.Id,new ShoppingCart(store.Id,store,new Dictionary<int, ProductInCart>()));
        }
    }
}
