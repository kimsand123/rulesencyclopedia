using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace rulesencyclopediaclient.Interfaces
{
    interface ICommunicationElements
    {
        HttpResponseMessage checkConnection();
        HttpClient getClient();
        Uri getUri(string endpoint, string query = "");
        HttpResponseMessage get(string apiPath, string parameters, string body);
        HttpResponseMessage post(string apiPath, object payload);
        HttpResponseMessage delete(string apiPath, string parameters);
        HttpResponseMessage put(string apiPath, string parameters, object payload);
    }
}
