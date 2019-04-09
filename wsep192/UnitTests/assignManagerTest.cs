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
            ownerUser.State = state.signedIn;
            store = new Store(1111, "adidas", null, null);
            ownerRole = new Owner(store, ownerUser);
            ownerUser.Roles.Add(ownerUser.Id, ownerRole);


            managerUser = new User(7878, "baba", "3434", false, false);
            managerUser.State = state.signedIn;
            permmision = new List<int>() { 2, 5, 6 };
            permmision = new List<int>() { 2, 5, 6 };
            managerRole = new Manager(store, managerUser, permmision);

            user1 = new User(2456, "luli", "5656", false, false);

            store.Roles = new TreeNode<Role>(ownerRole);
            store.RolesDictionary.Add(ownerUser.Id, new TreeNode<Role>(ownerRole));

            system.Users.Add(ownerUser.Id, ownerUser);
            system.Users.Add(managerUser.Id, managerUser);
            system.Users.Add(user1.Id, user1);
            system.Stores.Add(store.Id, store);

        }

        [TestMethod]
        public void TestMethod1_success_storeClass_scenario()
        {
            setUp();
            Assert.AreEqual(true, store.assignManager(managerRole, (Owner)ownerRole));
        }


        [TestMethod]
        public void TestMethod1_success_userClass_scenario()
        {
            setUp();
            Assert.AreEqual(true, ownerUser.assignManager(managerUser, store.Id, permmision));
        }

        [TestMethod]
        public void TestMethod1_success_system_scenario()
        {
            setUp();

            Assert.AreEqual(true, system.assignManager(ownerUser.Id, managerUser.Id, store.Id, permmision));
        }

        [TestMethod]
        public void TestMethod1_fail_system_scenario()
        {
            setUp();
            Assert.AreEqual(false, system.assignManager(ownerUser.Id, user1.Id, store.Id, permmision));
        }



        class StubOwner : Owner
        {
            private bool retVal;

            public StubOwner(Store store, User user, bool ret) : base(store, user)
            {
                this.retVal = ret;
            }


            public override bool assignManager(User managerUser, List<int> permissionToManager)
            {
                return retVal;
            }
        }

        class StubUser : User
        {
            bool retVal;
            public StubUser(int id, string userName, string password, bool isAdmin, bool isRegistered, bool ret) : base(id, userName, password, isAdmin, isRegistered)
            {
                this.retVal = ret;
            }

            public override bool assignManager(User managerUser, int storeId, List<int> permissionToManager)
            {
                return retVal;
            }
        }
    }
}
