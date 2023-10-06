using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using TVMEmulator.api.Workflows;

namespace TVMEmulator.api.Payment
{
    //Base Transaction values for IPA5
    public class LinkPaymentRequest
    {
        public int RequestedAmount { get; set; }
        public int RequestedAmountOther { get; set; }
        public string CurrencyCode { get; set; }
        public int? MasterTCCustID { get; set; }
        public string MasterTCPassword { get; set; }
        public string PreviousBillingID { get; set; }
        public string PreviousTCTransactionID { get; set; }
        public LinkPaymentRequestType? PaymentType { get; set; }
        public bool? CreateBillingID { get; set; }
        public bool? UpdateBillingIDTender { get; set; }
        public LinkPayerRequest PayerValues { get; set; }
        public LinkBankRequest BankAccountValues { get; set; }
        public bool? AVSVerify { get; set; }
        public LinkReferenceInformation ReferenceInformation { get; set; }
        public List<string> PartnerRegistryKeys { get; set; }
        public List<Workflows.LinkCustomField> CustomFields { get; set; }

        public LinkPaymentRequestedTenderType? RequestedTenderType { get; set; }

        public LinkPaymentAttributes PaymentAttributes { get; set; }


        public LinkWorkflowControls WorkflowControls { get; set; }


        public LinkCardWorkflowControls CardWorkflowControls { get; set; }

        public bool? Demo { get; set; }
        public LinkIIASRequest IIASRequest { get; set; }

    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum LinkPaymentRequestType
    {
        Sale,
        Cancel,
        Void,
        Credit,
        ChargeBack,
        Reversal,
        PreAuth,
        PostAuth,
        Store,
        Unstore,
        Update,
        Verify,
        BalanceInquiry,
    }

    //Payment tender types
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LinkPaymentRequestedTenderType
    {
        Unspecified,
        Card,
        Check
    }

}
