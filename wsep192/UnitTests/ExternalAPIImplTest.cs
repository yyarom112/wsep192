using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;
using src;

namespace UnitTests
{

    [TestClass]
    public class ExternalAPIImplTest
    {
        ExternalAPIImpl ex;
        public void setUp()
        {
            ex = new ExternalAPIImpl();
        }

        [TestMethod]
        public void Test_Connect_Method_success()
        {

            setUp();
            Assert.IsTrue(ex.connect());

        }

        [TestMethod]
        public void Test_Pay_Method_success()
        {

            setUp();
            if (!ex.connect())
                Assert.Fail();

            Assert.AreNotEqual(ex.pay("111","4","2020","aviv","143","20000000"),-1);
        }

        [TestMethod]
        public void Test_Cancel_Pay_Method_success()
        {

            setUp();
            if (!ex.connect())
                Assert.Fail();
            int transaction_id = ex.pay("111", "4", "2020", "aviv", "143", "20000000");
            if(transaction_id==-1)
                Assert.Fail();
            Assert.IsTrue(ex.cancel_pay(transaction_id + ""));
        }

        [TestMethod]
        public void Test_Supply_Method_success()
        {
            setUp();
            if (!ex.connect())
                Assert.Fail();
            Assert.AreNotEqual(ex.supply("aviv", "telaviv", "telaviv", "Israel", "84344"), -1);
        }

        [TestMethod]
        public void Test_Cancel_Supply_Method_success()
        {

            setUp();
            if (!ex.connect())
                Assert.Fail();
            int supply_id = ex.supply("aviv", "telaviv", "telaviv", "Israel", "84344");
            if (supply_id == -1)
                Assert.Fail();
            Assert.IsTrue(ex.cancel_supply(supply_id + ""));
        }

    }
}


