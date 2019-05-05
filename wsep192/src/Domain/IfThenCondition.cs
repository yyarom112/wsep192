using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class IfThenCondition : PurchasePolicy
    {
        private int id;
        private PurchasePolicy ifCond;
        private PurchasePolicy thenCond;

        public bool CheckCondition(List<KeyValuePair<ProductInStore, int>> cart)
        {
            throw new NotImplementedException();
        }
    }
}
