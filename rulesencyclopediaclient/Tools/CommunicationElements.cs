using rulesencyclopediaclient.Pouch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace rulesencyclopediaclient.Tools
{
    class CommunicationElements
    {
        public HttpClient getClient()
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (SettingsAndData.Instance.token != "")
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SettingsAndData.Instance.token);
            }
            return client;
        }

        public Uri getUri(string endpoint, string query="")
        {
            UriBuilder uriBuilder = new UriBuilder("https://" + SettingsAndData.Instance.apiAddress + ":" + SettingsAndData.Instance.portNr + "/api/" + endpoint);
            //send values to api/login as parameters;
            
            if (query!="")
            {
                uriBuilder.Query = query;
            }
            return uriBuilder.Uri;
        }
    }






}
