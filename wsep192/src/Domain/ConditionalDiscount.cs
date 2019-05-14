using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    abstract class ConditionalDiscount : DiscountPolicy
    {

        private int Id;
        private double discountPrecentage;
        private Dictionary<int, ProductInStore> products;
        private DateTime endDateDiscount;
        private DuplicatePolicy logic;

        protected ConditionalDiscount(int id, double discountPrecentage, Dictionary<int, ProductInStore> products, DateTime endDateDiscount, DuplicatePolicy logic)
        {
            Id1 = id;
            this.DiscountPrecentage = discountPrecentage;
            this.Products = products ;
            this.EndDateDiscount = endDateDiscount;
            this.Logic = logic;
        }

        public DateTime EndDateDiscount { get => EndDateDiscount1; set => EndDateDiscount1 = value; }
        internal Dictionary<int, ProductInStore> Products { get => Products1; set => Products1 = value; }
        public int Id1 { get => Id; set => Id = value; }
        public double DiscountPrecentage { get => discountPrecentage; set => discountPrecentage = value; }
        internal Dictionary<int, ProductInStore> Products1 { get => products; set => products = value; }
        public DateTime EndDateDiscount1 { get => endDateDiscount; set => endDateDiscount = value; }
        internal DuplicatePolicy Logic { get => logic; set => logic = value; }

        public double calculate(List<KeyValuePair<ProductInStore, int>> productList)
        {
            double sum = 0;
            if (EndDateDiscount1 < DateTime.Now)
                return sum;
            foreach (KeyValuePair< ProductInStore, int> product in productList)
            {
                if (Products.ContainsKey(product.Key.Product.Id))
                    sum += (product.Key.Product.Price * product.Value) * DiscountPrecentage;
            }
            return sum;
        }

        public abstract bool checkCondition(List<KeyValuePair<ProductInStore, int>> productList);

        public DuplicatePolicy GetDuplicatePolicy()
        {
            return Logic;
        }

        public void UpdateProductPrice(List<KeyValuePair<ProductInStore, int>> productList)
        {
            List<KeyValuePair<ProductInStore, int>> output = new List<KeyValuePair<ProductInStore, int>>();
            foreach (KeyValuePair<ProductInStore, int> product in productList)
            {
                product.Key.Product.Price -= (product.Key.Product.Price * DiscountPrecentage);
            }
        }
        public Dictionary<int, ProductInStore> GetRelevantProducts()
        {
            return this.Products;
        }
        public void removeProduct(ProductInStore product)
        {
            this.Products1.Remove(product.Product.Id);
        }

        public abstract DiscountPolicy copy();

    }
}
