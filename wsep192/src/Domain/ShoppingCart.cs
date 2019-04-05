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
        private Dictionary<int,ProductInCart> products;

        public ShoppingCart(int storeId, Store store, Dictionary<int, ProductInCart> products)
        {
            this.storeId = storeId;
            this.store = store;
            this.products = products;
        }

        public int StoreId { get => storeId; set => storeId = value; }
        internal Store Store { get => store; set => store = value; }
        internal Dictionary<int, ProductInCart> Products { get => products; set => products = value; }

        internal string showCart()
        {
            return createOutputTable();
        }

        private string createOutputTable()
        {
            string table = "Store Name: "+store.Name+"\n";
            int idx = 0;
            table += "Product Name\t\t\tQuantity\n";
            foreach (int key in products.Keys)
            {
                idx++;
                ProductInCart p = products[key];
                table += idx + ". " + p.Product.ProductName + "\t\t\t" + p.Quantity + "\n";
            }


            return table;
        }
    }
}
