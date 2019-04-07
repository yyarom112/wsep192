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
    }
}
