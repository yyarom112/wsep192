using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class BuyConditionPolicy : PurchasePolicy
    {
        private int id;
        private int min;
        private int max;
        private int sumMin;
        private int sumMax;

        public bool CheckCondition(List<KeyValuePair<ProductInStore, int>> cart)
        {
            throw new NotImplementedException();
        }
    }
}
