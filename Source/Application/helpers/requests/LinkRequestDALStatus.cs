using Newtonsoft.Json;
using System;
using TVMEmulator.api;
using TVMEmulator.api.DAL;
using TVMEmulator.helpers.builders;

namespace TVMEmulator.helpers.requests
{
    public static class LinkRequestDALStatus
    {
        public static string MessageID;
        public static string SessionID;
        public static string TCCustID;
        public static string Password;

        public static string GenerateStatusRequest(long custid = 0, string password = null)
        {
            LinkRequest myRequest = RandomBuilder.BuildLinkDALStatusRequest(false);  //Even though we are local, build as if not.

            if (!string.IsNullOrWhiteSpace(MessageID))
            {
                myRequest.MessageID = MessageID;
            }

            myRequest.TCCustID = custid > 0 ? custid : myRequest.TCCustID;
            myRequest.TCPassword = password ?? myRequest.TCPassword;
            SetDALRequest(myRequest.Actions[0].DALRequest);
            Utilities.SetSessionID(myRequest, SessionID);
            return JsonConvert.SerializeObject(myRequest);
        }

        public static void SetDALRequest(LinkDALRequest request, LinkDALLookupPreference? lookupPreference = LinkDALLookupPreference.Username)
        {
            request.DALIdentifier.DnsName = Utilities.GetHostName();
            request.DALIdentifier.WorkstationName = Utilities.GetHostName();
            request.DALIdentifier.Username = Environment.UserName;
            request.DALIdentifier.IPv4 = Utilities.GetIPv4();
            request.DALIdentifier.IPv6 = Utilities.GetIPv6();
            request.DALIdentifier.LookupPreference = lookupPreference;
            MessageID = null;
        }

        public static string GenerateSetKBRequest(LinkDeviceIdentifier device, long custid = 0, string password = null)
        {
            LinkRequest myRequest = RandomBuilder.BuildLinkDALStatusRequest(true);
            myRequest.Actions[0].DALActionRequest.DALAction = LinkDALActionType.SetDeviceKB;
            myRequest.Actions[0].DALRequest.DALIdentifier ??= new LinkDALIdentifier();

            if (!string.IsNullOrWhiteSpace(MessageID))
            {
                myRequest.MessageID = MessageID;
            }

            myRequest.TCCustID = custid > 0 ? custid : myRequest.TCCustID;
            myRequest.TCPassword = password ?? myRequest.TCPassword;
            SetDALRequest(myRequest.Actions[0].DALRequest);
            myRequest.Actions[0].DALRequest.DeviceIdentifier = device;
            Utilities.SetSessionID(myRequest, SessionID);
            return JsonConvert.SerializeObject(myRequest);
        }
    }
}
