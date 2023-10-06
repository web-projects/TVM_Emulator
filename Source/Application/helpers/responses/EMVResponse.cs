namespace TVMEmulator.helpers.responses
{
    public class EMVResponse
    {
        public string ApplicationID { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationResponse { get; set; }
        public string AuthorizationMode { get; set; }
        public string IssuerApplicationData { get; set; }
        public string CardHolderVerification { get; set; }
        public string TerminalVerificationResults { get; set; }
        public string TransactionStatusInformation { get; set; }
    }
}
