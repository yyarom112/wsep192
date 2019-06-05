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
        //Testing ExternalAPIImpl class method
        [TestMethod]
        public void TestMethod_succses1()
        {

            setUp();
            Assert.IsTrue(ex.connect());

        }

        //Testing ExternalAPIImpl class method
        [TestMethod]
        public void TestMethod_succses2()
        {

            setUp();
            ex.connect();
            ex.pay();

        }





    }
}


