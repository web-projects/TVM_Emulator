namespace TVMEmulator.api.Payment
{
    //Payment attributes
    public class LinkPaymentAttributes
    {
        public bool? PartialPayment { get; set; }
        public bool? SplitTender { get; set; }
        public bool? Installment { get; set; }
    }
}
