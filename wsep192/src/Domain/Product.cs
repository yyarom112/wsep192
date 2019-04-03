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

        public int Id { get { return id; } set { id = value; } }
        public string ProductName { get { productName; set => productName = value; }
        public string Category { get => category; set => category = value; }
        public string Details { get => details; set => details = value; }
        public int Price { get => price; set => price = value; }
        public int ProductRate { get => productRate; set => productRate = value; }
    }
}
