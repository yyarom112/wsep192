using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;

namespace Acceptance_Tests
{
    [TestClass]
    public class openStore
    {
        ServiceLayer service;
        private string user;

        public void setUp()
        {
            service = ServiceLayer.getInstance(false);
            user =service.initUser();
        }

        [TestMethod]
        //an unregisterd user tries to preform open store - invalid
        public void testOpenStore1()
        {
            setUp();
            bool x = service.openStore("Bershka", "Yonit");
            Assert.IsFalse(x);
            service.shutDown();
        }

        [TestMethod]
        //a registerd user tries to preform open store - valid
        public void testOpenStore2()
        {
            setUp();
            service.register("Yonit", "23&As2", user);
            service.signIn("Yonit", "23&As2");
            bool x= service.openStore("Bershka", "Yonit");
            Assert.IsTrue(x);
            service.shutDown();
        }

        [TestMethod]
        //The store owner tries to assign another owner to the store -valid
        public void testOpenStore3()
        {
            setUp();
            service.register("Yonit", "23&As2", user);
            service.signIn("Yonit", "23&As2");
            service.openStore("Bershka", "Yonit");
            string newOwner = service.initUser();
            service.register("Shay", "Aw32!9", newOwner);
            service.signIn("Shay", "Aw32!9");
            bool x=service.assignOwner("Yonit", "Shay", "Bershka");
            Assert.IsTrue(x);
            service.shutDown();
        }

        [TestMethod]
        //The admin tries to remove the main owner - invalid
        public void testOpenStore4()
        {
            setUp();
            service.register("Yonit", "23&As2", user);
            service.signIn("Yonit", "23&As2");
            service.openStore("Bershka", "Yonit");
            bool x = service.removeUser("admin", "Yonit");
            Assert.IsFalse(x);
            service.shutDown();
        }
    }
}
