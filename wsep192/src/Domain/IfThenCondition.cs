using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Domain.Dataclass;

namespace src.Domain
{
    class IfThenCondition : PurchasePolicy
    {
        private int id;
        private PurchasePolicy ifCond;
        private PurchasePolicy thenCond;

        public bool CheckCondition(List<KeyValuePair<ProductInStore, int>> cart, UserDetailes user)
        {
            throw new NotImplementedException();
        }
    }
}
