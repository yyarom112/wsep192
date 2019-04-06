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
        private Store store;
        private List<int> permmision;
        private Role ownerRole;

        public void setUp()
        {
            system = new TradingSystem(null, null);
            ownerUser = new User(1234, "Seifan", "2457", true, true);
            managerUser = new User(7878, "baba", "3434", false, false);
            store = new Store(1111, "adidas", 0, null, null);
            ownerRole = new Owner(store, ownerUser);
            permmision = new List<int>() { 2, 5, 6 };
            system.Users.Add(ownerUser.Id, ownerUser);
            system.Users.Add(managerUser.Id, managerUser);
            system.Stores.Add(store.Id, store);
        }

        [TestMethod]
        public void TestMethod1_success_scenario()
        {
            setUp();
            system.register(ownerUser.UserName, ownerUser.Password, ownerUser.Id.ToString());
            system.signIn(ownerUser.UserName, ownerUser.Password, ownerUser.Id.ToString());
            system.register(managerUser.UserName, managerUser.Password, managerUser.Id.ToString());
            system.signIn(managerUser.UserName, managerUser.Password, managerUser.Id.ToString());
            ownerUser.Roles.Add(ownerUser.Id, ownerRole);
            Boolean tmp = system.assignManager(ownerUser.Id, managerUser.Id, store.Id, permmision);
            Console.WriteLine(tmp);

        }
    }
}
