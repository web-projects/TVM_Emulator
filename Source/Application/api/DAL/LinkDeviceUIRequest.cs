using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace TVMEmulator.api.DAL
{
    public class LinkDeviceUIRequest
    {

        public LinkDeviceUIActionType? UIAction { get; set; }
        public bool? AutoConfirmKey { get; set; }
        public bool? ReportCardPresented { get; set; }
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
        public List<string> DisplayText { get; set; }
    }

    //DeviceUI action selection
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LinkDeviceUIActionType
    {
        DisplayIdle,
        KeyRequest,
        InputRequest,
        Display
    }
}
