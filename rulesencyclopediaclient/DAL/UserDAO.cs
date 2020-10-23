using rulesencyclopediaclient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace rulesencyclopediaclient.DAL
{
    class UserDAO
    {
        public HttpWebResponse createUser(UserDTO user)
        {

        }

        public async Task loginUser(String userName, String Password)
        {
            string url = "/api/User/{userName}{Password}";
        }

        public HttpWebResponse getUser(String token)
        {

        }

    }
}
