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
            this.ShoppingCarts = new Dictionary<int, ShoppingCart>();
        }
        public virtual int basketCheckout()
        {
            int sum = 0;
            int tmp = 0;
            foreach (ShoppingCart c in shoppingCarts.Values)
            {
                tmp = c.cartCheckout();
                if (tmp == -1)
                    return -1;
                sum += tmp;
            }
            return sum;

        }
        internal Dictionary<int, ShoppingCart> ShoppingCarts { get => shoppingCarts; set => shoppingCarts = value; }

        public ShoppingCart addProductsToCart(LinkedList<KeyValuePair<Product, int>> productsToInsert, int storeID)
        {
            bool exist = true;
            if (productsToInsert.Count == 0)
                return null;
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
