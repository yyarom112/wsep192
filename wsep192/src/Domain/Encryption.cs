using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    interface Encryption
    {
        bool connect();

        String encrypt(String password);
    }
}
