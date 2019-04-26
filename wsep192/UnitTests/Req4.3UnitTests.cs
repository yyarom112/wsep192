using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class Req4_3
    {
        private Store store;
        private User owner;
        private Role ownerRole;
        private User assigned;
        private User manager;
        private Role managerRole;
        public void setUp()
        {
            store = new Store(2, "ZARA", new List<PurchasePolicy>(), new List<DiscountPolicy>());
            owner = new User(205600191, "Rotem", "r455!2@", false, false);
            owner.State = state.signedIn;
            ownerRole = new Owner(store, owner);
            owner.Roles.Add(owner.Id, ownerRole);
            store.Roles = new TreeNode<Role>(ownerRole);
            store.RolesDictionary.Add(owner.Id, new TreeNode<Role>(ownerRole));
            assigned = new User(301600802, "Hen", "!235yZ", false, false);
            manager = new User(205667112, "Shir", "!223@lSa", false, false);
            managerRole = new Manager(store, manager, new List<int>());
            manager.Roles.Add(manager.Id, managerRole);
            store.Roles = new TreeNode<Role>(managerRole);
            store.RolesDictionary.Add(manager.Id, new TreeNode<Role>(managerRole));
        }
        [TestMethod]
        //The owner assigns a diffrent user to be a owner-valid.
        public void AssignOwnerTest1()
        {
            setUp();
            bool x=store.assignOwner(assigned, ownerRole);
            Assert.IsTrue(x);

        }

        [TestMethod]
        //Not the owner assigns a diffrent user to be a owner-invalid.
        public void AssignOwnerTest2()
        {
            setUp();
            bool x = manager.assignOwner(store.Id, assigned);
            Assert.IsFalse(x);
        }

        [TestMethod]
        //The owner assigns a diffrent user to be a owner -valid.
        public void AssignOwnerTest3()
        {
            setUp();
            bool x = owner.assignOwner(store.Id, assigned);
            Assert.IsTrue(x);
        }

    }
}
