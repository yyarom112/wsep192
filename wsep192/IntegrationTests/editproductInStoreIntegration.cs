using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;

namespace IntegrationTests
{
    [TestClass]
    class editproductInStoreIntegration
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

        //Store Manager with the right premission edits a product in the store - valid procedure.
        [TestMethod]
        public void editProductInStoreTest1()
        {
            setUp();
            system.editProductInStore(2, "Skinny jeans", "Trouses", "Gray", 99,2, 203114469);
            Assert.AreEqual(pis.Product.Price, 99);
        }

        //Store owner edits a product in the store - valid procedure.
        [TestMethod]
        public void editProductInStoreTest2()
        {
            setUp();
            system.editProductInStore(2, "Skinny jeans", "Trouses", "Gray", 99,2, 205600191);
            Assert.AreEqual(pis.Product.Price, 99);
        }

        //Not the store owner edits a product in the store - invalid procedure.
        [TestMethod]
        public void editProductInStoreTest3()
        {
            setUp();
            system.editProductInStore(2, "Skinny jeans", "Trouses", "Gray", 99,2, 201119304);
            Assert.AreEqual(pis.Product.Price, 159);
        }

        //Store manager edits a product in the store with no premission - invalid procedure.
        [TestMethod]
        public void editProductInStoreTest4()
        {
            setUp();
            system.editProductInStore(2, "Skinny jeans", "Trouses", "Gray", 99,2, 201119304);
            Assert.AreEqual(pis.Product.Price, 159);
        }


    }
}
