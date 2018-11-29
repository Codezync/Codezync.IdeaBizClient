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
    public class SmsClient : IdeaBizClientBase
    {

        private string IdeaMartSmsEndPoint;
        private string IdearMartSenderName;
        private string IdearMartSenderAddress;
        public SmsClient()
        {
            IdeaMartSmsEndPoint = ConfigurationManager.AppSettings["IdeaMartSmsEndPoint"];
            IdearMartSenderName = ConfigurationManager.AppSettings["IdearMartSenderName"];
            IdearMartSenderAddress = ConfigurationManager.AppSettings["IdearMartSenderAddress"];
        }
        public bool SendSms(string number, string message)
        {
            Authenticate();
            var smsSubscription = new SMSSubcriptionService();
            var IsSubscribed = smsSubscription.Subscribe(number);
            var flag = false;

            if (AuthModel != null)
            {
                var token = AuthModel.AccessToken;
                var request = new RestRequest(IdeaMartSmsEndPoint.Replace("{port}", "87711"), Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", $"Bearer {token}");

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
                    request.JsonSerializer = new CustomJsonSerializer();
                    request.AddJsonBody(smsRequest);
                    var response = client.Execute<SmsResponseModel>(request);
                    var test = response.Data.Fault;
                    if (response.Data.Fault == null)
                    {
                        flag = true;
                    }


                }
                catch (System.Exception)
                {
                    throw;
                    flag = false;
                }

            }
            return flag;
        }

    }
}
