using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;

namespace UnitTests
{   
    [TestClass]
    public class assignManagerTest
    {
        private TradingSystem system;
        private User ownerUser;
        private User managerUser;
        private User user1;
        private Store store;
        private List<int> permmision;
        private Role ownerRole;
        private Role managerRole;

        public void setUp()
        {
            system = new TradingSystem(null, null);
            ownerUser = new User(1234, "Seifan", "2457", false, false);
            managerUser = new User(7878, "baba", "3434", false, false);
            user1 = new User(2456, "luli", "5656", false, false);
            store = new Store(1111, "adidas", 0, null, null);
            permmision = new List<int>() { 2, 5, 6 };
            ownerRole = new Owner(store, ownerUser);
            managerRole = new Manager(store, managerUser, permmision);
            permmision = new List<int>() { 2, 5, 6 };
            system.Users.Add(ownerUser.Id, ownerUser);
            system.Users.Add(managerUser.Id, managerUser);
            system.Users.Add(user1.Id, user1);
            system.Stores.Add(store.Id, store);
        }

        [TestMethod]
        public void TestMethod1_success_scenario()
        {
            setUp();
            String userName = ownerUser.UserName;
            String password = ownerUser.Password;
            int userId = ownerUser.Id;
            system.register(userName, password, userId.ToString());
            system.signIn(userName, password, userId.ToString());
            String userName1 = managerUser.UserName;
            String password1 = managerUser.Password;
            int userId1 = managerUser.Id;
            system.register(userName1, password1, userId1.ToString());
            system.signIn(userName1, password1, userId1.ToString());
            ownerUser.Roles.Add(ownerUser.Id, ownerRole);
            store.Roles.AddChild(ownerRole);
            Assert.AreEqual(true, system.assignManager(ownerUser.Id, managerUser.Id, store.Id, permmision));
        }

        [TestMethod]
        public void TestMethod1_fail_regular_costumer_scenario()
        {
            setUp();
            String userName = user1.UserName;
            String password = user1.Password;
            int userId = user1.Id;
            system.register(userName, password, userId.ToString());
            system.signIn(userName, password, userId.ToString());
            String userName1 = managerUser.UserName;
            String password1 = managerUser.Password;
            int userId1 = managerUser.Id;
            system.register(userName1, password1, userId1.ToString());
            system.signIn(userName1, password1, userId1.ToString());
            Assert.AreEqual(false, system.assignManager(user1.Id, managerUser.Id, store.Id, permmision));
        }

        [TestMethod]
        public void TestMethod1_fail_noOwnerOfTheStore_scenario()
        {
            setUp();
            String userName = ownerUser.UserName;
            String password = ownerUser.Password;
            int userId = ownerUser.Id;
            system.register(userName, password, userId.ToString());
            system.signIn(userName, password, userId.ToString());
            String userName1 = managerUser.UserName;
            String password1 = managerUser.Password;
            int userId1 = managerUser.Id;
            system.register(userName1, password1, userId1.ToString());
            system.signIn(userName1, password1, userId1.ToString());
            ownerUser.Roles.Add(ownerUser.Id, ownerRole);
            Assert.AreEqual(false, system.assignManager(ownerUser.Id, managerUser.Id, store.Id, permmision));
        }

        [TestMethod]
        public void TestMethod1_fail_manager_not_register_scenario()
        {
            setUp();
            String userName = ownerUser.UserName;
            String password = ownerUser.Password;
            int userId = ownerUser.Id;
            system.register(userName, password, userId.ToString());
            system.signIn(userName, password, userId.ToString());
            ownerUser.Roles.Add(ownerUser.Id, ownerRole);
            store.Roles.AddChild(ownerRole);
            Assert.AreEqual(false, system.assignManager(ownerUser.Id, managerUser.Id, store.Id, permmision));
        }

        [TestMethod]
        public void TestMethod1_fail_owner_assign_owner_scenario()
        {
            setUp();
            String userName = ownerUser.UserName;
            String password = ownerUser.Password;
            int userId = ownerUser.Id;
            system.register(userName, password, userId.ToString());
            system.signIn(userName, password, userId.ToString());
            ownerUser.Roles.Add(ownerUser.Id, ownerRole);
            store.Roles.AddChild(ownerRole);
            Assert.AreEqual(false, system.assignManager(ownerUser.Id, ownerUser.Id, store.Id, permmision));
        }

        /*[TestMethod]
        public void TestMethod1_fail_manager_already_exist_scenario()
        {
            setUp();
            String userName = ownerUser.UserName;
            String password = ownerUser.Password;
            int userId = ownerUser.Id;
            system.register(userName, password, userId.ToString());
            system.signIn(userName, password, userId.ToString());
            String userName1 = managerUser.UserName;
            String password1 = managerUser.Password;
            int userId1 = managerUser.Id;
            system.register(userName1, password1, userId1.ToString());
            system.signIn(userName1, password1, userId1.ToString());
            ownerUser.Roles.Add(ownerUser.Id, ownerRole);
            store.Roles.AddChild(ownerRole);
            managerUser.Roles.Add(managerUser.Id, managerRole);
            TreeNode<Role> currOwner = store.Roles.FindInChildren(ownerRole);
            currOwner.AddChild(managerRole);
            Boolean tmp = system.assignManager(ownerUser.Id, managerUser.Id, store.Id, permmision);
            Assert.AreEqual(false, system.assignManager(ownerUser.Id, managerUser.Id, store.Id, permmision));
        }*/
    }
}
