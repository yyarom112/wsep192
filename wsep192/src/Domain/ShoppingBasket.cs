using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class ShoppingBasket
    {
        private Dictionary<int, ShoppingCart> shoppingCarts;

        public ShoppingBasket()
        {
            this.shoppingCarts = new Dictionary<int, ShoppingCart>(); 
        }

        internal Dictionary<int, ShoppingCart> ShoppingCarts { get { return shoppingCarts; } set { shoppingCarts = value; } }

        public ShoppingCart addProductsToCart(LinkedList<KeyValuePair<Product, int>> productsToInsert, int storeID)
        {
            bool exist = true;
            if (!this.shoppingCarts.ContainsKey(storeID))
            {
                exist = false;
                shoppingCarts.Add(storeID, new ShoppingCart(storeID, null));
            
            }
            shoppingCarts[storeID].addProducts(productsToInsert);
            if (!exist)
                return shoppingCarts[storeID];
            return null;
        }
    }
}
