using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class Role
    {
        private Store store;
        private User user;

        public Role(Store store, User user)
        {
            this.store = store;
            this.user = user;
        }

        internal Store Store { get { return store; } set { store = value; } }
        internal User User { get { return user; } set { user = value; } }
    }
}
