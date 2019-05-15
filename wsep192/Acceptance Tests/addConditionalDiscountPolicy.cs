using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;

namespace Acceptance_Tests
{
    [TestClass]
    public class addConditionalDiscountPolicy
    {
        ServiceLayer service;
        String idOwner;
        String ownerUser;
        String passwordUser;

        String idUser;
        String tmpUser;
        String passUser;

        List<KeyValuePair<String, int>> products;
        List<String> productsInCart;

        public void setUp()
        {
            service = ServiceLayer.getInstance();
            idOwner = service.initUser();
            ownerUser = "Seifan";
            passwordUser = "2345";
            service.register(ownerUser, passwordUser, idOwner);
            service.signIn(ownerUser, passwordUser);

            idUser = service.initUser();
            tmpUser = "luli";
            passUser = "1313";
            service.register(tmpUser, passUser, idUser);

            service.openStore("adidas", ownerUser);
            service.createNewProductInStore("milk", "milk", "none", 10, "adidas", ownerUser);
            products = new List<KeyValuePair<String, int>>();
            products.Add(new KeyValuePair<string, int>("milk", 7));
            service.addProductsInStore(products, "adidas", ownerUser);

            productsInCart = new List<String>();
            productsInCart.Add("milk");
        }

        [TestMethod]
        public void addConditionalDiscountPolicy_succ()
        {
            setUp();
            int ans = service.addConditionalDiscuntPolicy(productsInCart, "", "20", "40", "0", "0", ownerUser, "adidas");
            //Assert.AreEqual(0, ans);
            service.shutDown();
        }

        [TestMethod]
        public void addConditionalDiscountPolicy_fail()
        {
            setUp();
            int ans = service.addConditionalDiscuntPolicy(productsInCart, "", "20", "40", "0", "0", tmpUser, "adidas");
            //Assert.AreEqual(-1, ans);
            service.shutDown();
        }
    }
}
