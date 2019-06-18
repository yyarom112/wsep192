using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;
using src;

namespace UnitTests
{

    [TestClass]
    public class FinancialSystemImplTest
    {
        FinancialSystemImpl financialSystem;
        public void setUp()
        {
            financialSystem = new FinancialSystemImpl();
        }

        [TestMethod]
        public void Test_Connect_Method_success()
        {
            setUp();
            Assert.IsTrue(financialSystem.connect());
        }

        [TestMethod]
        public void Test_Pay_Method_success()
        {
            setUp();
            long cardNum = 123456780;
            DateTime date = new DateTime();
            int amount = 10;
            int paymentTarget = 10;
            Assert.AreNotEqual(financialSystem.payment(cardNum,date,amount,paymentTarget),-1);
        }

        [TestMethod]
        public void Test_Cancel_Pay_Method_success()
        {

            setUp();
            long cardNum = 123456780;
            DateTime date = new DateTime();
            int amount = 10;
            int paymentTarget = 10;
            Assert.AreNotEqual(financialSystem.payment(cardNum, date, amount, paymentTarget), -1);
            //NEED TO ADD TRANACTION_ID AND CANCEL PAYMENT
        }

    }
}


