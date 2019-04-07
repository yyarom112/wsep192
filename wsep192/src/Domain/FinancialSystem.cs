using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    interface FinancialSystem
    {
        bool Connect();
        bool Payment(long cardNumber, DateTime date, int sum);

    }
}
