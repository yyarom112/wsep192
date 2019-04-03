using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Common;

namespace src.Domain
{
    class Store
    {
        private int id;
        private String name;
        private Dictionary<int, ProductInStore> products;
        private int storeRate;
        private ITree<Role> roles;
        private List<PurchasePolicy> purchasePolicy;
        private List<DiscountPolicy> discountPolicy;

        public Store(int id, string name, Dictionary<int, ProductInStore> products, int storeRate, ITree<Role> roles, List<PurchasePolicy> purchasePolicy, List<DiscountPolicy> discountPolicy)
        {
            this.id = id;
            this.name = name;
            this.products = products;
            this.storeRate = storeRate;
            this.roles = roles;
            this.purchasePolicy = purchasePolicy;
            this.discountPolicy = discountPolicy;
        }
        public void removeOwner(int userID)
        {
            Role role = searchOwner(userID);
            if (role != null)
            {
                role.User.Roles.Remove(id);
                roles.Remove(role);
            }
            
        }
        public Role searchOwner(int userID)
        {
            foreach (Role role in roles.Values)
                if (role.User.Id == userID)
                    return role;
            return null;
        }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int StoreRate { get => storeRate; set => storeRate = value; }
        internal Dictionary<int, ProductInStore> Products { get => products; set => products = value; }
        internal ITree<Role> Roles { get => roles; set => roles = value; }
        internal List<PurchasePolicy> PurchasePolicy { get => purchasePolicy; set => purchasePolicy = value; }
        internal List<DiscountPolicy> DiscountPolicy { get => discountPolicy; set => discountPolicy = value; }
    }
}
