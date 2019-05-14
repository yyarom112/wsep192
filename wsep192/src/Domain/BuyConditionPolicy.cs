using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Domain.Dataclass;

namespace src.Domain
{
    class BuyConditionPolicy : PurchasePolicy
    {
        private int id;
        private int min;
        private int max;
        private int sumMin;
        private int sumMax;
        private LogicalConnections act;

        public BuyConditionPolicy(int id, int min, int max, int sumMin, int sumMax, LogicalConnections act)
        {
            this.id = id;
            this.Min = min;
            this.Max = max;
            this.SumMin = sumMin;
            this.SumMax = sumMax;
            this.act = act;
        }

        public int Min { get => min; set => min = value; }
        public int Max { get => max; set => max = value; }
        public int SumMin { get => sumMin; set => sumMin = value; }
        public int SumMax { get => sumMax; set => sumMax = value; }

        public bool CheckCondition(List<KeyValuePair<ProductInStore, int>> cart, UserDetailes user)
        {
            double sum = 0;
            int totalProducts = 0;

            foreach (KeyValuePair<ProductInStore, int> product in cart)
            {
                totalProducts += product.Value;
                sum += product.Key.Product.Price * product.Value;
            }

            if (Min != -1 && totalProducts < Min)
                return false;
            if (Max != -1 && totalProducts > Max)
                return false;
            if (SumMin != -1 && sum < SumMin)
                return false;
            if (SumMax != -1 && sum > SumMax)
                return false;
            return true;
        }

        public int getId()
        {
            return id;
        }
    }
}
