using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Domain.Dataclass;

namespace src.Domain
{
    class LogicalConditionPolicy : PurchasePolicy
    {
        private int id;
        private LogicalConnections log;
        private List<PurchasePolicy> children;

        public bool addChild(PurchasePolicy p)
        {
            throw new NotImplementedException();
        }
        public bool removeChild(PurchasePolicy p)
        {
            throw new NotImplementedException();
        }
        public bool getChild(int id)
        {
            throw new NotImplementedException();
        }

        public bool CheckCondition(List<KeyValuePair<ProductInStore, int>> cart, UserDetailes user)
        {
            throw new NotImplementedException();
        }
    }
}
