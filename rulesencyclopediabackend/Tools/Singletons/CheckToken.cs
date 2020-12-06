using rulesencyclopediabackend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
// from https://csharpindepth.com/articles/singleton

// Fully lazy instantiation. It is triggered first time there is a reference to the static member of the containing
// Class in Instance, and it will only be executed once per app domain, which does so that only 1 thread at a time 
// can run it. These two things makes it lazy and threadsafe

// It is made a singleton, because it has to use the list of tokens all over the application.

namespace rulesencyclopediabackend.Tools
{
    public sealed class CheckToken
    {
        List<TokenDTO> tokenList = new List<TokenDTO>();
        private CheckToken()
        {

        }
        // Here the class returns itself
        public static CheckToken Instance {  get { return check.instance; } }
        private class check { 
            static check()
            {

            }
            internal static readonly CheckToken instance = new CheckToken();
        }
        public bool doCheckToken(string token)
        {
            //Check if token is in list. If not put it in list and return true
            //If token is in list, check time on token. If too old return false and remove token
            //else return true and refresh token

            return checkForToken(token);
        }
        private bool checkForToken(string token)
        {
            //Using Linq to check if token is in the list
            TokenDTO tokenFound = tokenList.FirstOrDefault(tok => tok.token.Contains(token));
            if (tokenFound != null)
            {
                // if the token is not not too old
                if (checkTokenTime(tokenFound))
                {
                    return true;
                }
            }
            return false;
        }

        public TokenDTO userLogin (UserDTO user)
        {
            //Could have additional businesslogic before token creation.
            return createToken(user);
        }

        private void addToken(TokenDTO token)
        {
            //Could have additional businesslogic before adding token.
            tokenList.Add(token);
        }

        private void removeToken(TokenDTO token)
        {
            //Could have additional businesslogic before removing token.
            tokenList.Remove(token);
        }

        private TokenDTO createToken(UserDTO user)
        {
            //Inspiration from  https://stackoverflow.com/questions/14643735/how-to-generate-a-unique-token-which-expires-after-24-hours
            TokenDTO token = new TokenDTO();

            //Getting a random key
            byte[] key = Guid.NewGuid().ToByteArray();
            //Make FirstName, MiddleName, LastName into a byte array
            byte[] userData = Encoding.ASCII.GetBytes(user.FirstName + user.MiddleName + user.LastName);
            //Use the bytearray and the key to make the token 
            string tok = Convert.ToBase64String(userData.Concat(key).ToArray());
            token.token = tok;
            //Make the timetamp
            token.timestamp = DateTime.Now;
            //Add the token to the token list
            addToken(token);
            return token;
        }

        private Boolean checkTokenTime(TokenDTO token)
        {
            //If the token is more than 8 hours old
            if (token.timestamp<DateTime.UtcNow.AddHours(-8))
            {
                removeToken(token);
                return false;
            }
            refreshToken(token);
            return true;
        }

        private void refreshToken(TokenDTO token)
        {
            //To refresh the token i remove it, set a new timestamp for the token,
            //and add the token to the token list again.
            removeToken(token);
            token.timestamp = DateTime.Now;
            addToken(token);
        }
    }
}