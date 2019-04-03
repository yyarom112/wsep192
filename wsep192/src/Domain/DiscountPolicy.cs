using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class DiscountPolicy
    {
        private int id;
        private String details;
        private Dictionary<int, ProductInStore> products;


        public DiscountPolicy(int id, String details, Dictionary<int, ProductInStore> products)
        {
            this.Id = id;
            this.Details = details;
            this.Products = products;
        }

        public int Id { get => id; set => id = value; }
        public string Details { get => details; set => details = value; }
        internal Dictionary<int, ProductInStore> Products { get => products; set => products = value; }
    }
}
