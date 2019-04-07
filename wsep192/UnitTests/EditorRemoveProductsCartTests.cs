using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;

namespace UnitTests
{
    [TestClass]
    public class EditorRemoveProductsCartTests
    {
        TradingSystem system;
        User user;
        Store store;

        public void setUp()
        {
            system = new TradingSystem(null, null);
            user = new User(1, "user", "1234", false, false);
            store = new Store(1, "store", 0, null, null);

        }
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
