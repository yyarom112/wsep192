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

        public IfThenCondition(int id, PurchasePolicy ifCond, PurchasePolicy thenCond)
        {
            this.id = id;
            this.ifCond = ifCond ?? throw new ArgumentNullException(nameof(ifCond));
            this.thenCond = thenCond ?? throw new ArgumentNullException(nameof(thenCond));
        }

        public bool CheckCondition(List<KeyValuePair<ProductInStore, int>> cart, UserDetailes user)
        {
            if (ifCond.CheckCondition(cart, user))
                return thenCond.CheckCondition(cart, user);
            return false;
        }
        public int getId()
        {
            return id;
        }
    }
}
