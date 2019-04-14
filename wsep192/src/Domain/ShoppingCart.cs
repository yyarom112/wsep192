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


        internal virtual string showCart()
        {
            LogManager.Instance.WriteToLog("ShoppingCart:showCart success\n");
            return createOutputTable();
        }

        private string createOutputTable()
        {
            string table = "Store Name: " + store.Name + "\n";
            if (products.Count == 0)
                return table + "Cart is empty\n";
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

        internal virtual bool removeProductsFromCart(List<KeyValuePair<int, int>> productsToRemove)
        {
            foreach (KeyValuePair<int, int> pair in productsToRemove)
            {
                if (!products.ContainsKey(pair.Key) || products[pair.Key].Quantity < pair.Value)
                    LogManager.Instance.WriteToLog("ShoppingCart:removeProductQuantityFromCart failed - shopping cart does not contain the product " +
                        "or invalid quantity\n");
                return false;
            }
            foreach (KeyValuePair<int, int> pair in productsToRemove)
            {
                products[pair.Key].Quantity -= pair.Value;
            }
            LogManager.Instance.WriteToLog("ShoppingCart:removeProductsFromCart success\n");
            return true;
        }
    }
}
