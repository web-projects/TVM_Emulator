using TVMEmulator.Helpers.Responses;

namespace TVMEmulator.helpers.responses
{
    public class CardResponse
    {
        public string AuthorizationCode { get; set; }
        public string LeadingMaskedPAN { get; set; }
        public string TrailingMaskedPAN { get; set; }
        public string ExpMonth { get; set; }
        public string ExpYear { get; set; }
        public bool? SignatureRequested { get; set; }
        public bool? DebitCard { get; set; }
        public string EntryMode { get; set; }
        public string CardholderName { get; set; }
        public string TenderType { get; set; }
        public EMVResponse EMVData { get; set; }
        public string AVSStatus { get; set; }
        public string CommercialCard { get; set; }
        public string CardIdentifier { get; set; }
        public string HeldCardDataID { get; set; }
        public string CardSource { get; set; }
    }
}
