using System;
using System.Security.Cryptography;
using System.Text;

namespace src.Domain
{
    class EncryptionImpl : Encryption
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
