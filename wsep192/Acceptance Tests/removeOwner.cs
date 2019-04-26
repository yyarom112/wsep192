using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;

namespace Acceptance_Tests
{

    [TestClass]
    public class removeOwner
    {

        ServiceLayer service;
        public void setUp()
        {
            service = new ServiceLayer();
            service.init("Admin", "2323");
            String tmpuser = service.initUser();
            service.register("aviv", "1234", tmpuser);
            tmpuser = service.initUser();
            service.register("zahi", "1234", tmpuser);
            service.signIn("aviv", "1234");
            if (!service.openStore("footlocker", "aviv"))
                Assert.Fail();
            if (!service.assignOwner("aviv", "zahi", "footlocker"))
                Assert.Fail();
        }
        [TestMethod]
        public void TestOwnerRemovesOwner()
        {
            setUp();
            Assert.AreEqual(true,service.removeOwner("zahi", "footlocker", "aviv"));
        }
        [TestMethod]
        public void TestNotOwnerRemovesOwner()
        {
            setUp();
            Assert.AreEqual(false, service.removeOwner("zahi", "footlocker", "yossi"));
        }
        [TestMethod]
        public void TestOwnerRemovesNotOwner()
        {
            setUp();
            Assert.AreEqual(false, service.removeOwner("yossi", "footlocker", "aviv"));
        }

    }
}