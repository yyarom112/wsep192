using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class LeafCondition : ConditionalDiscount
    {
        private Dictionary<int, KeyValuePair<ProductInStore, int>> relatedProducts;

        public override bool checkCondition(List<KeyValuePair<ProductInStore, int>> productList)
        {
            throw new NotImplementedException();
        }
    }
}
