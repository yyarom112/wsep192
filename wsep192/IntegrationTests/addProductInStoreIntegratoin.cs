using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;
using System.Collections.Generic;

namespace IntegrationTests
{
    [TestClass]
    public class addProductInStoreIntegratoin
    {
        private TradingSystem system;
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
            system = new TradingSystem(null, null);
            store = new Store(2, "ZARA");
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
            store.Products.Add(p.Id, pis);
            system.Stores.Add(store.Id, store);
            system.Users.Add(owner.Id, owner);
            system.Users.Add(manager.Id, manager);
            system.Users.Add(notPremittedManager.Id, notPremittedManager);
            system.Users.Add(user.Id, user);
        }

        //Store Manager with the right premission adds a product to the store - valid procedure.
        [TestMethod]
        public void addProductInStoreTest1()
        {
            setUp();
            List<KeyValuePair<int, int>> productsQuantityList = new List<KeyValuePair<int, int>>();
            productsQuantityList.Add(new KeyValuePair<int, int>(2, 7));
            bool x = system.addProductsInStore(productsQuantityList, 2, 203114469);
            Assert.AreEqual(pis.Quantity, 37);
        }

        //Store owner adds a product to the store - valid procedure.
        [TestMethod]
        public void addProductInStoreTest2()
        {
            setUp();
            List<KeyValuePair<int, int>> productsQuantityList = new List<KeyValuePair<int, int>>();
            productsQuantityList.Add(new KeyValuePair<int, int>(2, 4));
            bool x = system.addProductsInStore(productsQuantityList, 2, 205600191);
            Assert.AreEqual(pis.Quantity, 34);
        }

        //Not the store owner adds a product to the store - invalid procedure.
        [TestMethod]
        public void addProductInStoreTest3()
        {
            setUp();
            List<KeyValuePair<int, int>> productsQuantityList = new List<KeyValuePair<int, int>>();
            productsQuantityList.Add(new KeyValuePair<int, int>(2, 11));
            bool x = system.addProductsInStore(productsQuantityList, 2, 201119304);
            Assert.IsFalse(x);
        }

        //Store manager adds a product to the store with no premission - invalid procedure.
        [TestMethod]
        public void addProductInStoreTest4()
        {
            setUp();
            List<KeyValuePair<int, int>> productsQuantityList = new List<KeyValuePair<int, int>>();
            productsQuantityList.Add(new KeyValuePair<int, int>(2, 11));
            bool x = system.addProductsInStore(productsQuantityList, 2, 201119304);
            Assert.IsFalse(x);
        }

    }
}
