using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Domain.Dataclass;

namespace src.Domain
{
    class ProductConditionPolicy : PurchasePolicy
    {
        private int id;
        private int productID;
        private int min;
        private int max;
        private LogicalConnections act;

        public ProductConditionPolicy( int id, int productID, int min, int max, LogicalConnections act)
        {
            this.id = id;
            this.ProductID = productID;
            this.min = min;
            this.max = max;
            this.act = act;
        }

        public int ProductID { get => productID; set => productID = value; }

        public bool CheckCondition(List<KeyValuePair<ProductInStore, int>> cart, UserDetailes user)
        {
            foreach (KeyValuePair<ProductInStore, int> product in cart)
            {
                if (product.Key.Product.Id == this.ProductID)
                {
                    if (max != -1)
                    {
                        if (product.Value > max)
                            return false;
                    }
                    if (min != -1)
                    {
                        if (product.Value < min)
                            return false;
                    }
                    if (product.Value < 0)
                        return false;
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
