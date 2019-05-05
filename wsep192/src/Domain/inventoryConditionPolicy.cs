using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class inventoryConditionPolicy : PurchasePolicy
    {
        protected int id;
        protected int productID;
        protected int min;
        protected int max;
        protected LogicalConnections act;

        public bool CheckCondition(List<KeyValuePair<ProductInStore, int>> cart)
        {
            throw new NotImplementedException();
        }
    }
}
