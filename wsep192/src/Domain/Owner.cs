using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class Owner : Role
    {
        public Owner(Store store, User user) : base(store, user)
        {
        }

        public bool removeOwner(int userID)
        {
            return Store.removeOwner(userID);
        }

        public virtual bool removeManager(int userID) => Store.removeOwner(userID);
    }
}
