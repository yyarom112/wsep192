using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    interface DiscountPolicy
    {
        bool checkCondition(List<KeyValuePair<ProductInStore, int>> productList);
        double calculate(List<KeyValuePair<ProductInStore, int>> productList);
        void UpdateProductPrice(List<KeyValuePair<ProductInStore, int>> productList);
        Dictionary<int, ProductInStore> GetRelevantProducts(); 
        DuplicatePolicy GetDuplicatePolicy();
        void removeProduct(ProductInStore product);
        DiscountPolicy copy();
    }
}
