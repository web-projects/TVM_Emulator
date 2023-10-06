using System.Collections.Generic;
using TVMEmulator.api.Workflows;
using TVMEmulator.Helpers.Responses;

namespace TVMEmulator.helpers.responses
{
    public class DeviceResponse
    {
        public List<ErrorValue> Errors { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string SerialNumber { get; set; }
        public string Port { get; set; }
        public List<string> Features { get; set; }
        public List<string> Configurations { get; set; }
        public LinkCardWorkflowControls CardWorkflowControls { get; set; }
    }
}
