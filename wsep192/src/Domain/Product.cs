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


        public int getId()
        {
            return this.id;
        }
    }
}
