using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class LeafCondition : ConditionalDiscount
    {
        private Dictionary<int, KeyValuePair<ProductInStore, int>> relatedProducts; //<productID , <Product, minimum quntitiy to buy>>

        public LeafCondition(Dictionary<int, KeyValuePair<ProductInStore, int>> relatedProducts,int id, double discountPrecentage, Dictionary<int, ProductInStore> products, DateTime endDateDiscount, DuplicatePolicy dup):base(id, discountPrecentage, products, endDateDiscount, dup)
        {
            this.relatedProducts = relatedProducts ?? throw new ArgumentNullException(nameof(relatedProducts));
        }


        //If any product related to the discount is listed in the appropriate quantity,
        //you will return the true
        public override bool checkCondition(List<KeyValuePair<ProductInStore, int>> productList)
        {
            int sumOfProducts = 0;
            foreach (KeyValuePair<ProductInStore, int> product in productList)
            {
                //If the product exists in the list but not in the appropriate quantity will return a false
                if (relatedProducts.ContainsKey(product.Key.Product.Id) && product.Value < this.relatedProducts[product.Key.Product.Id].Value)
                    return false;
                //If the product exists in the list will increase the amount of suitable products
                if (relatedProducts.ContainsKey(product.Key.Product.Id))
                    sumOfProducts++;
            }
            //If all products are in the correct quantity will return true
            if (sumOfProducts== relatedProducts.Count)
                return true;
            return false;
        }
    }
}
