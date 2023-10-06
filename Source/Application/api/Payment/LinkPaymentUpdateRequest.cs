namespace TVMEmulator.api.Payment
{
    public class LinkPaymentUpdateRequest
    {
        public int RequestedAmount { get; set; }
        public bool? CompletePayment { get; set; }
        public bool? CancelPayment { get; set; }
        public bool? CancelTransactions { get; set; }
        public int? CancelTransactionsDelay { get; set; }
        public LinkPaymentAttributes PaymentAttributes { get; set; }
        public LinkReferenceInformation ReferenceInformation { get; set; }
    }
}
