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
// fully lazy instantiation. Den bliver udløst første gang der er en reference til det statiske medlem af den 
// indeholdte klasse i Instance, og den bliver kun udført en gang pr Appdomæne hvilket gør at det kun er en tråd
// ad gangen der kan køre den. Disse to ting gør den lazy og threadsafe.
namespace rulesencyclopediabackend.Tools
{
    public sealed class CheckToken
    {
        List<TokenDTO> tokenList = new List<TokenDTO>();
        private CheckToken()
        {

        }
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
        
        public TokenDTO userLogin (UserDTO user)
        {
            //Could have additional checks before token creation.
            return createToken(user);
        }

        private void addToken(TokenDTO token)
        {
            //Could have additional checks before adding token.
            tokenList.Add(token);
        }

        private void removeToken(TokenDTO token)
        {
            //Could have additional checks before removing token.
            tokenList.Remove(token);
        }

        private bool checkForToken(string token)
        {
            //Using Linq to check if token is in the list
            TokenDTO tokenFound = tokenList.FirstOrDefault(tok => tok.token.Contains(token));
            if (tokenFound != null)
            {
                if (checkTokenTime(tokenFound))
                {
                    return true;
                }
            }
            return false;
        }
        private TokenDTO createToken(UserDTO user)
        {
            //Inspired from  https://stackoverflow.com/questions/14643735/how-to-generate-a-unique-token-which-expires-after-24-hours
            TokenDTO token = new TokenDTO();

            byte[] key = Guid.NewGuid().ToByteArray();
            byte[] userData = Encoding.ASCII.GetBytes(user.FirstName + user.MiddleName + user.LastName);
            string tok = Convert.ToBase64String(userData.Concat(key).ToArray());
            token.token = tok;
            token.timestamp = DateTime.Now;
            addToken(token);
            return token;
        }

        private Boolean checkTokenTime(TokenDTO token)
        {
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
            removeToken(token);
            token.timestamp = DateTime.Now;
            addToken(token);
        }
    }
}