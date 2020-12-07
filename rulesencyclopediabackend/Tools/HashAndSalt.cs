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
    //member   amidele alebge
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

        //Run this recursive method 43 time. The first time the password as typed by the user 
        //is used for the hashing and salting operation. The rest of the time the result of the
        //previous recursive action is used for the hashing and salting operation.
        //Its ok it takes a little bit of time... Just makes it that much harder to break.
        //when done return the resulting hashed and salted password.
        //This operation will produce the same encrypted password every time for the same
        //typed in password.
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
            //chech if the usertyped password is equal to the hashed and salted password in the DB
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
                //If the strings are of equal length, check if the hashed typed in password 
                //equals that of the db
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