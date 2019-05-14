using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain.Dataclass
{
    class UserDetailes
    {
        private String adress;
        private bool isregister;

        public UserDetailes(string adress, bool isregister)
        {
            this.Adress = adress;
            this.isregister = isregister;
        }

        public string Adress { get => adress; set => adress = value; }
        public bool Isregister { get => isregister; set => isregister = value; }
    }
}
