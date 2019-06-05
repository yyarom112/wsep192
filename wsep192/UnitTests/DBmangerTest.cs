using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.DataLayer;
using src.Domain;

namespace UnitTests
{
    [TestClass]
    public class DBmangerTest
    {


        private User admin;
        private ShoppingBasket basket_admin;

        private User user;
        private ShoppingBasket basket_user;


        private Product p1;
        private Product p2;
        private Product p3;
        private Product p4;
        private ProductInStore pis1;
        private ProductInStore pis2;
        private ProductInStore pis3;
        private ProductInStore pis4;

        



        private Store store;


        public void setUp()
        {
            admin = new User(0, "admin", "123456", true, true);
            basket_admin = admin.Basket;
            user = new User(1, null, null, false, false);
            basket_user = user.Basket;

            store = new Store(-1, "store");

            p1 = new Product(0, "first", "", "", 5000);
            p2 = new Product(1, "second", "", "", 5000);
            p3 = new Product(2, "third", "", "", 5000);
            p4 = new Product(3, "fourth", "", "", 5000);
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
        public void TestMethod_usertableTest()
        {
            DBmanager db = new DBmanager();
            User user = new User(0, "raul", "1234", false, true);
            user.State = state.signedIn;

            Assert.AreEqual(true, db.addNewUser(user), "Add to user table failed");

            Assert.AreEqual(user.UserName, db.getUser(user.Id).UserName, "Get user from table failed");

            user.UserName = "segio";
            user.State = state.visitor;
            user.Password = "471421";
            user.IsAdmin = true;
            Assert.AreEqual(true, db.updateUser(user), "Update user from table failed");

            Assert.AreEqual(user.UserName, db.getUser(user.Id).UserName, "Update user from table failed");

            Assert.AreEqual(true, db.removeUser(user.Id), "Remove user from table failed");

            Assert.AreEqual(null, db.getUser(user.Id), "Remove user from table failed");

        }


        [TestMethod]
        public void TestMethod_productInCartTable()
        {
            setUp();
            DBmanager db = new DBmanager();
            user.Basket.ShoppingCarts.Add(store.Id, new ShoppingCart(store.Id, store));
            Assert.AreEqual(true, db.addProductInCart(new ProductInCart(10,user.Basket.ShoppingCarts[store.Id],p1),user.Id));
            try
            {
                Assert.AreEqual(true, db.addProductInCart(new ProductInCart(10, user.Basket.ShoppingCarts[store.Id], p1), user.Id));
                Assert.AreEqual(true, false, " Insert to table same entry");
            }
            catch (Exception e) { };
            Assert.AreEqual(10, db.getProductInCartquntity(user.Id, store.Id, p1.Id));
            Assert.AreEqual(true, db.updateProductInCart(user.Id, store.Id, p1.Id, 20));
            Assert.AreEqual(20, db.getProductInCartquntity(user.Id, store.Id, p1.Id));
            Assert.AreEqual(true, db.removeProductInCartBy(user.Id, store.Id, p1.Id));
            Assert.AreEqual(-1, db.getProductInCartquntity(user.Id, store.Id, p1.Id));
            Assert.AreEqual(true, db.addProductInCart(new ProductInCart(10, user.Basket.ShoppingCarts[store.Id], p1), user.Id));
            Assert.AreEqual(true, db.removeAllProductInCartByStoreId(store.Id));
            Assert.AreEqual(-1, db.getProductInCartquntity(user.Id, store.Id, p1.Id));
            Assert.AreEqual(true, db.addProductInCart(new ProductInCart(10, user.Basket.ShoppingCarts[store.Id], p1), user.Id));
            Assert.AreEqual(true, db.removeAllProductInCartByUserId(user.Id));
            Assert.AreEqual(-1, db.getProductInCartquntity(user.Id, store.Id, p1.Id));
        }

        [TestMethod]
        public void TestMethod_ProductInStoreTable()
        {
            setUp();
            DBmanager db = new DBmanager();
            user.Basket.ShoppingCarts.Add(store.Id, new ShoppingCart(store.Id, store));
            Assert.AreEqual(true, db.addNewProductInStore(new ProductInStore(10,store,p1)));
            try
            {
                Assert.AreEqual(true, db.addNewProductInStore(new ProductInStore(10, store, p1)));
                Assert.AreEqual(true, false, " Insert to table same entry");
            }
            catch (Exception e) { };
            Assert.AreEqual(10, db.getProductInStoreQuntity(store.Id, p1.Id));
            Assert.AreEqual(true, db.updateProductInStore( store.Id, p1.Id, 20));
            Assert.AreEqual(20, db.getProductInStoreQuntity(store.Id, p1.Id));
            Assert.AreEqual(true, db.removeProductInStore(store.Id, p1.Id));
            Assert.AreEqual(-1, db.getProductInCartquntity(user.Id, store.Id, p1.Id));
        }
    }
}
