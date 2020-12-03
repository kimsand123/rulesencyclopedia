using rulesencyclopediaclient.Model;
using rulesencyclopediaclient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace rulesencyclopediaclient.DAL
{
    class UserDAO
    {
        static HttpClient client = new HttpClient();
        static async Task<HttpResponseMessage> CreateUserAsync(UserDTO user)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
               "api/User", user);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task loginUser(String userName, String Password)
        {
            string url = "/api/Login/{userName}{Password}";


        }

        public HttpWebResponse GetUser(String token)
        {
            HttpWebResponse response = null;

            return response;
        }

    }
}
