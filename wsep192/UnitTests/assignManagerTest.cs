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
        private List<int> permissions;
        private Owner ownerRole;
        private Manager managerRole;

        public void setUp()
        {
            system = new TradingSystem(null, null);
            ownerUser = new User(1234, "Seifan", "2457", false, false);
            ownerUser.register(ownerUser.UserName, ownerUser.Password);
            ownerUser.signIn(ownerUser.UserName, ownerUser.Password);
            store = new Store(1111, "adidas", null, null);
            ownerRole = new Owner(store, ownerUser);
            ownerUser.Roles.Add(store.Id, ownerRole);


            managerUser = new User(7878, "baba", "3434", false, false);
            managerUser.register(managerUser.UserName, managerUser.Password);
            permissions = new List<int>();
            permissions.Add(1);
            managerRole = new Manager(store, managerUser, permissions);

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
        public void TestMethod1_success_managerClass_scenario()
        {
            setUp();
            Assert.AreEqual(true, managerRole.validatePermission(1));
        }

        [TestMethod]
        public void TestMethod1_fail_storeClass_managerExist_scenario()
        {
            setUp();
            store.RolesDictionary.Add(managerUser.Id, new TreeNode<Role>(managerRole));
            Assert.AreEqual(false, store.assignManager(managerRole, (Owner)ownerRole));
        }

        [TestMethod]
        public void TestMethod1_success_ownerClass_scenario()
        {
            setUp();
            Assert.AreEqual(true, ownerRole.assignManager(managerUser, permissions));
        }

        [TestMethod]
        public void TestMethod1_success_userClass_scenario()
        {
            setUp();
            StubStore sStore = new StubStore(3456, "nike", null, null, true);
            ownerUser.Roles.Add(sStore.Id, ownerRole);
            Assert.AreEqual(true, ownerUser.assignManager(managerUser, sStore.Id, permissions));
        }

        [TestMethod]
        public void TestMethod1_fail_userClass_scenario()
        {
            setUp();
            StubStore sStore = new StubStore(3456, "nike", null, null, true);
            Assert.AreEqual(false, ownerUser.assignManager(user1, sStore.Id, permissions));
        }

        [TestMethod]
        public void TestMethod1_success_system_scenario()
        {
            setUp();
            StubStore sStore = new StubStore(3456, "nike", null, null, true);
            StubUser ownerUserStub = new StubUser(2222, "owner", "7878", false, true, true);
            StubUser managerUserStub = new StubUser(2323, "babi", "3434", false, true, true);
            system.Users.Add(ownerUserStub.Id, ownerUserStub);
            system.Users.Add(managerUserStub.Id, managerUserStub);
            system.Stores.Add(sStore.Id, sStore);
            Assert.AreEqual(true, system.assignManager(ownerUserStub.Id, managerUserStub.Id, sStore.Id, permissions));
        }

        [TestMethod]
        public void TestMethod1_fail_system_scenario()
        {
            setUp();
            StubStore sStore = new StubStore(3456, "nike", null, null, false);
            StubUser ownerUserStub = new StubUser(2222, "owner", "7878", false, true, false);
            StubUser managerUserStub = new StubUser(2323, "babi", "3434", false, true, false);
            system.Users.Add(ownerUserStub.Id, ownerUserStub);
            system.Stores.Add(sStore.Id, sStore);
            Assert.AreEqual(false, system.assignManager(ownerUser.Id, user1.Id, store.Id, permissions));
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

        class StubStore : Store
        {
            bool retVal;
            public StubStore(int id, string name, List<PurchasePolicy> purchasePolicy, List<DiscountPolicy> discountPolicy, bool ret) : base(id, name, purchasePolicy, discountPolicy)
            {
                this.retVal = ret;
            }

            public override bool assignManager(Role newManager, Owner owner)
            {
                return retVal;
            }
        }
    }
}