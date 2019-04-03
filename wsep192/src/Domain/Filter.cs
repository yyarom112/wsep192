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

        public Filter(String productName, String category, String keyword, KeyValuePair<Int32, Int32> priceRange, Int32 productRate, Int32 storeRate)
        {
            this.productName = productName;
            this.category = category;
            this.keyword = keyword;
            this.priceRange = priceRange;
            this.productRate = productRate;
            this.storeRate = storeRate;
        }

        public String ProductName
        {
            get
            {
                return productName;
            }

            set
            {
                productName = value;
            }
        }

        public String Category
        {
            get
            {
                return category;
            }

            set
            {
                category = value;
            }
        }

        public String Keyword
        {
            get
            {
                return keyword;
            }

            set
            {
                keyword = value;
            }
        }

        public KeyValuePair<Int32, Int32> PriceRange
        {
            get
            {
                return priceRange;
            }

            set
            {
                priceRange = value;
            }
        }

        public Int32 ProductRate
        {
            get
            {
                return productRate;
            }

            set
            {
                productRate = value;
            }
        }

        public Int32 StoreRate
        {
            get
            {
                return storeRate;
            }

            set
            {
                storeRate = value;
            }
        }
    }
}
