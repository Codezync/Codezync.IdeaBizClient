using Codezync.IdeaBizClient.Models;
using Codezync.IdeaBizClient.Properties;
using IdeaBizSms;
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

        private ApiRequestGenarator request;
        public SMSSubcriptionService()
        {
            IdeaMartApiSmsSubscription = ConfigurationManager.AppSettings["IdeaMartApiSmsSubscription"];
            IdeaMartApiSmsUnSubscription = ConfigurationManager.AppSettings["IdeaMartApiSmsUnSubscription"];
            request = new ApiRequestGenarator(IdeaMartApiCallbase);
            Authenticate();
        }

        public bool Subscribe(string number, string subscriptionMethod = SUBSCRIPTION_METHOD, string serviceID = null)
        {
            var flag = false;

            var subscriptionRequest = new SubscriptionRequestModel
            {
                Method = subscriptionMethod,
                ServiceID = serviceID,
                Msisdn = "tel:+" + number
            };

            var response = request.RequestApi<SubscriptionRequestModel, SubscriptionResponseModel>(subscriptionRequest, request.GetCommonHeaders(AuthModel.AccessToken), null, Method.POST, IdeaMartApiSmsSubscription);


            if (response.StatusCode == IdeaBizResource.SUCCESS)
            {
                flag = true;
            }

            return flag;
        }

        public bool UnSubscribe(string number, string subscriptionMethod = SUBSCRIPTION_METHOD, string serviceID = null)
        {
            var flag = false;

            var subscriptionRequest = new SubscriptionRequestModel
            {
                Method = subscriptionMethod,
                ServiceID = serviceID,
                Msisdn = "tel:+" + number
            };

            var response = request.RequestApi<SubscriptionRequestModel, SubscriptionResponseModel>(subscriptionRequest, request.GetCommonHeaders(AuthModel.AccessToken), null, Method.POST, IdeaMartApiSmsUnSubscription);


            if (response.StatusCode == IdeaBizResource.SUCCESS)
            {
                flag = true;
            }


            return flag;
        }
    }
}
