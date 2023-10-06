
using System;

namespace TVMEmulator.api.DAL
{
    //DAL controls
    public partial class LinkDALRequest
    {
        public LinkDALIdentifier DALIdentifier { get; set; }

        public LinkDeviceIdentifier DeviceIdentifier { get; set; }

        public Guid? HeldCardDataID { get; set; }
    }
}
