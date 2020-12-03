using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace rulesencyclopediabackend.Tools
{
    //Inspired and altered from https://stackoverflow.com/questions/2138429/hash-and-salt-passwords-in-c-sharp?fbclid=IwAR032vRjI6-lGkUq3YpP0wAVTVhNkyXQKLtrM5U0e3Cb6y9v9373rPNFeBI 
    //amidele alebge
    public class HashAndSalt
    {
        public string getSalt()
        {
            //Generate a cryptographic random number for use as a salt.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[16];
            rng.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }


        public string GenerateHash(string password, string salt, int counter=0)
        {
            //When reached 43 recursions return the resultating password.
            if (counter == 43)
            {
                return password;
            }
            //Concatenates password and salt
            byte[] bytes = Encoding.UTF8.GetBytes(password + salt);
            SHA256Managed sHA256ManagedString = new SHA256Managed();
            //Computes the hash of the concatenated byte array using a 256 bit algorithm 
            byte[] hash = sHA256ManagedString.ComputeHash(bytes);
            //Converts the bytearray to a string and encodes it with base-64 digits. 
            //Recursively passes that result into itself as a password, alongside the salt and an increased counter.
            return GenerateHash(Convert.ToBase64String(hash), salt, counter + 1);
        }

        public bool AreEqual(string recievedPassword, string dbPassword, string salt)
        {
            //chech if the usertyped password is equal the the hashed and salted password in the DB
            string hashedRecievedPassword = GenerateHash(recievedPassword, salt, 0);
            return areStringsEqual(hashedRecievedPassword, dbPassword);
        }

        public bool areStringsEqual(string hashedRecievedPassword, string dbPassword)
        {
            //Check if the strings are of equal length and content.
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