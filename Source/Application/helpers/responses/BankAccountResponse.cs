namespace TVMEmulator.helpers.responses
{
    public class BankAccountResponse
    {
        public string AccountType { get; set; }
        public string RoutingNumber { get; set; }
        public string TrailingAccountNumber { get; set; }
        public string CheckNumber { get; set; }
        public string HolderName { get; set; }
    }
}
