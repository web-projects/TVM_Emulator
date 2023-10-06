using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TVMEmulator.api.DAL;
using TVMEmulator.api.Payment;
using TVMEmulator.api.Session;

namespace TVMEmulator.api
{
    public class LinkActionRequest
    {
        public string MessageID { get; set; }
        public string RequestID { get; set; }
        public string SessionID { get; set; }
        public LinkAction? Action { get; set; }
        public bool? AbortOnError { get; set; }
        public int? Timeout { get; set; }


        //Payment only used when Action = 'Payment'; can be null
        public Payment.LinkPaymentRequest PaymentRequest { get; set; }

        public LinkDALActionRequest DALActionRequest { get; set; }

        public LinkPaymentUpdateRequest PaymentUpdateRequest { get; set; }

        //DAL controls; can be null
        public LinkDALRequest DALRequest { get; set; }


        //Session controls; can be null
        public LinkSessionRequest SessionRequest { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum LinkAction
    {
        Payment,
        DALAction,
        Session,
        PaymentUpdate
    }

}
