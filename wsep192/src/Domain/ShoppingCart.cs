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
            this.storeId = storeId;
        }
        public virtual int cartCheckout()
        {
            if (!store.confirmPurchasePolicy(products))
                return -1;
            int sum = 0;
            foreach (ProductInCart p in products.Values)
                sum += p.Product.Price * p.Quantity;
            int discount = store.calculateDiscountPolicy(products);
            return sum - discount;
            
        }
        
        public int StoreId { get => storeId; set => storeId = value; }
        internal Store Store { get => store; set => store = value; }
        internal Dictionary<int, ProductInCart> Products { get => products; set => products = value; }
    }
}
