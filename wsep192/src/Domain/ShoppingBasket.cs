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

        internal Dictionary<int, ShoppingCart> ShoppingCarts { get => shoppingCarts; set => shoppingCarts = value; }

        internal string showCart(int storeId)
        {
            if (!shoppingCarts.ContainsKey(storeId)) 
                return "Error : Shopping basket does not contain this store";

            return shoppingCarts[storeId].showCart();
        }

        internal bool removeProductsFromCart(List<KeyValuePair<int, int>> productsToRemove, int storeId)
        {
            if (!shoppingCarts.ContainsKey(storeId))
                return false;
            return shoppingCarts[storeId].removeProductsFromCart(productsToRemove);
        }
        internal bool editProductQuantityInCart(int productId, int quantity, int storeId)
        {
            if (!shoppingCarts.ContainsKey(storeId))
                return false;
            return shoppingCarts[storeId].editProductQuantityInCart(productId, quantity);
        }
    }
}
