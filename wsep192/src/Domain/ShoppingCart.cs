using src.Domain.Dataclass;
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
        private Dictionary<int, ProductInCart> products;

        public ShoppingCart(int storeId, Store store)
        {
            this.storeId = storeId;
            this.store = store;
            this.products = new Dictionary<int, ProductInCart>();
        }

        public int StoreId { get => storeId; set => storeId = value; }
        internal Store Store { get => store; set => store = value; }
        internal Dictionary<int, ProductInCart> Products { get => products; set => products = value; }


        public virtual int cartCheckout(UserDetailes user)
        {
            if (!store.confirmPurchasePolicy(products, user))
            {
                LogManager.Instance.WriteToLog("Failed to close cart due to a purchase attempt that contradicts the store's purchase policy.\n");
                return -1;
            }
            int sum = 0;
            //foreach (ProductInCart p in products.Values)
                //    sum += p.Product.Price * p.Quantity;
                //double discount = store.calculateDiscountPolicy(products);
                //return sum - discount;
                return 0;

        }


        internal virtual List<KeyValuePair<string, int>> showCart()
        {
            LogManager.Instance.WriteToLog("ShoppingCart:showCart success\n");
            return createOutputTable();
        }

        private List<KeyValuePair<string, int>> createOutputTable()
        {
            List<KeyValuePair<string, int>> table = new List<KeyValuePair<string, int>>();
            int idx = 0;
            foreach (int key in products.Keys)
            {
                idx++;
                ProductInCart p = products[key];
                KeyValuePair<string, int> pair = new KeyValuePair<string, int>(p.Product.ProductName, p.Quantity);
                table.Add(pair);
            }

            return table;
        }

        public virtual void addProducts(LinkedList<KeyValuePair<Product, int>> productsToInsert)
        {
            foreach (KeyValuePair<Product, int> toInsert in productsToInsert)
            {
                if (this.products.ContainsKey(toInsert.Key.Id))
                {
                    products[toInsert.Key.Id].Quantity = this.products[toInsert.Key.Id].Quantity + toInsert.Value;
                }
                else
                {
                    products.Add(toInsert.Key.Id, new ProductInCart(toInsert.Value, this, toInsert.Key));
                }
            }

        }

        internal virtual bool editProductQuantityInCart(int productId, int quantity)
        {
            if (!products.ContainsKey(productId))
            {
                LogManager.Instance.WriteToLog("ShoppingCart:editProductQuantityInCart failed - shopping cart does not contain the product\n");
                return false;
            }
            products[productId].Quantity = quantity;
            LogManager.Instance.WriteToLog("ShoppingCart:editProductQuantityInCart success\n");
            return true;
        }

        internal virtual bool removeProductsFromCart(List<int> productsToRemove)
        {
            foreach (int p in productsToRemove)
            {
                if (!products.ContainsKey(p))
                {
                    LogManager.Instance.WriteToLog("ShoppingCart:removeProductQuantityFromCart failed - shopping cart does not contain the product " +
                        "or invalid quantity\n");
                    return false;
                }
            }
            foreach ( int p in productsToRemove)
            {
                    products.Remove(p);
            }
            LogManager.Instance.WriteToLog("ShoppingCart:removeProductsFromCart success\n");
            return true;
        }
    }
}
