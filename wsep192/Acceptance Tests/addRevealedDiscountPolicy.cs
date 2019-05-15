using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.ServiceLayer;
using src.Domain;

namespace Acceptance_Tests
{
    
    [TestClass]
    public class addRevealedDiscountPolicy
    {
        ServiceLayer service;
        String idOwner;
        String ownerUser;
        String passwordUser;

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
            Console.WriteLine(ans);
            service.shutDown();
        }

    }
}
