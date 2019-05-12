using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class addProductToCart
    {
        private TradingSystem sys;
        private Encryption encrypt;

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

            p1 = new Product(0, "first", null, "", 5000);
            p2 = new Product(1, "second", null, "", 5000);
            p3 = new Product(2, "third", null, "", 5000);
            p4 = new Product(3, "fourth", null, "", 5000);
            pis1 = new ProductInStore(10000000, store, p1);
            pis2 = new ProductInStore(10000000, store, p2);
            pis3 = new ProductInStore(10000000, store, p3);
            pis4 = new ProductInStore(10000000, store, p4);
            store.Products.Add(p1.Id, pis1);
            store.Products.Add(p2.Id, pis2);
            store.Products.Add(p3.Id, pis3);
            store.Products.Add(p4.Id, pis4);
            sys = new TradingSystem(null,null);
            sys.StoreCounter = 1;
            sys.ProductCounter = 4;
            sys.UserCounter = 2;
            sys.Stores.Add(store.Id, store);
            sys.Users.Add(admin.Id, admin);
            sys.Users.Add(user.Id, user);


        }
        [TestMethod]
        public void TestMethod1_cart_successSenrio()
        {
            setUp();
            ShoppingCart cart = new ShoppingCart(store.Id, store);
            LinkedList<KeyValuePair<Product, int>> toInsert = new LinkedList<KeyValuePair<Product, int>>();
            Assert.AreEqual(0, cart.Products.Count);
            cart.addProducts(toInsert);
            Assert.AreEqual(0, cart.Products.Count);

            toInsert.AddLast(new KeyValuePair<Product, int>(this.p1, 10000000));

            cart.addProducts(toInsert);
            Assert.AreEqual(1, cart.Products.Count);
            Assert.AreEqual(true, cart.Products.ContainsKey(key: 0));
        }

        [TestMethod]
        public void TestMethod1_cart_updateSenrio()
        {
            setUp();
            ShoppingCart cart = new ShoppingCart(store.Id, store);

            LinkedList<KeyValuePair<Product, int>> toInsert = new LinkedList<KeyValuePair<Product, int>>();
            Assert.AreEqual(0, cart.Products.Count);
            cart.addProducts(toInsert);
            Assert.AreEqual(0, cart.Products.Count);

            toInsert.AddLast(new KeyValuePair<Product, int>(this.p1, 10));

            cart.addProducts(toInsert);
            Assert.AreEqual(1, cart.Products.Count);
            Assert.AreEqual(true, cart.Products.ContainsKey(key: 0));
            Assert.AreEqual(10, cart.Products[0].Quantity);


            cart.addProducts(toInsert);
            Assert.AreEqual(1, cart.Products.Count);
            Assert.AreEqual(20, cart.Products[0].Quantity);
        }

        [TestMethod]
        public void TestMethod1_basket_successSenrio()
        {
            setUp();
            Assert.AreEqual(0, basket_user.ShoppingCarts.Count);
            LinkedList<KeyValuePair<Product, int>> toInsert = new LinkedList<KeyValuePair<Product, int>>();
            Assert.AreEqual(null, basket_user.addProductsToCart(toInsert, store.Id));

            toInsert.AddLast(new KeyValuePair<Product, int>(this.p1, 10000000));

            Assert.AreEqual(0, basket_user.ShoppingCarts.Count);
            Assert.AreEqual(expected: store.Id, actual: basket_user.addProductsToCart(toInsert, store.Id).StoreId);
            Assert.AreEqual(1, basket_user.ShoppingCarts.Count);
        }


        [TestMethod]
        public void TestMethod1_basket_updateSenrio()
        {
            setUp();
            Assert.AreEqual(0, basket_user.ShoppingCarts.Count);
            LinkedList<KeyValuePair<Product, int>> toInsert = new LinkedList<KeyValuePair<Product, int>>();
            Assert.AreEqual(null, basket_user.addProductsToCart(toInsert, store.Id));

            toInsert.AddLast(new KeyValuePair<Product, int>(this.p1, 10));

            Assert.AreEqual(0, basket_user.ShoppingCarts.Count);
            Assert.AreEqual(expected: store.Id, actual: basket_user.addProductsToCart(toInsert, store.Id).StoreId);
            Assert.AreEqual(1, basket_user.ShoppingCarts.Count);


            StubCart cart = new StubCart(-1, null,10);
            cart.copy(basket_user.ShoppingCarts[store.Id]);
            basket_user.ShoppingCarts[store.Id] = cart;


            Assert.AreEqual(1, basket_user.ShoppingCarts.Count);
            Assert.AreEqual(10, basket_user.ShoppingCarts[cart.StoreId].Products[this.p1.Id].Quantity);

            Assert.AreEqual(expected: null, actual: basket_user.addProductsToCart(toInsert, store.Id));
            Assert.AreEqual(1, basket_user.ShoppingCarts.Count);
            Assert.AreEqual(20, basket_user.ShoppingCarts[cart.StoreId].Products[this.p1.Id].Quantity);


        }

        [TestMethod]
        public void TestMethod1_system_successSenrio()
        {
            setUp();
            stubUser stubUser = new stubUser(2, null, null, false, false);
            sys.Users.Add(2, stubUser);


            List<KeyValuePair<int, int>> toInsert = new List<KeyValuePair<int, int>>();

            toInsert.Add(new KeyValuePair<int, int>(p1.Id,1));
            toInsert.Add(new KeyValuePair<int, int>(p2.Id, 1));
            toInsert.Add(new KeyValuePair<int, int>(p3.Id, 1));
            Assert.AreEqual(false, ((stubUser)sys.Users[stubUser.Id]).Carts_entrys.ContainsKey(store.Id));

            Assert.AreEqual(true, sys.addProductsToCart(toInsert, store.Id, stubUser.Id));

            toInsert = new List<KeyValuePair<int, int>>();
            toInsert.Add(new KeyValuePair<int, int>(p4.Id, 1));

            Assert.AreEqual(true, sys.addProductsToCart(toInsert, store.Id, stubUser.Id));

        }


        [TestMethod]
        public void TestMethod1_system_failSenrio()
        {
            setUp();
            stubUser stubUser = new stubUser(2, null, null, false, false);
            sys.Users.Add(2, stubUser);

            Assert.AreEqual(false, sys.addProductsToCart(null, store.Id, stubUser.Id));


            List<KeyValuePair<int, int>> toInsert = new List<KeyValuePair<int, int>>();


            Assert.AreEqual(false, sys.addProductsToCart(toInsert, 10, stubUser.Id));
            Assert.AreEqual(false, sys.addProductsToCart(toInsert, store.Id, 10));
            toInsert.Add(new KeyValuePair<int, int>(10, 10));
            Assert.AreEqual(false, sys.addProductsToCart(toInsert, store.Id, stubUser.Id));


        }
    }

    internal class StubCart: ShoppingCart
    {
        private int retval;
        public StubCart(int storeId, Store store, int ret):base(storeId,store)
        {
            retval = ret;
        }

        public void copy(ShoppingCart cart)
        {
            this.StoreId = cart.StoreId;
            this.Store = cart.Store;
            this.Products = cart.Products;

            cart.StoreId = -1;
            cart.Products = null;
            cart.Store = null;
        }

        //only update
        public override void addProducts(LinkedList<KeyValuePair<Product, int>> productsToInsert)
        {
            foreach (KeyValuePair<Product, int> toInsert in productsToInsert)
            {
                if (this.Products.ContainsKey(toInsert.Key.Id))
                {
                    Products[toInsert.Key.Id].Quantity = this.Products[toInsert.Key.Id].Quantity + toInsert.Value;
                }
            }

        }
        public override double cartCheckout(src.Domain.Dataclass.UserDetailes user)
        {
            return  retval;
        }
    }


    internal class stubUser : User
    {
        Dictionary<int,int> carts_entrys;

        public stubUser(int id, string userName, string password, bool isAdmin, bool isRegistered) : base(id,userName,password,isAdmin,isRegistered)
        {
            Carts_entrys = new Dictionary<int, int>();
        }

        public Dictionary<int, int> Carts_entrys { get => carts_entrys; set => carts_entrys = value; }


        //only update
        public ShoppingCart AddProductsToCart(LinkedList<KeyValuePair<Product, int>> productsToInsert, int storeId)
        {
            if (Carts_entrys.ContainsKey(storeId))
            {
                Carts_entrys[storeId]++;
                return null;
            }
            else
            {
                Carts_entrys.Add(storeId, 1);
                return new ShoppingCart(storeId, null);
            }
        }

    }
}
