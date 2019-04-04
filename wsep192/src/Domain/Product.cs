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

        public Product(int id, string productName, string category, string details, int price, int productRate)
        {
            this.Id = id;
            this.ProductName = productName;
            this.Category = category;
            this.Details = details;
            this.Price = price;
            this.ProductRate = productRate;
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
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

        public String Details
        {
            get
            {
                return details;
            }

            set
            {
                details = value;
            }
        }

        public int Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
            }
        }

        public int ProductRate
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

        


    }
}
