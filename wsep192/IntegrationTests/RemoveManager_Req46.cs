using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;

namespace IntegrationTests
{
    [TestClass]
    public class RemoveManager_Req46
    {
        private TradingSystem sys;
        private Encryption encrypt;

        private User admin;
        private ShoppingBasket basket_admin;

        private User user;
        private ShoppingBasket basket_user;

        private User manager;

        private Owner storeOwner;
        private Manager storeManager;


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
            admin.State = state.signedIn;
            basket_admin = admin.Basket;
            user = new User(1, null, null, false, false);
            basket_user = user.Basket;
            manager = new User(2, "a", "1234", false, true);

            store = new Store(-1, "store");

            storeOwner = new Owner(store, admin);
            storeManager = new Manager(store, manager, new List<int>());


            admin.Roles.Add(store.Id, storeOwner);
            manager.Roles.Add(store.Id, storeManager);


            store.Roles = new TreeNode<Role>(storeOwner);
            TreeNode<Role> manegerNode = store.Roles.AddChild(storeManager);

            store.RolesDictionary.Add(admin.Id, store.Roles);
            store.RolesDictionary.Add(manager.Id, manegerNode);


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
            sys = new TradingSystem(null, null);
            sys.StoreCounter = 1;
            sys.ProductCounter = 4;
            sys.UserCounter = 2;
            sys.Stores.Add(store.Id, store);
            sys.Users.Add(admin.Id, admin);
            sys.Users.Add(user.Id, user);
            sys.Users.Add(manager.Id, manager);


        }

        [TestMethod]
        public void Store_RemoveManager_succ()
        {
            setUp();
            Assert.AreEqual(true, store.removeManager(manager.Id, storeOwner));
        }

        [TestMethod]
        public void Store_RemoveManager_fail_theUserIsNotManager()
        {
            setUp();
            Assert.AreEqual(false, store.removeManager(user.Id, new Role(store, user)));
        }

        [TestMethod]
        public void User_searchRoleByStoreID_succ()
        {
            setUp();
            Assert.AreEqual(store, manager.searchRoleByStoreID(store.Id, manager.Id).Store);
        }



        [TestMethod]
        public void User_searchRoleByStoreID_fail()
        {
            setUp();
            Assert.AreEqual(null, manager.searchRoleByStoreID(0, manager.Id));
        }

        [TestMethod]
        public void User_removeManager_succ()
        {
            setUp();
            Assert.AreEqual(true, admin.removeManager(manager.Id, store.Id));
        }

        [TestMethod]
        public void User_removeManager_fail_noPremission()
        {
            setUp();
            Assert.AreEqual(false, manager.removeManager(manager.Id, store.Id));
        }

        [TestMethod]
        public void User_removeManager_fail_NoRoleInTheStore()
        {
            setUp();
            admin.Roles.Remove(store.Id);
            Assert.AreEqual(false, admin.removeManager(manager.Id, store.Id));
        }

        [TestMethod]
        public void TradingSystem_removeManager_succ()
        {
            setUp();
            Assert.AreEqual(true, sys.removeManager(admin.Id, manager.Id, store.Id));
        }

        [TestMethod]
        public void TradingSystem_removeManager_fail_preCondDoesNotExist()
        {
            setUp();
            Assert.AreEqual(false, sys.removeManager(4, admin.Id, store.Id));
        }

        [TestMethod]
        public void TradingSystem_fail_getFalseFromUser()
        {
            setUp();
            Assert.AreEqual(false, sys.removeManager(admin.Id, user.Id, store.Id));
        }

    }
}
