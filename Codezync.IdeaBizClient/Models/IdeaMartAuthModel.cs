using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codezync.IdeaBizClient.Models
{
    public class IdeaMartAuthModel
    {
        [JsonProperty("scope")]
        public string Scope { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }


    public class OutboundSMSTextMessage
    {

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class ReceiptRequest
    {

        [JsonProperty("notifyURL")]
        public string NotifyURL { get; set; }

        [JsonProperty("callbackData")]
        public string CallbackData { get; set; }
    }

    public class OutboundSMSMessageRequest
    {

        public OutboundSMSMessageRequest()
        {
            OutboundSMSTextMessage = new OutboundSMSTextMessage();
            ReceiptRequest = new ReceiptRequest();
        }


        [JsonProperty("address")]
        public IList<string> Address { get; set; }

        [JsonProperty("senderAddress")]
        public string SenderAddress { get; set; }

        [JsonProperty("outboundSMSTextMessage")]
        public OutboundSMSTextMessage OutboundSMSTextMessage { get; set; }

        [JsonProperty("clientCorrelator")]
        public string ClientCorrelator { get; set; }

        [JsonProperty("receiptRequest")]
        public ReceiptRequest ReceiptRequest { get; set; }

        [JsonProperty("senderName")]
        public string SenderName { get; set; }
    }

    public class SmsRequestModel
    {
        public SmsRequestModel()
        {
            OutboundSMSMessageRequest = new OutboundSMSMessageRequest();
        }

        [JsonProperty("outboundSMSMessageRequest")]
        public OutboundSMSMessageRequest OutboundSMSMessageRequest { get; set; }
    }
    public class SmsResponseModel
    {
        [JsonProperty("fault")]
        public SmsResponseFaultModel Fault { get; set; }
    }

    public class SmsResponseFaultModel
    {

        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class ResponseBaseModel
    {

        [JsonProperty("statusCode")]
        public string StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
    public class ResponseDataBaseModel
    {
        [JsonProperty("serverRef")]
        public string ServerRef { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("msisdn")]
        public string PhoneNumber { get; set; }
    }



    public class SubscriptionRequestModel
    {

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("serviceID")]
        public string ServiceID { get; set; }

        [JsonProperty("msisdn")]
        public string Msisdn { get; set; }
    }

    public class SubscriptionModel : ResponseDataBaseModel
    {

        [JsonProperty("serviceID")]
        public string ServiceID { get; set; }


    }

    public class SubscriptionResponseModel
    {
        [JsonProperty("statusCode")]
        public string StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public SubscriptionModel Data { get; set; }
    }

    public class PinVerificationModel
    {
        [JsonProperty("pin")]
        public string Pin { get; set; }
        [JsonProperty("serverRef")]
        public string ServerRef { get; set; }
    }

    public class ActivityResult
    {
        public string Status { get; set; }
        public string ServerRef { get; set; }
    }

    public class PaymentResponseModel : ResponseBaseModel
    {
        [JsonProperty("data")]
        public PaymentRequest Data { get; set; }
    }

    public class PaymentRequest : ResponseDataBaseModel
    {
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("taxable")]
        public bool Taxable { get; set; }
        [JsonProperty("callbackURL")]
        public string CallBackUrl { get; set; }
        [JsonProperty("txnRef")]
        public string Transactionreference { get; set; }
        [JsonProperty("amount")]
        public double Amount { get; set; }

    }
}
