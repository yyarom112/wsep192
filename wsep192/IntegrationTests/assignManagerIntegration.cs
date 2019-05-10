using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;

namespace IntegrationTests
{
    [TestClass]
    public class assignManagerIntegration
    {
        private TradingSystem system;
        private User ownerUser;
        private User managerUser;
        private User user1;
        private Store store;
        private List<int> permissions;
        private Role ownerRole;
        private Role managerRole;

        public void setUp()
        {
            system = new TradingSystem(null, null);
            ownerUser = new User(1234, "Seifan", "2457", false, false);
            ownerUser.register(ownerUser.UserName, ownerUser.Password);
            ownerUser.signIn(ownerUser.UserName, ownerUser.Password);
            store = new Store(1111, "adidas");
            ownerRole = new Owner(store, ownerUser);
            ownerUser.Roles.Add(store.Id, ownerRole);


            managerUser = new User(7878, "baba", "3434", false, false);
            managerUser.register(managerUser.UserName, managerUser.Password);
            permissions = new List<int>();
            permissions.Add(2);
            user1 = new User(2456, "luli", "5656", false, false);

            store.Roles = new TreeNode<Role>(ownerRole);
            store.RolesDictionary.Add(ownerUser.Id, new TreeNode<Role>(ownerRole));

            system.Users.Add(ownerUser.Id, ownerUser);
            system.Users.Add(managerUser.Id, managerUser);
            system.Users.Add(user1.Id, user1);
            system.Stores.Add(store.Id, store);
        }

        [TestMethod]
        public void TestMethod1_success_system_scenario()
        {
            setUp();
            Assert.AreEqual(true, system.assignManager(ownerUser.Id, managerUser.Id, store.Id, permissions));
        }

        [TestMethod]
        public void TestMethod1_fail_system_notManager_scenario()
        {
            setUp();
            Assert.AreEqual(false, system.assignManager(ownerUser.Id, user1.Id, store.Id, permissions));
        }

        [TestMethod]
        public void TestMethod1_fail_system_notOwner_scenario()
        {
            setUp();
            Assert.AreEqual(false, system.assignManager(user1.Id, managerUser.Id, store.Id, permissions));
        }

        [TestMethod]
        public void TestMethod1_fail_system_ownerAssignOwner_scenario()
        {
            setUp();
            Assert.AreEqual(false, system.assignManager(ownerUser.Id, ownerUser.Id, store.Id, permissions));
        }

        [TestMethod]
        public void TestMethod1_fail_system_ownerAssignExistManager_scenario()
        {
            setUp();
            managerRole = new Manager(store, managerUser, permissions);
            managerUser.Roles.Add(managerUser.Id, managerRole);
            store.RolesDictionary.Add(managerUser.Id, new TreeNode<Role>(managerRole));
            Assert.AreEqual(false, system.assignManager(ownerUser.Id, managerUser.Id, store.Id, permissions));
        }

    }
}
