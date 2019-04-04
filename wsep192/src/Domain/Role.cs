using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class Role
    {
        protected Store store;
        protected User user;

        public Role(Store store, User user)
        {
            this.store = store;
            this.user = user;
        }

        internal Store Store { get => store; set => store = value; }
        internal User User { get => user; set => user = value; }
    }
}
