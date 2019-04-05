using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;

namespace UnitTests
{
    [TestClass]
    public class InitSystemTest
    {
        private TradingSystem system;


        public void setUp()
        {
            system = new TradingSystem(new src.ProductSupplySystemImpl(),new src.FinancialSystemImpl());
        }
        [TestMethod]
        public void TestMethod1()
        {
            setUp();
            Assert.AreEqual(true, system.Users.Keys.Count==0);
            Assert.AreEqual(true, system.init("admin","1234"));
            Assert.AreEqual(true, system.Users.ContainsKey(0));
            Assert.AreEqual(true, system.Users[0].IsAdmin);
            Assert.AreEqual(true, system.Users[0].Password=="1234");
            Assert.AreEqual(true, system.Users[0].UserName == "admin");
        }
    }
}
