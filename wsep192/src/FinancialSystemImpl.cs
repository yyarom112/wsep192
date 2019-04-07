using src.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    class FinancialSystemImpl : FinancialSystem
    {
        public bool Connect()
        {
            return true;
        }

        public bool Payment(long cardNumber, DateTime date, int sum)
        {
            return true ;
        }
    }
}
