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
        int calculate(List<KeyValuePair<ProductInStore, int>> productList);
    }
}
