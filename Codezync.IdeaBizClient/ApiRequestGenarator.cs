﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Configuration;
using Newtonsoft.Json;

namespace IdeaBizSms
{
    class ApiRequestGenarator
    {
        protected RestClient client;
        protected string IdeaMartApiCallbase { get; }
        public ApiRequestGenarator()
        {

            IdeaMartApiCallbase = ConfigurationManager.AppSettings["IdeaMartApiCallbase"];
            client = new RestClient(IdeaMartApiCallbase);

        }

        public R RequestApi<T, R>(T Params, Dictionary<string, string> Headers, Dictionary<string, string> QueryParametes, RestSharp.Method method, string url) where T : new()
        {

            var request = new RestRequest(url, method);

            foreach (KeyValuePair<string, string> entry in Headers)
            {
                request.AddHeader(entry.Key, entry.Value);
            }
            if (QueryParametes != null)
            {
                foreach (KeyValuePair<string, string> param in QueryParametes)
                {
                    request.AddQueryParameter(param.Key, param.Value);
                }
            }
            request.JsonSerializer = new CustomJsonSerializer();
            request.AddJsonBody(Params);
            var response = client.Execute(request);

            var result = (R)(JsonConvert.DeserializeObject(response.Content, typeof(R)));
            return result;


        }
    }
}