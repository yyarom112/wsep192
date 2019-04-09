using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class FinancialSystemImpl : FinancialSystem
    {
        public bool connect()
        {
            return true;
        }

        public bool payment(long cardNumber, DateTime date, int sum)
        {
            return true;
        }
    }
}
