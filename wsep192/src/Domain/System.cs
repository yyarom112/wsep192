using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class System
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

        public System(Dictionary<int, User> users, Dictionary<int, Store> stores, ProductSupplySystem supplySystem, FinancialSystem financialSystem, int productCounter, int storeCounter, int userCounter, int purchasePolicyCounter, int discountPolicyCounter)
        {
            this.users = users;
            this.stores = stores;
            this.supplySystem = supplySystem;
            this.financialSystem = financialSystem;
            this.productCounter = productCounter;
            this.storeCounter = storeCounter;
            this.userCounter = userCounter;
            this.purchasePolicyCounter = purchasePolicyCounter;
            this.discountPolicyCounter = discountPolicyCounter;
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
    }
}
