using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace rulesencyclopediabackend.Tools
{
    //altered from https://stackoverflow.com/questions/2138429/hash-and-salt-passwords-in-c-sharp?fbclid=IwAR032vRjI6-lGkUq3YpP0wAVTVhNkyXQKLtrM5U0e3Cb6y9v9373rPNFeBI 
    //amidele alebge
    public class HashAndSalt
    {
        public string getSalt()
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[16];
            rng.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }


        public string GenerateHash(string password, string salt, int counter=0)
        {
            if (counter == 43)
            {
                return password;
            }

            byte[] bytes = Encoding.UTF8.GetBytes(password + salt);
            SHA256Managed sHA256ManagedString = new SHA256Managed();
            byte[] hash = sHA256ManagedString.ComputeHash(bytes);
            return GenerateHash(Convert.ToBase64String(hash), salt, counter + 1);
        }

        public bool AreEqual(string recievedPassword, string dbPassword, string salt)
        {
            string hashedRecievedPassword = GenerateHash(recievedPassword, salt, 0);


            return areStringsEqual(hashedRecievedPassword, dbPassword);
        }

        public bool areStringsEqual(string hashedRecievedPassword, string dbPassword)
        {
            if (hashedRecievedPassword.Length != dbPassword.Length)
            {
                return false;
            } else
            {
                int length = hashedRecievedPassword.Length;
                for (int x = 0; x < length; x++)
                {
                    if (hashedRecievedPassword[x]!=(dbPassword[x]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}