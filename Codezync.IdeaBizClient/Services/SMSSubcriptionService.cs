using Codezync.IdeaBizClient.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codezync.IdeaBizClient
{
    public class SMSSubcriptionService : IdeaBizClientBase
    {


        private const string SUBSCRIPTION_METHOD = "WEB";
        protected string IdeaMartApiSmsSubscription { get; }
        protected string IdeaMartApiSmsUnSubscription { get; }

        public SMSSubcriptionService()
        {
            IdeaMartApiSmsSubscription = ConfigurationManager.AppSettings["IdeaMartApiSmsSubscription"];
            IdeaMartApiSmsUnSubscription = ConfigurationManager.AppSettings["IdeaMartApiSmsUnSubscription"];
            Authenticate();
        }

        public bool Subscribe(string number, string subscriptionMethod = SUBSCRIPTION_METHOD, string serviceID = null)
        {
            var flag = false;
            var request = new RestRequest(IdeaMartApiSmsSubscription, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {AuthModel.AccessToken}");
            request.AddHeader("Accept", "application/json");

            var subscriptionRequest = new SubscriptionRequestModel
            {
                Method = subscriptionMethod,
                ServiceID = serviceID,
                Msisdn = "tel:+" + number
            };

            request.JsonSerializer = new CustomJsonSerializer();
            request.AddJsonBody(subscriptionRequest);
            var response = client.Execute<SubscriptionResponseModel>(request);

            if (response.Data.StatusCode == "SUCCESS")
            {
                flag = true;
            }

            return flag;
        }

        public bool UnSubscribe(string number, string subscriptionMethod = SUBSCRIPTION_METHOD, string serviceID = null)
        {
            var flag = false;
            var request = new RestRequest(IdeaMartApiSmsUnSubscription, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {AuthModel.AccessToken}");
            request.AddHeader("Accept", "application/json");

            var subscriptionRequest = new SubscriptionRequestModel
            {
                Method = subscriptionMethod,
                ServiceID = serviceID,
                Msisdn = "tel:+" + number
            };

            request.JsonSerializer = new CustomJsonSerializer();
            request.AddJsonBody(subscriptionRequest);
            //var response = client.Execute(request);
            var response = client.Execute<SubscriptionResponseModel>(request);

            if (response.Data.StatusCode == "SUCCESS")
            {
                flag = true;
            }


            return flag;
        }
    }
}
