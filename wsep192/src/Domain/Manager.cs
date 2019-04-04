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

        public List<int> Permissions { get => permissions; set => permissions = value; }
    }
}
