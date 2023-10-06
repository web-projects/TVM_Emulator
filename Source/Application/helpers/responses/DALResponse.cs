using System.Collections.Generic;
using TVMEmulator.api.DAL;
using TVMEmulator.Helpers.Responses;

namespace TVMEmulator.helpers.responses
{
    public class DALResponse
    {
        public LinkDALIdentifier DALIdentifier { get; set; }
        public bool? OnlineStatus { get; set; }
        public bool? AvailableStatus { get; set; }
        public List<DeviceResponse> Devices { get; set; }
        public List<ErrorValue> Errors { get; set; }
    }
}
