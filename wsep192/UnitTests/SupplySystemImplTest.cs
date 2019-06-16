using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;
using src;

namespace UnitTests
{

    [TestClass]
    public class SupplySystemImplTest
    {
        ProductSupplySystemImpl productSupplySystem;
        public void setUp()
        {
            productSupplySystem = new ProductSupplySystemImpl();
        }

        [TestMethod]
        public void Test_Connect_Method_success()
        {

            setUp();
            Assert.IsTrue(productSupplySystem.connect());

        }

        [TestMethod]
        public void Test_Supply_Method_success()
        {
            setUp();
            if (!productSupplySystem.connect())
                Assert.Fail();
            Assert.IsTrue(productSupplySystem.deliverToCustomer("telaviv","blablabla"));    //NEED TO BE CHANGED

        }

        [TestMethod]
        public void Test_Cancel_Supply_Method_success()
        {

            setUp();
            if (!productSupplySystem.connect())
                Assert.Fail();
            Assert.IsTrue(productSupplySystem.deliverToCustomer("telaviv", "blablabla"));    //NEED TO BE CHANGED
            //NEED TO BE ADD TRANSACTION_ID AND CANCEL SUPPLY
        }

    }
}


