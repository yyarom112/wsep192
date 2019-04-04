using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class TradingSystem
    {
        private Dictionary<int,User> users;
        private Dictionary<int,Store> stores;
        private ProductSupplySystem supplySystem;
        private FinancialSystem financialSystem;
        private int productCounter;
        private int storeCounter;
        private int userCounter;
        private int purchasePolicyCounter;
        private int discountPolicyCounter;

        public TradingSystem(ProductSupplySystem supplySystem, FinancialSystem financialSystem)
        {
            this.users = new Dictionary<int, User>();
            this.stores = new Dictionary<int, Store>();
            this.supplySystem = supplySystem;
            this.financialSystem = financialSystem;
            this.productCounter = 0;
            this.storeCounter = 0;
            this.userCounter = 0;
            this.purchasePolicyCounter = 0;
            this.discountPolicyCounter =  0;
        }

        public int ProductCounter { get => productCounter; set => productCounter = value; }
        public int StoreCounter { get => storeCounter; set => storeCounter = value; }
        public int UserCounter { get => userCounter; set => userCounter = value; }
        public int PurchasePolicyCounter { get => purchasePolicyCounter; set => purchasePolicyCounter = value; }
        public int DiscountPolicyCounter { get => discountPolicyCounter; set => discountPolicyCounter = value; }
        internal Dictionary<int, User> Users { get => users; set => users = value; }
        internal Dictionary<int, Store> Stores { get => stores; set => stores = value; }
        internal ProductSupplySystem SupplySystem { get => supplySystem; set => supplySystem = value; }
        internal FinancialSystem FinancialSystem { get => financialSystem; set => financialSystem = value; }

        public Boolean assignManager(int ownerId, int managerId, int storeId, List<int> permissionToManager)
        {
            if (this.users.ContainsKey(ownerId) && this.users.ContainsKey(managerId))
            {
                User ownerUser = this.users[ownerId];
                Owner owner = (Owner)ownerUser.Roles[ownerId];
                User managerUser = this.users[managerId];
                Role newManager = ownerUser.assignManager(managerUser, storeId, permissionToManager);
                if (newManager != null)
                {
                    if (this.stores.ContainsKey(storeId)){
                        Store currStore = this.stores[storeId];
                        if (currStore != null)
                        {
                            return currStore.assignManager(newManager,owner);
                        }
                    }
                    else
                        return false;
                }      
            }
            return false;
        }
    }
}
