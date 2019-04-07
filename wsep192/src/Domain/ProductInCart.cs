using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class ProductInCart
    {
        private int quantity;
        private ShoppingCart shoppingCart;
        private Product product;

        public ProductInCart(int quantity, ShoppingCart shoppingCart, Product product)
        {
            this.Quantity = quantity;
            this.ShoppingCart = shoppingCart;
            this.Product = product;
        }

        public int Quantity { get { return quantity; } set { quantity = value; } }
        internal ShoppingCart ShoppingCart { get { return shoppingCart; } set { shoppingCart = value; } }
        internal Product Product { get {return product; } set { product = value; }
        }
    }
}
