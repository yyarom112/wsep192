using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;

namespace Acceptance_Tests
{
    [TestClass]
    public class openStore
    {
        private TradingSystem sys;
        private User admin;
        private User user;
        private ShoppingBasket basket_user;
        private Product p1;
        private Product p2;
        private Product p3;
        private ProductInStore pis1;
        private ProductInStore pis2;
        private ProductInStore pis3;
        private Store store;
        private List<DiscountPolicy> discountPolicies;
        private List<PurchasePolicy> purchasePolicies;

        public void setUp()
        {
            service = new ServiceLayer();
            service.init("admin", "1234");
            service.initUser("tmpuser");
        }

        [TestMethod]
        //an unregisterd user tries to preform open store.
        public void testOpenStore1()
        {
            setUp();
            bool x=service.openStore("Bershka", "Yonit Levy");
            Assert.IsFalse(x);
        }

        [TestMethod]
        //a registerd user tries to preform open store.
        public void testOpenStore2()
        {
            setUp();
            service.register("Yonit Levy", "23&As2", "tmpuser");
            bool x = service.openStore("Bershka", "Yonit Levy");
            Assert.IsTrue(x);
        }
    }
}
