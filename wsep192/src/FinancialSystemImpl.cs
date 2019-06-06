using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src;
using src.Domain;

namespace src
{
    class FinancialSystemImpl : Domain.FinancialSystem
    {
        ExternalAPIImpl external = new ExternalAPIImpl();
        public bool connect()
        {
            return external.connect();
        }

        public bool payment(long cardNumber, DateTime date, double amount, int paymentTarget)
        {
            if (external.pay(cardNumber + "", date.Month + "", date.Year + "", "default", "123", "123456789") != -1)    //FOR NOW   
                return true;
            return false;
        }

        public bool Chargeback(long cardNumber, DateTime date, double amount)
        {
            return external.cancel_pay("12345");    //FOR NOW
        }
    }
}
