using src.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    class ProductSupplySystemImpl : ProductSupplySystem
    {
        public bool connect()
        {
            return true;
        }

        public bool deliverToCustomer(string address, string packageDetails)
        {
            return true;
        }
    }
}
