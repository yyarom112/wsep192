using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Domain.Dataclass;

namespace src.Domain
{
    class inventoryConditionPolicy : PurchasePolicy
    {
        private int id;
        private int productID;
        private int min;
        private LogicalConnections act;

        public inventoryConditionPolicy(int id, int productID, int min, LogicalConnections act)
        {
            this.id = id;
            this.productID = productID;
            this.min = min;
            this.act = act;
        }

        public bool CheckCondition(List<KeyValuePair<ProductInStore, int>> cart, UserDetailes user)
        {
            foreach (KeyValuePair<ProductInStore, int> product in cart)
            {
                if (product.Key.Product.Id == this.productID)
                {
                    if (product.Key.Quantity - product.Value < min)
                        return false;
                    else
                    {
                        return true;
                    }
                }
            }
            return true;
        }

        public int getId()
        {
            return id;
        }
    }
}
