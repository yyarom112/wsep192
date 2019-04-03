using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class Manager
    {
        private List<int> permissions;

        public Manager(List<int> permissions)
        {
            this.Permissions = permissions;
        }

        public List<int> Permissions { get { return permissions; } set { permissions = value; } }
    }
}
