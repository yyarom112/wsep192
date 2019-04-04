using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class System
    {
        private Dictionary<int,User> users;
        private Dictionary<int,Store> stores;
        private ProductSupplySystem supplySystem;
        private FinancialSystem financialSystem;
        private int productCounter;
        private int storeCounter;
        private int userCounter;
        private int purchasePolicyCounter;
        private int discountPolicyCounter;



        internal Dictionary<Int32, User> Users
        {
            get
            {
                return users;
            }

            set
            {
                users = value;
            }
        }

        internal Dictionary<Int32, Store> Stores
        {
            get
            {
                return stores;
            }

            set
            {
                stores = value;
            }
        }

        internal ProductSupplySystem SupplySystem
        {
            get
            {
                return supplySystem;
            }

            set
            {
                supplySystem = value;
            }
        }

        internal FinancialSystem FinancialSystem
        {
            get
            {
                return financialSystem;
            }

            set
            {
                financialSystem = value;
            }
        }

        public Int32 ProductCounter
        {
            get
            {
                return productCounter;
            }

            set
            {
                productCounter = value;
            }
        }

        public Int32 StoreCounter
        {
            get
            {
                return storeCounter;
            }

            set
            {
                storeCounter = value;
            }
        }

        public Int32 UserCounter
        {
            get
            {
                return userCounter;
            }

            set
            {
                userCounter = value;
            }
        }

        public Int32 PurchasePolicyCounter
        {
            get
            {
                return purchasePolicyCounter;
            }

            set
            {
                purchasePolicyCounter = value;
            }
        }

        public Int32 DiscountPolicyCounter
        {
            get
            {
                return discountPolicyCounter;
            }

            set
            {
                discountPolicyCounter = value;
            }
        }


        public void addProductsToCart(LinkedList<KeyValuePair<int, int>> productsToInsert, int storeID , int userID)
        {           
            if(!this.Users.ContainsKey(userID)||!this.Stores.ContainsKey(storeID))
            {
                Console.WriteLine("The store or user does not exist\n");
            }
            this.Users[userID].addProductsToCart(searchProductById(productsToInsert, storeID), storeID);


        }

        private LinkedList<KeyValuePair<Product, int>> searchProductById(LinkedList<KeyValuePair<int, int>> productsToInsert , int storeId)
        {
            LinkedList<KeyValuePair<Product, int>> output = new LinkedList<KeyValuePair<Product, int>>();
            Store store = this.stores[storeId];
            foreach(KeyValuePair<int, int> product in productsToInsert)
            {
                if (!store.Products.ContainsKey(product.Key))
                {
                    output.AddLast(new KeyValuePair<Product, int>(store.Products[product.Key].Product, product.Value));
                }

            }
            return output;
        }



    }
}
