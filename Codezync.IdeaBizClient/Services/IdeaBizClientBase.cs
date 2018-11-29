using Codezync.IdeaBizClient.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codezync.IdeaBizClient
{
    public abstract class IdeaBizClientBase
    {
        protected string IdeaMartApiCallbase { get; }
        protected string IdeaMartTokenEndPoint { get; }
        protected string IdeaMartConsumerKey { get; }
        protected string IdeaMartConsumerSecret { get; }
        protected string IdeaMartUserName { get; }
        protected string IdeaMartPassword { get; }

        protected RestClient client;

        public IdeaMartAuthModel AuthModel;

        public IdeaBizClientBase()
        {
            IdeaMartApiCallbase = ConfigurationManager.AppSettings["IdeaMartApiCallbase"];
            IdeaMartTokenEndPoint = ConfigurationManager.AppSettings["IdeaMartTokenEndPoint"];
            IdeaMartConsumerKey = ConfigurationManager.AppSettings["IdeaMartConsumerKey"];
            IdeaMartConsumerSecret = ConfigurationManager.AppSettings["IdeaMartConsumerSecret"];
            IdeaMartUserName = ConfigurationManager.AppSettings["IdeaMartUserName"];
            IdeaMartPassword = ConfigurationManager.AppSettings["IdeaMartPassword"];

            client = new RestClient(IdeaMartApiCallbase);
            AuthModel = Authenticate();
        }


        protected IdeaMartAuthModel Authenticate()
        {
            try
            {
                var authCode = Utilities.Base64Encode($"{IdeaMartConsumerKey}:{IdeaMartConsumerSecret}");
                var request = new RestRequest(IdeaMartTokenEndPoint, Method.POST);
                request.AddHeader("Authorization", $"Basic {authCode}");
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddQueryParameter("grant_type", "password");
                request.AddQueryParameter("username", IdeaMartUserName);
                request.AddQueryParameter("password", IdeaMartPassword);
                request.AddQueryParameter("scope", "PRODUCTION");
                var response = client.Execute(request);
                var result = (IdeaMartAuthModel)(JsonConvert.DeserializeObject(response.Content, typeof(IdeaMartAuthModel)));
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
