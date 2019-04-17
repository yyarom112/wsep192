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
            String output = service.searchProduct("airmax nike shoe 10 150 0 0");
            Assert.AreEqual(output, "Name: airmax\nStore Name: footlocker\nQuantity: 0");
            
        }
        [TestMethod]
        public void TestMethod1_searchProductUnavailable()  //NEED TO CHECK
        {
            setUp();
            String output = service.searchProduct("blabla adidas shoe 10 150 0 0");
            Assert.AreEqual(output, "");

        }

    }
}