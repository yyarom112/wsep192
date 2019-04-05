using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;

namespace UnitTests
{
    [TestClass]
    public class ShowCartTest
    {
        public void setUp()
        {
            store = new Store(0, "blabla", 0, new User());
            user = new User(1, null, null, null, state.visitor, true, false, new ShoppingBasket(), new Dictionary<int, Role>());
            basket_user = user.Basket;
            admin = new User(0, "admin", "1234", "ashdod", state.signedIn, true, false, new ShoppingBasket(), new Dictionary<int, Role>());
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
            sys.Users.Add(user.Id, user);

        }



        [TestMethod]
        public void TestMethod1_success()
        {
            setUp();
        }
    }
}
