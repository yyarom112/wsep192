using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    class FinancialSystemImpl : Domain.FinancialSystem
    {
        public bool connect()
        {
            return true;
        }

        public bool payment(long cardNumber, DateTime date, double amount, int paymentTarget)
        {
            return true;
        }

        public bool Chargeback(long cardNumber, DateTime date, double amount)
        {
            return true;
        }
    }
}
