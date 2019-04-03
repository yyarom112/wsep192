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
        public List<ProductInStore> searchProduct(String name,int userID ,int storeID)
        {
            User user = searchUser(userID);
            Store store = searchStore(storeID);
            //if(store&&user&&user.searchProduct) TODO:Continue
            
            

        }
        public User searchUser(int userID)
        {
            foreach (User u in users.Values)
                if (u.getID() == userID)
                    return u;
            return null;
        }
        public Store searchStore(int storeID)
        {
            foreach (Store s in stores.Values)
                if (s.getStoreID() == storeID)
                    return s;
            return null;
        }
    }
}
