
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Configuration;
using Newtonsoft.Json;
using Codezync.IdeaBizClient;

namespace IdeaBizSms
{
    class ApiRequestGenarator
    {
        protected RestClient client;
  
        public ApiRequestGenarator(string ideaMartApiCallbase)
        {
             client = new RestClient(ideaMartApiCallbase);

        }

        public R RequestApi<T, R>(T requestBody, Dictionary<string, string> headers, Dictionary<string, string> queryParametes, RestSharp.Method method, string url) where T : new()
        {

            var request = new RestRequest(url, method);

            foreach (KeyValuePair<string, string> entry in headers)
            {
                request.AddHeader(entry.Key, entry.Value);
            }
            if (queryParametes != null)
            {
                foreach (KeyValuePair<string, string> param in queryParametes)
                {
                    request.AddQueryParameter(param.Key, param.Value);
                }
            }
            request.JsonSerializer = new CustomJsonSerializer();
            request.AddJsonBody(requestBody);
            var response = client.Execute(request);

            var result = (R)(JsonConvert.DeserializeObject(response.Content, typeof(R)));
            return result;


        }

        public Dictionary<string, string> GetCommonHeaders(string token) {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Content-Type", "application/json");
            headers.Add("Authorization", $"Bearer {token}");
            headers.Add("Accept", "application/json");
            return headers;
        }
    }
}
