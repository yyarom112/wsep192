﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    interface ProductSupplySystem
    {
        bool connect();
        bool deliverToCustomer(String address, String packageDetails);
    }
}
