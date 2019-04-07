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
            this.ProductName = productName;
            this.Category = category;
            this.Keyword = keyword;
            this.PriceRange = priceRange;
            this.ProductRate = productRate;
            this.StoreRate = storeRate;
        }

        public string ProductName { get => productName; set => productName = value; }
        public string Category { get => category; set => category = value; }
        public string Keyword { get => keyword; set => keyword = value; }
        public KeyValuePair<int, int> PriceRange { get => priceRange; set => priceRange = value; }
        public int ProductRate { get => productRate; set => productRate = value; }
        public int StoreRate { get => storeRate; set => storeRate = value; }
    }
}
