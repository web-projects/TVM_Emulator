using System.Collections.Generic;
using TVMEmulator.api.Payment;

namespace TVMEmulator.api.Workflows
{
    public class LinkCardWorkflowControls
    {
        public int? CardCaptureTimeout { get; set; }
        public int? ManualCardTimeout { get; set; }
        public int? SignatureCaptureTimeout { get; set; }
        public bool? DebitEnabled { get; set; }
        public bool? EMVEnabled { get; set; }
        public bool? ContactlessEnabled { get; set; }
        public bool? ContactlessEMVEnabled { get; set; }
        public bool? CVVEnabled { get; set; }
        public bool? VerifyAmountEnabled { get; set; }
        public bool? CardExpEnabled { get; set; }
        public bool? AVSEnabled { get; set; }
        public bool? SignatureEnabled { get; set; }
        public bool? AllowCardInReader { get; set; }
        public bool? ForceDebit { get; set; }
        public bool? ForceCredit { get; set; }
        public List<LinkThresholdAmount> SignatureThresholds { get; set; }
    }
}
