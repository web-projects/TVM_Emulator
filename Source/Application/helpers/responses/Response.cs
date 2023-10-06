using System.Collections.Generic;
using TVMEmulator.Helpers.Responses;

namespace TVMEmulator.helpers.responses
{
    public class Response
    {
        public string MessageID { get; set; }
        public PaymentResponse PaymentResponse { get; set; }
        public PaymentUpdateResponse PaymentUpdateResponse { get; set; }
        public DALResponse DALResponse { get; set; }
        public SessionResponse SessionResponse { get; set; }
        public EventResponse EventResponse { get; set; }
        public string RequestID { get; set; }
        public List<ErrorValue> Errors { get; set; }
        public DALActionResponse DALActionResponse { get; set; }
    }
}
