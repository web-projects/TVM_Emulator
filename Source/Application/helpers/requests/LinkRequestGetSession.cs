using Newtonsoft.Json;
using TVMEmulator.api;
using TVMEmulator.helpers.builders;

namespace TVMEmulator.helpers.requests
{
    public static class LinkRequestGetSession
    {
        public static string MessageID;
        public static string SessionID;

        public static string GenerateGetSessionRequest(LinkRequest linkRequest, long custid = 0, string password = null)
        {
            LinkRequest myRequest = linkRequest ?? RandomBuilder.BuildLinkSessionRequest();
            myRequest.MessageID = MessageID ?? myRequest.MessageID;
            myRequest.TCCustID = custid > 0 ? custid : myRequest.TCCustID;
            myRequest.TCPassword = password ?? myRequest.TCPassword;
            LinkRequestDALStatus.SetDALRequest(myRequest.Actions[0].DALRequest, null);
            Utilities.SetSessionID(myRequest, SessionID);

            return JsonConvert.SerializeObject(myRequest);
        }
    }
}
