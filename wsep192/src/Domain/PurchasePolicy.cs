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
            this.Id = id;
            this.Details = details;
            this.Products = products;
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

        internal Dictionary<int, ProductInStore> Products
        {
            get
            {
                return products;
            }

            set
            {
                products = value;
            }
        }

        

    }
}
