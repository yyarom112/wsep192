using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;

namespace Acceptance_Tests
{

    [TestClass]
    public class searchProduct
    {

        ServiceLayer service;

        public void setUp()
        {
            service = new ServiceLayer();
            service.init("Admin", "2323");
            service.initUser();
            service.register("aviv", "1234", "tmpuser");
            service.signIn("aviv", "1234");
            service.openStore("footlocker", "aviv");
            if(!service.createNewProductInStore("airmax", "nike", "", 100, "footlocker", "aviv"))
                Assert.Fail();
            if (!service.createNewProductInStore("gazzelle", "adidas", "", 100, "footlocker", "aviv"))
                Assert.Fail();
        }
        [TestMethod]
        public void TestMethod1_searchProductAvailable()
        {
            setUp();
            service.searchProduct("air max nike ");//TODO::fill this test
            
        }
        public void TestMethod1_searchProductUnavailable()
        {
            setUp();

        }

    }
}