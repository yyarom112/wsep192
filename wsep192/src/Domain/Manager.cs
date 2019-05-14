using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class Manager : Role
    {
        private List<int> permissions;

        public Manager(Store store,User user,List<int> permissions) : base(store,user)
        {
            this.Permissions = permissions;
        }

        private List<int> Permissions { get => permissions; set => permissions = value; }

        public virtual bool validatePermission(int permission)
        {
            if (this.Permissions.Contains(permission))
            {
                LogManager.Instance.WriteToLog("Manager:validatePermission success - user" + User.UserName + "\n");
                return true;
            }
            LogManager.Instance.WriteToLog("Manager:validatePermission failed - user" + User.UserName + "\n");
            return false;
        }
    }
}
