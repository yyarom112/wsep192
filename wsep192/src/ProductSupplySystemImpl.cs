﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    class ProductSupplySystemImpl : Domain.ProductSupplySystem
    {
        public bool connect()
        {
            return true;
        }

        public bool deliverToCustomer()
        {
            return true;
        }
    }
}
