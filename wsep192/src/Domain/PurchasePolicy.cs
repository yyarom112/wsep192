using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class PurchasePolicy
    {
        private int id;
        private String details;
        private Dictionary<int, ProductInStore> products;

        public PurchasePolicy(int id, string details, Dictionary<int, ProductInStore> products)
        {
            this.id = id;
            this.details = details;
            this.products = products;
        }
        public bool confirmPolicy()
        {
            return true;
        }
        public int Id { get => id; set => id = value; }
        public string Details { get => details; set => details = value; }
        internal Dictionary<int, ProductInStore> Products { get => products; set => products = value; }
    }
}
