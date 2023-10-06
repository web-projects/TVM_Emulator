using System.Collections.Generic;
using TVMEmulator.Helpers.Responses;

namespace TVMEmulator.helpers.responses
{
    public class PaymentUpdateResponse
    {
        List<ErrorValue> Errors { get; set; }
        public string Status { get; set; }
        public string OriginalTransactionId { get; set; }
        public List<LinkNameValueResponse> TCLinkResponse { get; set; }
    }
}
