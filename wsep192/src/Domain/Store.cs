using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace src.Domain
{
    class Store
    {
        private int id;
        private String name;
        private Dictionary<int, ProductInStore> products;
        private int storeRate;
        private TreeNode<Role> roles;
        private Dictionary<int, Role> rolesDictionary;
        private List<PurchasePolicy> purchasePolicy;
        private List<DiscountPolicy> discountPolicy;

        public Store(int id, string name, int storeRate, List<PurchasePolicy> purchasePolicy, List<DiscountPolicy> discountPolicy)
        {
            this.id = id;
            this.name = name;
            this.products = new Dictionary<int, ProductInStore>();
            this.storeRate = storeRate;
            this.roles = new TreeNode<Role>(null);
            this.rolesDictionary = new Dictionary<int, Role>();
            this.purchasePolicy = purchasePolicy;
            this.discountPolicy = discountPolicy;
        }

        public bool searchProduct(Filter filter,List<ProductInStore> listToAdd)
        {
            bool result = false;
            foreach(ProductInStore p in products.Values)
            {
                if (p.Product.compareProduct(filter)&&filter.StoreRate!=-1&&filter.StoreRate==this.storeRate)
                {
                    listToAdd.Add(p);
                    result = true;
                }
            }
            return result;
        }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int StoreRate { get => storeRate; set => storeRate = value; }
        internal Dictionary<int, ProductInStore> Products { get => products; set => products = value; }
        internal TreeNode<Role> Roles { get => roles; set => roles = value; }
        internal List<PurchasePolicy> PurchasePolicy { get => purchasePolicy; set => purchasePolicy = value; }
        internal List<DiscountPolicy> DiscountPolicy { get => discountPolicy; set => discountPolicy = value; }

        public Boolean assignManager(Role newManager, Owner owner)
        {
            TreeNode < Role > currOwner = roles.FindInChildren(owner);
            if(currOwner != null)
            {
                if (!rolesDictionary.ContainsKey(newManager.User.Id))
                {
                    currOwner.AddChild(newManager);
                    rolesDictionary.Add(newManager.User.Id, newManager);
                    newManager.User.Roles.Add(newManager.User.Id, newManager);
                    return true;
                }
            }
            return false;
        }
    }
}
