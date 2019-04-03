using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class Product
    {
        private int id;
        private String productName;
        private String category;
        private String details;
        private int price;
        private int productRate;
        public bool compareProduct(Filter filter)
        {
            if (!filter.getProductName().Equals(productName))
                return false;
            if (filter.getCategory() != "" && !filter.getCategory().Equals(category))
                return false;
            if (filter.getProductRate() != -1 && filter.getProductRate() != productRate)
                return false;
            /*if() TODO: fix details and compare them
                return false;*/
            return true;
        }
    }
}
