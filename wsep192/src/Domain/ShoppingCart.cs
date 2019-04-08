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
            this.products = new Dictionary< int, ProductInCart >();
            this.storeId = storeId;
        }

        public int StoreId
        {
            get { return storeId; }
            set { storeId = value; }
        }
        internal Store Store
        {
            get { return store; }
            set { store = value; }
        }
        internal Dictionary<int, ProductInCart> Products
        { get { return products; } set { products = value; } }

        public void addProducts(LinkedList<KeyValuePair<Product, int>> productsToInsert)
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

        public virtual int cartCheckout()
        {
            if (!store.confirmPurchasePolicy(products))
            {
                LogManager.Instance.WriteToLog("ShoppingCart- cart checkout failed- Buying cart " + StoreId + " does not match the story policy\n");
                return -1;
            }
            int sum = 0;
            foreach (ProductInCart p in products.Values)
                sum += p.Product.Price * p.Quantity;
            int discount = store.calculateDiscountPolicy(products);
            return sum - discount;


        }



        }
    }
