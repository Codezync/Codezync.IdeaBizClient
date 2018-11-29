using Codezync.IdeaBizClient.Models;
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
    public class SmsClient : IdeaBizClientBase
    {

        private string IdeaMartSmsEndPoint;
        private string IdearMartSenderName;
        private string IdearMartSenderAddress;
        private ApiRequestGenarator request;
        public SmsClient()
        {
            IdeaMartSmsEndPoint = ConfigurationManager.AppSettings["IdeaMartSmsEndPoint"];
            IdearMartSenderName = ConfigurationManager.AppSettings["IdearMartSenderName"];
            IdearMartSenderAddress = ConfigurationManager.AppSettings["IdearMartSenderAddress"];
            request = new ApiRequestGenarator(IdeaMartApiCallbase);
        }
        public bool SendSms(string number, string message)
        {
            Authenticate();

            var smsSubscription = new SMSSubcriptionService();
            var IsSubscribed = smsSubscription.Subscribe(number);
            var flag = false;

            if (AuthModel != null)
            {
                try
                {
                    var smsRequest = new SmsRequestModel
                    {
                        OutboundSMSMessageRequest =
                    {
                        SenderName = IdearMartSenderName,
                        SenderAddress = IdearMartSenderAddress,
                        ClientCorrelator = "123456",
                        Address = new List<string> {"tel:+" + number},
                        OutboundSMSTextMessage = {Message = message},
                        ReceiptRequest =
                        {
                            CallbackData = "",
                            NotifyURL = "http://128.199.174.220:1080/sms/report"
                        }
                    }
                    };

                    var response = request.RequestApi<SmsRequestModel, SmsResponseModel>(smsRequest, request.GetCommonHeaders(AuthModel.AccessToken), null, Method.POST, IdeaMartSmsEndPoint.Replace("{port}", "87711"));

                    var test = response.Fault;
                    if (response.Fault == null)
                    {
                        flag = true;
                    }


                }
                catch (System.Exception)
                {
                    throw;
                 
                }

            }
            return flag;
        }

    }
}
