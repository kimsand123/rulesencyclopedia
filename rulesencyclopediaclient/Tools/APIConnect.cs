using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using rulesencyclopediaclient.Pouch;

namespace rulesencyclopediaclient.Tools
{
    class APIConnect
    {
        string apiAddress = "localhost";
        string portNr = "44378";

        public WebClient client { get; set; }
        
        public APIConnect()
        {
            client = new WebClient();
            //her sætter vi addressen og porten for API servicen
            client.BaseAddress = "https://" + apiAddress + ":" + portNr + "/";
        }

        public WebClient initialiseClientForLogin()
        {
            client.Headers.Add(HttpRequestHeader.Accept, ("application/json"));
            client.Headers.Add(HttpRequestHeader.ContentType, ("application/json"));
            return client;
        }

        public WebClient initialiseClientForAcceptedAPIComs()
        {
            client.Headers.Add(HttpRequestHeader.Authorization, Pouch.Pouch.Instance.token);
            client.Headers.Add(HttpRequestHeader.Accept, ("application/json"));
            client.Headers.Add(HttpRequestHeader.ContentType, ("application/json"));
            return client;
        }


    }
}
