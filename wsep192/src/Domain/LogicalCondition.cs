using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class LogicalCondition : ConditionalDiscount
    {
        private LogicalConnections logical;
        private List<ConditionalDiscount> Children;

        public override bool checkCondition(List<KeyValuePair<ProductInStore, int>> productList)
        {
            throw new NotImplementedException();
        }
        public bool addChild(ConditionalDiscount discount)
        {
            throw new NotImplementedException();
        }
        public bool removeChild(ConditionalDiscount discount)
        {
            throw new NotImplementedException();
        }
        public bool getChild(ConditionalDiscount discount)
        {
            throw new NotImplementedException();
        }
    }
}
