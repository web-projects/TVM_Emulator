using System.Collections.Generic;
using TVMEmulator.Helpers.Responses;

namespace TVMEmulator.helpers.responses
{
    public class PaymentResponse
    {
        public List<ErrorValue> Errors { get; set; }
        public string Status { get; set; }
        public string EntryModeStatus { get; set; }
        public List<LinkNameValueResponse> TCLinkResponse { get; set; }
        public string TCTransactionID { get; set; }
        public string TCTimestamp { get; set; }
        public long? CollectedAmount { get; set; }
        public CardResponse CardResponse { get; set; }
        public EMVResponse EMVResponse { get; set; }
        public string BillingID { get; set; }
        public BankAccountResponse AccountResponse { get; set; }
        public ReceiptResponse ReceiptResponse { get; set; }
        public IIASResponse IIASResponse { get; set; }
        public string CancelType { get; set; }
    }
}
