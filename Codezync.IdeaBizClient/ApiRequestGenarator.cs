
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
        /**
         * generic method to send request to server
         * parameters:generic type request body,dictionaries of headers and query parameters,request method,endpoint
         * **/
        public R RequestApi<T, R>(T requestBody, Dictionary<string, string> headers, Dictionary<string, string> queryParametes, RestSharp.Method method, string url) where T : new()
        {
            //genarate request
            var request = new RestRequest(url, method);
            //add headers
            foreach (KeyValuePair<string, string> entry in headers)
            {
                request.AddHeader(entry.Key, entry.Value);
            }
            //add query parameters
            if (queryParametes != null)
            {
                foreach (KeyValuePair<string, string> param in queryParametes)
                {
                    request.AddQueryParameter(param.Key, param.Value);
                }
            }
            //add requestbody
            request.JsonSerializer = new CustomJsonSerializer();
            request.AddJsonBody(requestBody);
            //excute request
            var response = client.Execute(request);
            //deserialize result
            var result = (R)(JsonConvert.DeserializeObject(response.Content, typeof(R)));
            return result;


        }
        /**
         *method to add common headers 
         * parameters:AccessToken
         **/
        public Dictionary<string, string> GetCommonHeaders(string token) {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Content-Type", "application/json");
            headers.Add("Authorization", $"Bearer {token}");
            headers.Add("Accept", "application/json");
            return headers;
        }
    }
}
