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
            WinHttpHandler handler = new WinHttpHandler();
            var client = new HttpClient(handler);
            //HttpClient client = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (SettingsAndData.Instance.token != "")
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SettingsAndData.Instance.token);
            }
            return client;
        }

        public Uri getUri(string endpoint, string query = "")
        {
            UriBuilder uriBuilder = new UriBuilder("https://" + SettingsAndData.Instance.apiAddress + ":" + SettingsAndData.Instance.portNr + "/api/" + endpoint);
            //send values to api/login as parameters;

            if (query != "")
            {
                uriBuilder.Query = query;
            }
            return uriBuilder.Uri;
        }

        public HttpResponseMessage requestHandler(string httpRequestMethod, string apiPath, string parameters, string body)
        {
            Task<HttpResponseMessage> task=null;
            HttpClient client = getClient();
            HttpResponseMessage response = new HttpResponseMessage();
            Uri uri;
            response = null;
            if (httpRequestMethod == "GET")
            {
                if (parameters != "")
                {
                    uri = getUri(apiPath, parameters);
                }
                else
                {
                    uri = getUri(apiPath);
                }
                if (body != "")
                {
                    task = getResponseAsync(client, uri, body);
                    response = task.Result;
                } else
                {
                    task = getResponseAsync(client, uri);
                    response = task.Result;
                }


            }

            if (httpRequestMethod == "POST")
            {
                if (body != "")
                {

                }

            }

            if (httpRequestMethod == "PUT")
            {
                if (body != "")
                {

                }

            }

            if (httpRequestMethod == "DELETE")
            {
                if (body != "")
                {

                }

            }


            return response;
        }

        private async Task<HttpResponseMessage> getResponseAsync(HttpClient client, Uri uri, string body="")
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = uri,
                Content = new StringContent(body),
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.SendAsync(request).ConfigureAwait(false);
            return response;
        }
    }   
}
