using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Domain.Dataclass;

namespace src.Domain
{
    class UserConditionPolicy : PurchasePolicy
    {
        private String adress;
        private bool Isregister;

        public UserConditionPolicy(string adress, bool isregister)
        {
            this.adress = adress;
            Isregister = isregister;
        }

        public bool CheckCondition(List<KeyValuePair<ProductInStore, int>> cart, UserDetailes user)
        {
            if (adress != null)
                if (user.Adress != adress)
                    return false;
            if (Isregister == true && user.Isregister != true)
                return false;
            return true;
        }
    }
}
