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
        private int id;
        private String adress;
        private bool Isregister;
        private LogicalConnections act;


        public UserConditionPolicy(int id ,string adress, bool isregister , LogicalConnections act)
        {
            this.id = id;
            this.adress = adress;
            Isregister = isregister;
            this.act = act;
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
        public int getId()
        {
            return id;
        }
    }
}
