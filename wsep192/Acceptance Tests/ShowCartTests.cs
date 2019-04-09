using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;

namespace Acceptance_Tests
{
    [TestClass]
    public class ShowCartTests
    {
        ServiceLayer service;

        public void setUp()
        {
            service = new ServiceLayer();
            service.initUser("tmpuser");
        }

        public void setUpSuccess()
        {
            //service.openStore("store", "tmpuser");
            //service.
        }

        [TestMethod]
        public void TestMethod1_success()
        {
           // setUp();
           // setUpSuccess();
           // Assert.AreEqual(true, service.showCart(1));
        }
    }
}
