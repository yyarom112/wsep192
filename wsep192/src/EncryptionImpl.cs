using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class EncryptionImpl : Domain.Encryption
    {
        public bool connect()
        {
            return true;
        }


        public string encrypt(string password)
        {
            byte[] pwd;
            using (SHA512 shaM = new SHA512Managed())
            {
                pwd = Encoding.UTF8.GetBytes(password);
                pwd = shaM.ComputeHash(pwd);
            }
            String encryptPass = Encoding.UTF8.GetString(pwd);
            return encryptPass;
        }
    }
}
