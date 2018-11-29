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

namespace Codezync.IdeaBizClient.Services
{
    public class PinSubscription : IdeaBizClientBase
    {

        private const string SUBSCRIPTION_METHOD = "ANC";

        private string IdeaMartPinSubscriptionEndPoint { get; }
        private string IdeaMartPinValidateEndPoint { get; }

        private ApiRequestGenarator request;

        public PinSubscription()
        {
            IdeaMartPinSubscriptionEndPoint = ConfigurationManager.AppSettings["IdeaMartPinSubscriptionEndPoint"];
            IdeaMartPinValidateEndPoint = ConfigurationManager.AppSettings["IdeaMartPinValidateEndPoint"];
            request = new ApiRequestGenarator(IdeaMartApiCallbase);
            Authenticate();
        }


        public ActivityResult Subscribe(string number, string method = SUBSCRIPTION_METHOD)
        {

            var subscriptionRequest = new SubscriptionRequestModel
            {
                Method = method,
                Msisdn = number
            };

            var response = request.RequestApi<SubscriptionRequestModel, SubscriptionResponseModel>(subscriptionRequest, request.GetCommonHeaders(AuthModel.AccessToken), null, Method.POST, IdeaMartPinSubscriptionEndPoint);

            var res = new ActivityResult();

            if (response.StatusCode == IdeaBizResource.SUCCESS)
            {
                res.Status = response.StatusCode;
                res.ServerRef = response.Data.ServerRef;
            }
            else
            {
                res.Status = response.Message;
            }
            return res;

        }

        public ActivityResult ValidatePin(string pin, string serverRef)
        {

            var pinVerification = new PinVerificationModel
            {
                Pin = pin,
                ServerRef = serverRef
            };

            var response = request.RequestApi<PinVerificationModel, SubscriptionResponseModel>(pinVerification, request.GetCommonHeaders(AuthModel.AccessToken), null, Method.POST, IdeaMartPinValidateEndPoint);

            var res = new ActivityResult();

            if (response.StatusCode == IdeaBizResource.SUCCESS)
            {
                res.Status = response.StatusCode;
                res.ServerRef = response.Data.ServerRef;
            }
            else
            {
                res.Status = response.Message;
            }
            return res;
        }
    }
}
