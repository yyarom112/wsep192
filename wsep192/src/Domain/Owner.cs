using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class Owner : Role
    {
        public Owner(Store store, User user) : base(store, user) { }
 
        public virtual Boolean assignManager(User managerUser, List<int> permissionToManager)
        {
            Manager newManager = new Manager(null, managerUser, permissionToManager);
            return Store.assignManager(newManager, this);
        }

        public virtual bool removeOwner(int userID) => Store.removeOwner(userID, this);
        public virtual bool removeManager(int userID) => Store.removeManager(userID, this);
        public virtual bool assignOwner(User assignedUser) => Store.assignOwner(assignedUser, this);
    }
}
