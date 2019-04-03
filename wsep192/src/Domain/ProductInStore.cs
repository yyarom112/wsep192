using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class ProductInStore
    {
        private int quantity;
        private Store store;
        private Product product;

        public ProductInStore(int quantity, Store store, Product product)
        {
            this.Quantity = quantity;
            this.Store = store;
            this.Product = product;
        }

        public int Quantity { get => quantity; set => quantity = value; }
        internal Store Store { get => store; set => store = value; }
        internal Product Product { get => product; set => product = value; }
    }
}
