using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;

namespace Acceptance_Tests
{
    [TestClass]
    public class removePurchasePolicy
    {
        ServiceLayer service;
        String idOwner;
        String ownerUser;
        String passwordUser;

        String idUser;
        String tmpUser;
        String passUser;

        int purchaseId;

        List<KeyValuePair<String, int>> products;
        List<KeyValuePair<String, int>> productsInCart;

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

            productsInCart = new List<KeyValuePair<String, int>>();
            productsInCart.Add(new KeyValuePair<string, int>("milk", 10));

            purchaseId = service.addComplexPurchasePolicy("(0,milk,0,10,0)", "adidas", ownerUser);
        }

        [TestMethod]
        public void removePurchasePolicy_succ()
        {
            setUp();
            int ans = service.removePurchasePolicy(purchaseId.ToString(), "adidas", ownerUser);
            Assert.AreEqual(0, ans);          
            service.shutDown();
        }

        [TestMethod]
        public void removePurchasePolicy_fail_store_not_exist()
        {
            setUp();
            int ans = service.removePurchasePolicy(purchaseId.ToString(), "nike", ownerUser);
            Assert.AreEqual(-1, ans);
            service.shutDown();
        }

        [TestMethod]
        public void removePurchasePolicy_fail_userNotOwner()
        {
            setUp();
            int ans = service.removePurchasePolicy(purchaseId.ToString(), "adidas", tmpUser);
            Assert.AreEqual(-1, ans);
            service.shutDown();
        }
    }
}
