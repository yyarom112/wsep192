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

        public System(ProductSupplySystem supplySystem, FinancialSystem financialSystem)
        {
            this.users = new Dictionary<int, User>();
            this.stores = new Dictionary<int, Store>();
            this.supplySystem = supplySystem;
            this.financialSystem = financialSystem;
            this.productCounter = 1;
            this.storeCounter = 1;
            this.userCounter = 1;
            this.purchasePolicyCounter = 1;
            this.discountPolicyCounter = 1;
        }
        public List<ProductInStore> searchProduct(String details)
        {
            List<ProductInStore> products  = new List<ProductInStore>();
            String[] detailsForFilter = details.Split(' ');
            KeyValuePair<int, int> priceRange = new KeyValuePair<int, int>(Int32.Parse(detailsForFilter[3]),
                Int32.Parse(detailsForFilter[4]));
            Filter filter = new Filter(detailsForFilter[0],
                detailsForFilter[1], detailsForFilter[2], priceRange,
                Int32.Parse(detailsForFilter[5]), Int32.Parse(detailsForFilter[6]));
            foreach (Store s in stores.Values)
            {
                s.searchProduct(filter,products);
            }
            return products;
        }
        public User searchUser(int userID)
        {
            foreach (User u in users.Values)
                if (u.Id == userID)
                    return u;
            return null;
        }
        public Store searchStore(int storeID)
        {
            foreach (Store s in stores.Values)
                if (s.Id == storeID)
                    return s;
            return null;
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
