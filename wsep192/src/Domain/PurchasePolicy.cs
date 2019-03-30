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
    }
}
