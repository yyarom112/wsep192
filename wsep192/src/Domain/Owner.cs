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

        public Role assignManager(User managerUser, List<int> permissionToManager)
        {
            Manager newManager = new Manager(null, managerUser, permissionToManager);
            return newManager;
        }
    }
}
