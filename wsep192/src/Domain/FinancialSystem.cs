using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    interface FinancialSystem
    {
        bool connect();
        bool payment(long cardNumber, DateTime date, double amount , int paymentTarget);
        bool Chargeback(long cardNumber, DateTime date, double amount);
    }
}
