using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace src.Domain
{
    class Store
    {
        private int id;
        private String name;
        private Dictionary<int, ProductInStore> products;
        private int storeRate;
        private Tree<Role> roles;
        private List<PurchasePolicy> purchasePolicy;
        private List<DiscountPolicy> discountPolicy;

    }
}
