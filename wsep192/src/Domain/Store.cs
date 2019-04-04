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

        public Store(int id, string name, int storeRate, User owner  )
        {
            this.id = id;
            this.name = name;
            this.products = new Dictionary<int, ProductInStore>();
            this.storeRate = storeRate;
            this.roles =  NodeTree<Role>.NewTree();
            this.purchasePolicy = new List < PurchasePolicy >();
            this.discountPolicy = new List<DiscountPolicy>();
        }
        public Store()
        {
        }

        public int Id { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public int StoreRate { get { return storeRate; } set { storeRate = value; } }
        internal Dictionary<int, ProductInStore> Products { get { return products; } set { products = value; } }
        internal ITree<Role> Roles { get { return roles; } set { roles = value; } }
        internal List<PurchasePolicy> PurchasePolicy { get { return purchasePolicy; } set { purchasePolicy = value; } }
        internal List<DiscountPolicy> DiscountPolicy { get { return discountPolicy; } set { discountPolicy = value; } }
    }
}
