using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class PurchesPolicyData
    {
        protected int type;
        protected int id;
        protected int productID;
        protected int quntity;
        protected int min;
        protected int max;
        protected int sumMin;
        protected int sumMax;
        protected LogicalConnections act;
        protected String adress;
        protected bool Isregister;
    }
}
