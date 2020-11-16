using Newtonsoft.Json;
using rulesencyclopediaclient.Pouch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
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

        public HttpResponseMessage get(string apiPath, string parameters, string body) 
        {
            Task<HttpResponseMessage> task = null;
            HttpClient client = getClient();
            HttpResponseMessage response = new HttpResponseMessage();
            Uri uri;
            response = null;

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
                task = getResponseAsync("GET", client, uri, body);
                response = task.Result;
            } else
            {
                task = getResponseAsync("GET", client, uri);
                response = task.Result;
            }
            return response;
        }
        public HttpResponseMessage post(string apiPath, object payload)
        {
            Task<HttpResponseMessage> task = null;
            HttpClient client = getClient();
            HttpResponseMessage response = new HttpResponseMessage();
            Uri uri = getUri(apiPath);
            response = null;

            if (payload != null)
            {
                //TODO: Exception handling.
                task = getResponseAsync("POST", client, uri, "", payload);
            }
            return task.Result;
        }
        public HttpResponseMessage put(string httpRequestMethod, string apiPath, string parameters, string payload)
        {
            Task<HttpResponseMessage> task = null;
            HttpClient client = getClient();
            HttpResponseMessage response = new HttpResponseMessage();
            Uri uri;
            response = null;
            if (httpRequestMethod == "PUT")
            {
                if (payload != "")
                {

                }

            }
            return response;
        }
        public HttpResponseMessage delete(string httpRequestMethod, string apiPath, string parameters, string body)
        {
            Task<HttpResponseMessage> task = null;
            HttpClient client = getClient();
            HttpResponseMessage response = new HttpResponseMessage();
            Uri uri;
            response = null;
            if (httpRequestMethod == "DELETE")
            {
                if (body != "")
                {

                }

            }
            return response;
        }

        private async Task<HttpResponseMessage> getResponseAsync(string httpRequestMethod, HttpClient client, Uri uri, string body = "", object payload=null)
        {
            HttpRequestMessage request=null;
            if (httpRequestMethod == "GET")
            {
                request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = uri,
                };
                if (body != "")
                {
                    request.Content = new StringContent(body);
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                }

            }

            if (httpRequestMethod == "POST")
            {
                string payloadString = JsonConvert.SerializeObject(payload);
                request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = uri,
                    Content = new StringContent(payloadString),
                };
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            }

            var response = await client.SendAsync(request).ConfigureAwait(false);

            return response;
        }
    }   
}
