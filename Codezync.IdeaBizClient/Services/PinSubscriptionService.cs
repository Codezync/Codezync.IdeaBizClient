using Codezync.IdeaBizClient.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codezync.IdeaBizClient.Services
{
    public class PinSubscription : IdeaBizClientBase
    {

        private const string SUBSCRIPTION_METHOD = "ANC";

        private string IdeaMartPinSubscriptionEndPoint { get; }
        private string IdeaMartPinValidateEndPoint { get; }

        public PinSubscription()
        {
            IdeaMartPinSubscriptionEndPoint = ConfigurationManager.AppSettings["IdeaMartPinSubscriptionEndPoint"];
            IdeaMartPinValidateEndPoint = ConfigurationManager.AppSettings["IdeaMartPinValidateEndPoint"];
            Authenticate();
        }


        public ActivityResult Subscribe(string number, string method = SUBSCRIPTION_METHOD)
        {

            var request = new RestRequest(IdeaMartPinSubscriptionEndPoint, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {AuthModel.AccessToken}");
            request.AddHeader("Accept", "application/json");

            var subscriptionRequest = new SubscriptionRequestModel
            {
                Method = method,
                Msisdn = number
            };

            request.JsonSerializer = new CustomJsonSerializer();
            request.AddJsonBody(subscriptionRequest);

            var res = new ActivityResult();

            //var response = client.Execute(request);
            var response = client.Execute<SubscriptionResponseModel>(request);

            if (response.Data.StatusCode == "SUCCESS")
            {
                res.Status = response.Data.StatusCode;
                res.ServerRef = response.Data.Data.ServerRef;
            }
            else
            {
                res.Status = response.Data.Message;
            }

            return res;

        }

        public ActivityResult ValidatePin(string pin, string serverRef)
        {
            var flag = false;
            var request = new RestRequest(IdeaMartPinValidateEndPoint, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {AuthModel.AccessToken}");
            request.AddHeader("Accept", "application/json");

            var pinVerification = new PinVerificationModel
            {
                Pin = pin,
                ServerRef = serverRef
            };
            request.JsonSerializer = new CustomJsonSerializer();
            request.AddJsonBody(pinVerification);

            var response = client.Execute<SubscriptionResponseModel>(request);

            var res = new ActivityResult();

            if (response.Data.StatusCode == "SUCCESS")
            {
                res.Status = response.Data.StatusCode;
                res.ServerRef = response.Data.Data.ServerRef;
            }
            else
            {
                res.Status = response.Data.Message;

            }
            return res;
        }
    }
}
