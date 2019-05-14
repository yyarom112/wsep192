using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class Role
    {
        protected Store store;
        protected User user;

        public Role(Store store, User user)
        {
            this.store = store;
            this.user = user;
        }

        internal Store Store { get { return store; } set { store = value; } }
        internal User User { get { return user; } set { user = value; } }

        public virtual PurchasePolicy addSimplePurchasePolicy(PurchesPolicyData purchesData )
        {
            return store.addSimplePurchasePolicy(purchesData);
        }

        public virtual PurchasePolicy addComplexPurchasePolicy(int ID,String purchesData)
        {
            return store.addComplexPurchasePolicy(ID, purchesData);
        }
    }
}
