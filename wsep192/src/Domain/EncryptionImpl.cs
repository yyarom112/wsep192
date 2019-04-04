using System;
using System.Security.Cryptography;

namespace src.Domain
{
    class EncryptionImpl : Encryption
    {
        public String encrypt(String password)
        {
            byte[] pwd;
            using (SHA512 shaM = new SHA512Managed())
            {
                pwd = System.Text.Encoding.UTF8.GetBytes(password);
                pwd = shaM.ComputeHash(pwd);
            }
            String encryptPass = System.Text.Encoding.UTF8.GetString(pwd);
            return encryptPass;
        }
    }
}


