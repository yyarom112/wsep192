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
            service = ServiceLayer.getInstance();
            string tmpuser = service.initUser();
            service.register("aviv", "1234", tmpuser);
            service.signIn("aviv", "1234");
            service.openStore("footlocker", "aviv");
            if(!service.createNewProductInStore("airmax", "nike", "shoe", 100, "footlocker", "aviv"))
                Assert.Fail();
            if (!service.createNewProductInStore("gazzelle", "adidas", "", 100, "footlocker", "aviv"))
                Assert.Fail();
        }
        [TestMethod]
        public void TestMethod1_searchProductAvailable() //NEED TO CHECK
        {
            setUp();
            String output = service.searchProduct("airmax,nike,shoe,10,150,0,0");
            Assert.AreEqual(output, "name0=airmax&store0=footlocker&quantity0=0");
            service.shutDown();
            
        }
        [TestMethod]
        public void TestMethod1_searchProductUnavailable()  //NEED TO CHECK
        {
            setUp();
            String output = service.searchProduct("blabla,adidas,shoe10,150,0,0");
            Assert.AreEqual(output, "");
            service.shutDown();

        }

    }
}