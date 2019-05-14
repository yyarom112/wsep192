using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Domain.Dataclass;

namespace src.Domain
{
    interface PurchasePolicy
    {
        bool CheckCondition(List<KeyValuePair<ProductInStore, int>> cart, UserDetailes user );
        int getId();
    }
}
