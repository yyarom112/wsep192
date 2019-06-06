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
        ExternalAPIImpl external = new ExternalAPIImpl();
        public bool connect()
        {
            return external.connect();
        }

        public bool deliverToCustomer(string address, string packageDetails)
        {
            return true;
        }
    }
}
