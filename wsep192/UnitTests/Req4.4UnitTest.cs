using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;


namespace UnitTests
{

    [TestClass]
    public class Req4
    {
        private TradingSystem sys;
        private User admin;
        private User user,user2;
        private ShoppingBasket basket_user;
        private Store store;
        private List<DiscountPolicy> discountPolicies;
        private List<PurchasePolicy> purchasePolicies;
        public void setUp()
        {
            sys = new TradingSystem(null, null);
            
            discountPolicies = new List<DiscountPolicy>();
            purchasePolicies = new List<PurchasePolicy>();
            user = new User(1, "aviv", "123", false, false);
            store = new Store(0, "blabla");
            Owner owner = new Owner(store, user);
            user.Roles.Add(store.Id, owner);
            TreeNode<Role> ownerNode = store.Roles.AddChild(owner);
            store.RolesDictionary.Add(1, ownerNode);
            
            user2 = new User(2, "liraz", "123", false, false);
            Owner owner2 = new Owner(store, user2);
            user2.Roles.Add(store.Id, owner2);
            TreeNode<Role> owner2Node = ownerNode.AddChild(owner2);
            store.RolesDictionary.Add(2, owner2Node);
            basket_user = user.Basket;
            admin = new User(0, "admin", "1234", true, false);


            sys.Stores.Add(store.Id, store);
            sys.StoreCounter = 1;
            sys.ProductCounter = 4;
            
            sys.Users.Add(admin.Id, admin);
            sys.Users.Add(user.Id, user);
            sys.Users.Add(user2.Id, user2);
            sys.UserCounter = 3;
            user.State = state.signedIn;


        }
        [TestMethod]
        public void TestRemoveOwner()
        {
            setUp();
            Assert.AreEqual(false,sys.removeOwner(1, 4, 0));
            setUp();
            Assert.AreEqual(true, sys.removeOwner(1, 2, 0));
        }

    }
}
