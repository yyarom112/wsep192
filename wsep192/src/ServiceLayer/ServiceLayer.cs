using src.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.ServiceLayer
{
    class ServiceLayer
    {
        public enum Permission
        {
            AddDiscountPolicy = 1,
            AddPurchasePolicy = 2,
            EditProductQuantityInStore = 3,
            AddProductToStore = 4,
            RemoveProductFromStore = 5,
            EditProductInStore = 6,
            RemoveManager = 7,
            RemoveOwner = 8,
            CommunicationWithCustomers = 9,
            PurchasesHistory = 10,
            AssignOwner = 11,
            AssignManager = 12,
            CloseStore = 13
        }
        private Dictionary<String, int> users = new Dictionary<String, int>();
        private Dictionary<String, int> stores = new Dictionary<String, int>();
        private int productCounter;
        private int storeCounter;
        private int userCounter;
        private int purchasePolicyCounter;
        private int discountPolicyCounter;
        private TradingSystem system = new TradingSystem(null, null);



        //req1.1
        public bool init(String adminName, String adminPassword)
        {
            //TODO users.Add(adminName,);
            system.init(adminName, adminPassword);
        }
        //req2.2
        public bool signIn(String username, String password, String user)
        {
            system.signIn(username, password, user);
         }
        //req2.3
        public bool register(String username, String password, String user) {
            system.register(username, password, user);
        }
        //req2.5
        public String searchProduct(String details) {
            system.searchProduct(details);
        }
        //req2.6
        public bool addProductsToCart(List<int, int> products, String store, String user) {
        system.addProductsToCart(List<int, int> products, String store, String user)
         }
        //req2.7
        public String showCart(int store, int user) {
            system.showCart(store,user);
        }
        public bool editProductQuantityInCart(int product, int quantity, String store, String user) { }
        public bool removeProductsFromCart(List<int, int> productsToRemove, String store, String user) { }
        //req2.8
        public int basketCheckout(String address, int user) { }
        public String payForBasket(long cardNum, DateTime date, int user) { }
        //req3.1
        public bool signout(int user) { }
        //req3.1
        public bool openStore(String storeName, int user) { }
        //req4.1
        public bool createNewProductInStore(String productName, String category, String details, int price, int store, int user) { }
        public bool addProductsInStore(List<int, int> productsToAdd, int store, int user) { }
        public bool removeProductsInStore(List<int, int> productsToRemove, int store, int user) { }
        public bool editProductInStore(int, String, String, String, int, int store, int user) { }
        //req4.3
        public bool assignOwner(int owner, int user, int store) { }
        //req4.4
        public bool removeOwner(int ownerToRemove, int store, int user) { }
        //req4.5
        public bool assignManager(int manager, int user, int store, List<int> permissions) { }
        //req4.6
        public bool removeManager(int managerToRemove, String store, String user) { }
        //req6.2
        public bool removeUser(int userToRemove, String user) { }


    }
}
