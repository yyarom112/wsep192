using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Common;

namespace src.Domain
{
    class Store
    {
        private int id;
        private String name;
        private Dictionary<int, ProductInStore> products;
        private int storeRate;
        private ITree<Role> roles;
        private List<PurchasePolicy> purchasePolicy;
        private List<DiscountPolicy> discountPolicy;

        public Store(int id, string name, int storeRate, List<PurchasePolicy> purchasePolicy, List<DiscountPolicy> discountPolicy)
        {
            this.id = id;
            this.name = name;
            this.products = new Dictionary<int, ProductInStore>();
            this.storeRate = storeRate;
            this.roles = NodeTree<Role>.NewTree();
            this.purchasePolicy = purchasePolicy;
            this.discountPolicy = discountPolicy;
        }
        public void updateCart(ShoppingCart cart)
        {
            foreach (ProductInCart p in cart.Products.Values)
            {
                if (p.Quantity <= this.products[p.Product.Id].Quantity)
                    this.products[p.Product.Id].Quantity -= p.Quantity;
                else
                {
                    p.Quantity = this.products[p.Product.Id].Quantity;
                    this.products[p.Product.Id].Quantity = 0;
                }
            }
        }
        public bool confirmPurchasePolicy(Dictionary<int,ProductInCart> products)
        {
            List<ProductInStore> productsInStore = new List<ProductInStore>();
            foreach(ProductInCart p in products.Values)
            {
                ProductInStore productInStore = new ProductInStore(p.Quantity, this, p.Product);
                productsInStore.Add(productInStore);
            }
            foreach(PurchasePolicy pp in purchasePolicy)
            {
                if (!pp.confirmPolicy())
                    return false;
            }
            return true;

        }
        public int calculateDiscountPolicy(Dictionary<int, ProductInCart> products)
        {
            int sum = 0;
            List<ProductInStore> productsInStore = new List<ProductInStore>();
            foreach (ProductInCart p in products.Values)
            {
                ProductInStore productInStore = new ProductInStore(p.Quantity, this, p.Product);
                productsInStore.Add(productInStore);
            }
            foreach (DiscountPolicy dp in discountPolicy)
            {
                sum += dp.calculate(productsInStore);
            }
            return sum;
        }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int StoreRate { get => storeRate; set => storeRate = value; }
        internal Dictionary<int, ProductInStore> Products { get => products; set => products = value; }
        internal ITree<Role> Roles { get => roles; set => roles = value; }
        internal List<PurchasePolicy> PurchasePolicy { get => purchasePolicy; set => purchasePolicy = value; }
        internal List<DiscountPolicy> DiscountPolicy { get => discountPolicy; set => discountPolicy = value; }
    }
}
