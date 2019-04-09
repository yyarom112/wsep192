using System;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src;

namespace UnitTests
{
    [TestClass]
    public class LogManagerTest
    {
        [TestMethod]
        public void TestMethod1_writeToLog()
        {
            LogManager log = LogManager.Instance;
            log.WriteToLog("Real Madrid");
            Assert.AreEqual(true, readLog().Contains("Real Madrid"));
        }


        [TestMethod]
        public void TestMethod1_OpenNewLog()
        {
            LogManager log = LogManager.Instance;
            log.WriteToLog("Real Madrid");
            Assert.AreEqual(true, readLog().Contains("Real Madrid"));
            log.OpenAnewLogFile();
            Assert.AreEqual(false, readLog().Contains("Real Madrid"));

        }


        public String readLog()
        {
            return System.IO.File.ReadAllText(@"MarketLog.txt");

        }
    }
}
