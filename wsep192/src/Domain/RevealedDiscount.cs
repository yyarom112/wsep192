using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class RevealedDiscount : DiscountPolicy
    {
        private int Id;
        private double discountPrecentage;
        private Dictionary<int, KeyValuePair<ProductInStore, int>> products; // Dictionary<ProductID, KeyValuePair<ProductInStore, minquntity to get the discount>>
        private DateTime endDateDiscount;
        private DuplicatePolicy logic;

        public RevealedDiscount(int id, double discountPrecentage, Dictionary<int, KeyValuePair<ProductInStore, int>> products, DateTime endDateDiscount, DuplicatePolicy logic)
        {
            Id = id;
            this.discountPrecentage = discountPrecentage;
            this.products = products ?? throw new ArgumentNullException(nameof(products));
            this.EndDateDiscount = endDateDiscount;
            this.logic = logic;
        }

        public DateTime EndDateDiscount { get => endDateDiscount; set => endDateDiscount = value; }

        //arg is list of product and quntity to buy
        public double calculate(List<KeyValuePair<ProductInStore, int>> productList)
        {
            double sum = 0;
            if (endDateDiscount < DateTime.Now)
                return -1;
            foreach (KeyValuePair<ProductInStore, int> product in productList)
            {
                if (products.ContainsKey(product.Key.Product.Id) && product.Value >= this.products[product.Key.Product.Id].Value)
                    sum += (product.Key.Product.Price * product.Value) * discountPrecentage;
            }
            return sum;
        }


        //If there is one product in the list that fits the discount and the customer wants to buy
        //more than the minimum quantity then return the true. Otherwise, you will return a false.
        public bool checkCondition(List<KeyValuePair<ProductInStore, int>> productList)
        {
            if (endDateDiscount < DateTime.Now)
                return false;
            foreach(KeyValuePair<ProductInStore, int> product in productList)
            {
                if (products.ContainsKey(product.Key.Product.Id) && product.Value >= this.products[product.Key.Product.Id].Value)
                    return true;
            }
            return false;
        }
    }
}
