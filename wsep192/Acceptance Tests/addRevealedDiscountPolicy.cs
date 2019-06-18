using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;
using src.Domain;
using src.DataLayer;

namespace Acceptance_Tests
{
    
    [TestClass]
    public class addRevealedDiscountPolicy
    {
        ServiceLayer service;
        String idOwner;
        String ownerUser;
        String passwordUser;

        String idUser;
        String tmpUser;
        String passUser;
        
        List<KeyValuePair<String, int>> products;
        List<KeyValuePair<String, int>> productsInCart;

        public void setUp()
        {
            service = ServiceLayer.getInstance(false);
            DBtransactions db = DBtransactions.getInstance(true);
            db.isTest(true);
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

            productsInCart = new List<KeyValuePair<String, int>>();
            productsInCart.Add(new KeyValuePair<string, int>("milk", 10));  
        }

        [TestMethod]
        public void addRevealedDiscountPolicy_succ()
        {
            setUp();
            int ans = service.addRevealedDiscountPolicy(products, "20", "60", "0", ownerUser, "adidas");
            Assert.AreEqual(0, ans);
            service.shutDown();
        }

        [TestMethod]
        public void addRevealedDiscountPolicy_fail()
        {
            setUp();
            int ans = service.addRevealedDiscountPolicy(products, "20", "60", "0", tmpUser, "adidas");
            Assert.AreEqual(-1, ans);
            service.shutDown();
        }

    }
}
