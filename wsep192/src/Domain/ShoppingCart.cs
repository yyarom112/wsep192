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


    }
}
