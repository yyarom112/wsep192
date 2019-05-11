using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;

namespace UnitTests
{
    [TestClass]
    public class addPurchasePolicy
    {



        private TradingSystem sys;
        private Encryption encrypt;

        private User admin;
        private ShoppingBasket basket_admin;

        private User user;
        private ShoppingBasket basket_user;


        private Product p1;
        private Product p2;
        private Product p3;
        private Product p4;
        private ProductInStore pis1;
        private ProductInStore pis2;
        private ProductInStore pis3;
        private ProductInStore pis4;

        private Store store;


        public void setUp()
        {
            admin = new User(0, "admin", "123456", true, true);
            basket_admin = admin.Basket;
            user = new User(1, null, null, false, false);
            basket_user = user.Basket;

            store = new Store(-1, "store");

            p1 = new Product(0, "first", "", "", 5000);
            p2 = new Product(1, "second", "", "", 5000);
            p3 = new Product(2, "third", "", "", 5000);
            p4 = new Product(3, "fourth", "", "", 5000);
            pis1 = new ProductInStore(10, store, p1);
            pis2 = new ProductInStore(10, store, p2);
            pis3 = new ProductInStore(10, store, p3);
            pis4 = new ProductInStore(10, store, p4);
            store.Products.Add(p1.Id, pis1);
            store.Products.Add(p2.Id, pis2);
            store.Products.Add(p3.Id, pis3);
            store.Products.Add(p4.Id, pis4);
            sys = new TradingSystem(null, null);
            sys.StoreCounter = 1;
            sys.ProductCounter = 4;
            sys.UserCounter = 2;
            sys.Stores.Add(store.Id, store);
            sys.Users.Add(admin.Id, admin);
            sys.Users.Add(user.Id, user);


        }

        //-------------------------------@@@ Simple path @@@---------------------------------------------------------------------------

        [TestMethod]
        public void Store_addSimplePurchasePolicy()
        {
            setUp();
            PurchesPolicyData data = new PurchesPolicyData(0, 0, p1.Id, -1, 2, 10, -1, -1, LogicalConnections.and, null, false);
            try
            {
                ProductConditionPolicy p = (ProductConditionPolicy)store.addSimplePurchasePolicy(data);
                Assert.AreEqual(true, p.getId() == 0, "ProductConditionPolicy- id not insert correctly");
            }
            catch (Exception e)
            {
                Assert.AreEqual(true, false, "ProductConditionPolicy-An object of the wrong type was created");
            }
           
            data = new PurchesPolicyData(1, 0, p1.Id, -1, 5, -1, -1, -1, LogicalConnections.and, null, false);
            try
            {
                inventoryConditionPolicy p = (inventoryConditionPolicy)store.addSimplePurchasePolicy(data);
                Assert.AreEqual(true, p.getId() == 0, "inventoryConditionPolicy- id not insert correctly");
            }
            catch (Exception e)
            {
                Assert.AreEqual(true, false, "inventoryConditionPolicy-An object of the wrong type was created");
            }
            data = new PurchesPolicyData(2,0,-1,-1,2,10,15,30, LogicalConnections.and, null, false);
            try
            {
                BuyConditionPolicy p = (BuyConditionPolicy)store.addSimplePurchasePolicy(data);
                Assert.AreEqual(true, p.getId() == 0, "BuyConditionPolicy- id not insert correctly");
            }
            catch (Exception e)
            {
                Assert.AreEqual(true, false, "BuyConditionPolicy-An object of the wrong type was created");
            }
            data = new PurchesPolicyData(3, 0, -1, -1, -1, -1, -1, -1, LogicalConnections.and, "TelAviv", true);
            try
            {
                UserConditionPolicy p = (UserConditionPolicy)store.addSimplePurchasePolicy(data);
                Assert.AreEqual(true, p.getId() == 0, "UserConditionPolicy- id not insert correctly");
            }
            catch (Exception e)
            {
                Assert.AreEqual(true, false, "UserConditionPolicy-An object of the wrong type was created");
            }

        }

        //-------------------------------@@@ Complex path @@@---------------------------------------------------------------------------



        //-------------------------------@@@ StubClass @@@---------------------------------------------------------------------------







    }
}
