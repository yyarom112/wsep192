using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Acceptance_Tests
{
    [TestClass]
    public class InitTests
    {
        ServiceLayer service ;

        public void setUp()
        {
            service = new ServiceLayer();
        }

        [TestMethod]
        public void TestMethod1_success()
        {
            Assert.AreEqual(true, service.init("Admin","SecretPassword1D4F6Yt7"));
        }
    }
}
