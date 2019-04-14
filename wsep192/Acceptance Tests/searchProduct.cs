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
            service.initUser("tmpuser");
            service.register("aviv", "1234", "tmpuser");
            service.signIn("aviv", "1234");
            //TODO: continue this test when you have req 4.1
        }
        
    }
}