
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using TVMEmulator.api.DAL;
using TVMEmulator.api.Workflows;

namespace TVMEmulator.api.Session
{
    public class LinkSessionRequest
    {
        public LinkSessionActionType? SessionAction { get; set; }

        public bool? Exclusive { get; set; }
        public LinkDALRequest DALRequest { get; set; }
        public int? DefaultCVMAmount { get; set; }
        public string DefaultCVMCurrencyCode { get; set; }
        public LinkWorkflowControls WorkflowControls { get; set; }
        public LinkCardWorkflowControls CardWorkflowControls { get; set; }
        public List<LinkActionRequest> IdleActions { get; set; }
    }


    //DAL action selection
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LinkSessionActionType
    {
        Initialize
    }
}
