using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class RevealedDiscount : DiscountPolicy
    {
        private int Id;
        private double discountPrecentage;
        private Dictionary<int, ProductInStore> products;
        private DateTime endDateDiscount;
        private DuplicatePolicy logic;

        public int calculate(List<KeyValuePair<ProductInStore, int>> productList)
        {
            throw new NotImplementedException();
        }

        public bool checkCondition(List<KeyValuePair<ProductInStore, int>> productList)
        {
            throw new NotImplementedException();
        }
    }
}
