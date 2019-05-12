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
            Id = id;
            this.discountPrecentage = discountPrecentage;
            this.products = products ?? throw new ArgumentNullException(nameof(products));
            this.EndDateDiscount = endDateDiscount;
            this.logic = logic;
        }

        public DateTime EndDateDiscount { get => endDateDiscount; set => endDateDiscount = value; }

        public double calculate(List<KeyValuePair<ProductInStore, int>> productList)
        {
            double sum = 0;
            if (endDateDiscount < DateTime.Now)
                return sum;
            foreach (KeyValuePair< ProductInStore, int> product in productList)
            {
                if (products.ContainsKey(product.Key.Product.Id))
                    sum += (product.Key.Product.Price * product.Value) * discountPrecentage;
            }
            return sum;
        }

        public abstract bool checkCondition(List<KeyValuePair<ProductInStore, int>> productList);
    }
}
