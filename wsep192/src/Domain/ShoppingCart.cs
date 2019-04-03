using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class ShoppingCart
    {
        private int storeId;
        private Store store;
        private Dictionary<int, KeyValuePair<ProductInCart, int>> products;



        public bool addProducts(LinkedListNode<KeyValuePair<Product, int>> productsToInsert)
        {
            bool output= true;
            if (productsToInsert == null)
                output = false;
            foreach (KeyValuePair<Product, int> product in productsToInsert.List)
            {
                if (products.ContainsKey(product.Key.getId()))
                {

                }
            }

        }


        public ShoppingCart(int storeId, Store store, Dictionary<int, ProductInCart> products)
        {
            this.storeId = storeId;
            this.store = store;
            this.products = products;
            this.storeId = storeId;
        }

        public int StoreId { get => storeId; set => storeId = value; }
        internal Store Store { get => store; set => store = value; }
        internal Dictionary<int, ProductInCart> Products { get => products; set => products = value; }
    }
}
