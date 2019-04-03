using System;
using System.Collections.Generic;

namespace src.Domain
{
    class Filter
    {
        private String productName;
        private String category;
        private String keyword;
        private KeyValuePair <int, int> priceRange;
        private int productRate;
        private int storeRate;
        public Filter(String productName,
            String category, String keyword,
            KeyValuePair<int, int> priceRange,
            int productRate, int storeRate)
        {
            this.productName = productName;
            this.category = category;
            this.keyword = keyword;
            this.priceRange = priceRange;
            this.productRate = productRate;
            this.storeRate = storeRate;
        }
        public String getProductName()
        {
            return this.productName;
        }
        public String getCategory()
        {
            return this.category;
        }
        public String getKeyword()
        {
            return this.keyword;
        }
        public KeyValuePair<int,int> getPriceRange()
        {
            return this.priceRange;
        }
        public int getProductRate()
        {
            return this.productRate;
        }
        public int getStoreRate()
        {
            return this.storeRate;
        }
    }
}
