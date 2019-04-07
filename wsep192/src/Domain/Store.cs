using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public bool searchProduct(Filter filter, List<ProductInStore> listToAdd)
        {
            bool result = false;
            foreach (ProductInStore p in products.Values)
            {
                if (p.Product.compareProduct(filter) && filter.StoreRate != -1 && filter.StoreRate == this.storeRate)
                {
                    listToAdd.Add(p);
                    result = true;
                }
            }
            return result;
        }

        public virtual void updateCart(ShoppingCart cart, String opt)
        {
            foreach (ProductInCart p in cart.Products.Values)
            {
                if (opt.Equals("-"))
                {
                    if (p.Quantity <= this.products[p.Product.Id].Quantity)
                        if (opt.Equals("-"))
                            this.products[p.Product.Id].Quantity -= p.Quantity;

                        else
                        {
                            p.Quantity = this.products[p.Product.Id].Quantity;
                            this.products[p.Product.Id].Quantity = 0;
                        }
                }
                else
                {
                    this.products[p.Product.Id].Quantity += p.Quantity;
                }
                
            }
        }
        public virtual bool confirmPurchasePolicy(Dictionary<int,ProductInCart> products)
        {
            if (this.PurchasePolicy == null)
                return true;
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
        public virtual int calculateDiscountPolicy(Dictionary<int, ProductInCart> products)
        {
            if (this.DiscountPolicy == null)
                return 0;
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
