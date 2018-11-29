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
    public class BillPayment : IdeaBizClientBase
    {

        private string IdeaMartPaymentEndPoint { get; }
        private string IdeaMartPinSubmitEndPoint { get; }
        private ApiRequestGenarator request;
        public BillPayment()
        {
            IdeaMartPaymentEndPoint = ConfigurationManager.AppSettings["IdeaMartPaymentEndPoint"];
            IdeaMartPinSubmitEndPoint = ConfigurationManager.AppSettings["IdeaMartPinSubmitEndPoint"];
            request = new ApiRequestGenarator(IdeaMartApiCallbase);
        }
        public PaymentResponseModel Pay(string phoneNumber, double amount, string requestId)
        {
            try
            {

                var paymentObj = new PaymentRequest
                {
                    PhoneNumber = phoneNumber,
                    Amount = amount,
                    Taxable = true,
                    CallBackUrl = null,
                    Description = ConfigurationManager.AppSettings["IdeaMartPaymentRef"],
                    Transactionreference = requestId
                };
                var response = request.RequestApi<PaymentRequest, PaymentResponseModel>(paymentObj, request.GetCommonHeaders(AuthModel.AccessToken), null, Method.POST, IdeaMartPaymentEndPoint);

                return response;


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public PaymentResponseModel SubmitPin(string serverRef, string pin)
        {
            try
            {
                if (AuthModel != null)
                {

                    var pinObj = new PinVerificationModel
                    {
                        Pin = pin,
                        ServerRef = serverRef
                    };

                    var response = request.RequestApi<PinVerificationModel, PaymentResponseModel>(pinObj, request.GetCommonHeaders(AuthModel.AccessToken), null, Method.POST, IdeaMartPinSubmitEndPoint);

                    return response;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
