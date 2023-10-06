namespace TVMEmulator.api.Workflows
{
    //Payment workflow controls (note: only values that might affect DAL workflow are included)
    public class LinkWorkflowControls
    {

        public bool? CardEnabled { get; set; }
        public bool? CheckEnabled { get; set; }
        public bool? SuppressMonitor { get; set; }
        public int? WorkflowTimeout { get; set; }
        public int? TenderSelectionTimeout { get; set; }
        public int? ManualCheckTimeout { get; set; }
        public int? ServiceTimeout { get; set; }
    }
}
