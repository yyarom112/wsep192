using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;
using System.Collections.Generic;

namespace IntegrationTests
{
    [TestClass]
    public class createNewProductInStoreIntegration
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
            store = new Store(2, "ZARA", new List<PurchasePolicy>(), new List<DiscountPolicy>());
            owner = new User(205600191, "Rotem", "r455!2@", false, false);
            owner.State = state.signedIn;
            ownerRole = new Owner(store, owner);
            owner.Roles.Add(owner.Id, ownerRole);
            store.Roles = new TreeNode<Role>(ownerRole);
            store.RolesDictionary.Add(owner.Id, new TreeNode<Role>(ownerRole));
            manager = new User(203114469, "Noy", "!(ftR6", false, false);
            List<int> permissions = new List<int>();
            permissions.Add(3);
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

        //The store owner creats a new product in store - valid procedure.
        [TestMethod]
        public void createNewProductInStoreTest1()
        {
            setUp();
            bool x = system.createNewProductInStore("Top", "Tank tops", "Light blue", 89, 2, 205600191);
            Assert.IsTrue(x);
        }

        //The store manager creats a new product in store and he has premmision to do so - valid procedure.
        [TestMethod]
        public void createNewProductInStoreTest2()
        {
            setUp();
            bool x = system.createNewProductInStore("T-shirt", "Shirts", "White", 69, 2, 203114469);
            Assert.IsTrue(x);
        }

        //Not the store owner creats a new product in store - invalid procedure.
        [TestMethod]
        public void createNewProductInStoreTest3()
        {
            setUp();
            bool x = system.createNewProductInStore("Mini skirt", "Skirts", "Black", 129, 2, 201119304);
            Assert.IsFalse(x);
        }

        //Store Manager with no premission creats a new product in store - invalid procedure.
        [TestMethod]
        public void createNewProductInStoreTest4()
        {
            setUp();
            bool x = system.createNewProductInStore("Mini skirt", "Skirts", "Black", 129, 2, 201119304);
            Assert.IsFalse(x);
        }
    }
}
