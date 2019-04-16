using System;
using System.Collections.Generic;
using src.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class Req4_1
    {
        private Store store;
        private User owner;
        private Role ownerRole;
        private User manager;
        private Role managerRole;
        private User user;
        private User notPremittedManager;
        private Role notPremittedManagerRole;
        private Product p;
        private ProductInStore pis;

        public void setUp()
        {
            store = new Store(2, "ZARA", new List<PurchasePolicy>(), new List<DiscountPolicy>());
            owner = new User(205600191, "Rotem", "r455!2@", false, false);
            owner.State = state.signedIn;
            ownerRole = new Owner(store, owner);
            owner.Roles.Add(owner.Id, ownerRole);
            store.Roles = new TreeNode<Role>(ownerRole);
            store.RolesDictionary.Add(owner.Id, new TreeNode<Role>(ownerRole));
            manager = new User(203114469, "Noy", "!(ftR6", false, false);
            List<int> permissions = new List<int>();
            permissions.Add(2);
            permissions.Add(4);
            permissions.Add(5);
            permissions.Add(6);
            managerRole = new Manager(store, manager, permissions);
            manager.Roles.Add(manager.Id, managerRole);
            store.Roles.AddChild(managerRole);
            store.RolesDictionary.Add(manager.Id, new TreeNode<Role>(managerRole));
            user = new User(201119304, "Keren", "@rtY89", false, false);
            List<int> invalidPremissions = new List<int>();
            invalidPremissions.Add(1);
            notPremittedManager = new User(202445691, "Adi", "*&112rY", false, false);
            notPremittedManagerRole = new Manager(store, notPremittedManager, invalidPremissions);
            notPremittedManager.Roles.Add(notPremittedManager.Id, notPremittedManagerRole);
            store.Roles.AddChild(notPremittedManagerRole);
            store.RolesDictionary.Add(notPremittedManager.Id, new TreeNode<Role>(notPremittedManagerRole));
            p = new Product(2, "Skinny jeans", "Trouses", "Gray", 159);
            pis = new ProductInStore(30, store, p);
            store.Products.Add(p.Id,pis);
        }

        //The store owner creats a new product in store - valid procedure.
        [TestMethod]
        public void createNewProductInStoreTest1()
        {
            setUp();
            bool x = store.createNewProductInStore("Top", "Tank tops", "Light blue", 89, 3, 205600191);
            Assert.IsTrue(x);
        }

        //The store manager creats a new product in store and he has premmision to do so - valid procedure.
        [TestMethod]
        public void createNewProductInStoreTest2()
        {
            setUp();
            bool x = store.createNewProductInStore("T-shirt", "Shirts", "White", 69, 9, 203114469);
            Assert.IsTrue(x);
        }

        //Not the store owner creats a new product in store - invalid procedure.
        [TestMethod]
        public void createNewProductInStoreTest3()
        {
            setUp();
            bool x = store.createNewProductInStore("Mini skirt", "Skirts", "Black", 129, 5, 201119304);
            Assert.IsFalse(x);
        }

        //Store Manager with no premission creats a new product in store - invalid procedure.
        [TestMethod]
        public void createNewProductInStoreTest4()
        {
            setUp();
            bool x = store.createNewProductInStore("Mini skirt", "Skirts", "Black", 129, 5, 201119304);
            Assert.IsFalse(x);
        }

        //Store Manager with the right premission adds a product to the store - valid procedure.
        [TestMethod]
        public void addProductInStoreTest1()
        {
            setUp();
            List<KeyValuePair<int, int>> productsQuantityList = new List<KeyValuePair<int, int>>();
            productsQuantityList.Add(new KeyValuePair<int, int>(2, 7));
            bool x = store.addProductsInStore(productsQuantityList, 203114469);
            Assert.AreEqual(pis.Quantity, 37);
        }

        //Store owner adds a product to the store - valid procedure.
        [TestMethod]
        public void addProductInStoreTest2()
        {
            setUp();
            List<KeyValuePair<int, int>> productsQuantityList = new List<KeyValuePair<int, int>>();
            productsQuantityList.Add(new KeyValuePair<int, int>(2, 4));
            bool x = store.addProductsInStore(productsQuantityList, 205600191);
            Assert.AreEqual(pis.Quantity, 34);
        }

        //Not the store owner adds a product to the store - invalid procedure.
        [TestMethod]
        public void addProductInStoreTest3()
        {
            setUp();
            List<KeyValuePair<int, int>> productsQuantityList = new List<KeyValuePair<int, int>>();
            productsQuantityList.Add(new KeyValuePair<int, int>(2, 11));
            bool x = store.addProductsInStore(productsQuantityList, 201119304);
            Assert.IsFalse(x);
        }

        //Store manager adds a product to the store with no premission - invalid procedure.
        [TestMethod]
        public void addProductInStoreTest4()
        {
            setUp();
            List<KeyValuePair<int, int>> productsQuantityList = new List<KeyValuePair<int, int>>();
            productsQuantityList.Add(new KeyValuePair<int, int>(2, 11));
            bool x = store.addProductsInStore(productsQuantityList, 201119304);
            Assert.IsFalse(x);
        }

        //Store Manager with the right premission removes a product from the store - valid procedure.
        [TestMethod]
        public void removeProductInStoreTest1()
        {
            setUp();
            List<KeyValuePair<int, int>> productsQuantityList = new List<KeyValuePair<int, int>>();
            productsQuantityList.Add(new KeyValuePair<int, int>(2, 7));
            bool x = store.removeProductsInStore(productsQuantityList, 203114469);
            Assert.AreEqual(pis.Quantity, 23);
        }

        //Store owner removes a product from the store - valid procedure.
        [TestMethod]
        public void removeProductInStoreTest2()
        {
            setUp();
            List<KeyValuePair<int, int>> productsQuantityList = new List<KeyValuePair<int, int>>();
            productsQuantityList.Add(new KeyValuePair<int, int>(2, 4));
            bool x = store.removeProductsInStore(productsQuantityList, 205600191);
            Assert.AreEqual(pis.Quantity, 26);
        }

        //Not the store owner removes a product from the store - invalid procedure.
        [TestMethod]
        public void removeProductInStoreTest3()
        {
            setUp();
            List<KeyValuePair<int, int>> productsQuantityList = new List<KeyValuePair<int, int>>();
            productsQuantityList.Add(new KeyValuePair<int, int>(2, 11));
            bool x = store.removeProductsInStore(productsQuantityList, 201119304);
            Assert.IsFalse(x);
        }

        //Store manager removes a product from the store with no premission - invalid procedure.
        [TestMethod]
        public void removeProductInStoreTest4()
        {
            setUp();
            List<KeyValuePair<int, int>> productsQuantityList = new List<KeyValuePair<int, int>>();
            productsQuantityList.Add(new KeyValuePair<int, int>(2, 11));
            bool x = store.removeProductsInStore(productsQuantityList, 201119304);
            Assert.IsFalse(x);
        }

        //Store Manager with the right premission edits a product in the store - valid procedure.
        [TestMethod]
        public void editProductInStoreTest1()
        {
            setUp();
            store.editProductsInStore(2, "Skinny jeans", "Trouses", "Gray", 99 ,203114469);
            Assert.AreEqual(pis.Product.Price, 99);
        }

        //Store owner edits a product in the store - valid procedure.
        [TestMethod]
        public void editProductInStoreTest2()
        {
            setUp();
            store.editProductsInStore(2, "Skinny jeans", "Trouses", "Gray", 99, 205600191);
            Assert.AreEqual(pis.Product.Price, 99);
        }

        //Not the store owner edits a product in the store - invalid procedure.
        [TestMethod]
        public void editProductInStoreTest3()
        {
            setUp();
            store.editProductsInStore(2, "Skinny jeans", "Trouses", "Gray", 99, 201119304);
            Assert.AreEqual(pis.Product.Price, 159);
        }

        //Store manager edits a product in the store with no premission - invalid procedure.
        [TestMethod]
        public void editProductInStoreTest4()
        {
            setUp();
            store.editProductsInStore(2, "Skinny jeans", "Trouses", "Gray", 99, 201119304);
            Assert.AreEqual(pis.Product.Price, 159);
        }

        /*---------------------------------------------------------------Stub Classes------------------------------------------------------------------------------------------------*/

        class StubProductInStore : ProductInStore
        {
            bool retVal;
            public StubProductInStore(int quantity, Store store, Product product,bool ret) : base(quantity,store,product)
            {
                this.retVal = ret;
            }

        }
    


    }
}
