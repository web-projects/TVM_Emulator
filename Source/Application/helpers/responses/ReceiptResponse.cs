using TVMEmulator.Helpers.Responses;

namespace TVMEmulator.helpers.responses
{
    public class ReceiptResponse
    {
        public string CardholderPANFirst6 { get; set; }
        public string CardholderPANLast4 { get; set; }
        public string CardExpirationDate { get; set; }
        public string MerchantTerminalID { get; set; }
        public string TransactionAmount { get; set; }
        public string TransactionCurrencyCode { get; set; }
        public string ApplicationPreferredName { get; set; }
        public string PaymentNetwork { get; set; }
        public string ApplicationID { get; set; }
        public string CardEntryMethod { get; set; }
        public string CardholderVerificationMethod { get; set; }
        public string AuthorizationMode { get; set; }
        public string CardholderName { get; set; }
        public string ApplicationLabel { get; set; }
        public string PANSequenceNumber { get; set; }
        public string ApplicationInterchangeProfile { get; set; }
        public string TerminalVerificationResults { get; set; }
        public string TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public string AmountAuthorized { get; set; }
        public string OtherAmount { get; set; }
        public string ApplicationUsageControl { get; set; }
        public ActionCode IssuerActionCode { get; set; }
        public string TerminalCountryCode { get; set; }
        public string ApplicationCryptogram { get; set; }
        public string CryptogramInformationData { get; set; }
        public string CardholderVerificationMethodResults { get; set; }
        public string ApplicationTransactionCounter { get; set; }
        public string UnpredictableNumber { get; set; }
        public ActionCode TerminalActionCode { get; set; }
    }
}
