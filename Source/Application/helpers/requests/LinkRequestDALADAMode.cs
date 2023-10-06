using Newtonsoft.Json;
using System.Collections.Generic;
using TVMEmulator.api;
using TVMEmulator.api.DAL;
using TVMEmulator.helpers.builders;

namespace TVMEmulator.helpers.requests
{
    public static class LinkRequestDALADAMode
    {
        public static string MessageID;

        public static string SessionID;


        public static string GenerateStartRequest(long custid, string password, string adaMessage)
        {
            return GenerateADARequest(custid, password, true, adaMessage);
        }
        public static string GenerateUpdateMessageRequest(long custid, string password, string adaMessage)
        {
            return GenerateUpdateADAMessageRequest(custid, password, adaMessage);
        }
        public static string GenerateEndRequest(long custid, string password)
        {
            return GenerateADARequest(custid, password, false);
        }
        public static string GenerateADARequest(long custid = 0, string password = null, bool startAda = true, string adaMessage = null)
        {
            LinkRequest myRequest = RandomBuilder.BuildLinkAdaModeRequest(false, startAda);
            if (startAda)
            {
                if (myRequest.Actions[0].DALActionRequest.DeviceUIRequest == null)
                {
                    myRequest.Actions[0].DALActionRequest.DeviceUIRequest = new LinkDeviceUIRequest();
                }
                myRequest.Actions[0].DALActionRequest.DeviceUIRequest.DisplayText = new List<string>
                {
                    adaMessage
                };
            }

            if (!string.IsNullOrWhiteSpace(MessageID))
            {
                myRequest.MessageID = MessageID;
            }

            myRequest.TCCustID = custid > 0 ? custid : myRequest.TCCustID;
            myRequest.TCPassword = password ?? myRequest.TCPassword;
            SetAdaModeRequest(myRequest.Actions[0].DALRequest);
            Utilities.SetSessionID(myRequest, SessionID);
            return JsonConvert.SerializeObject(myRequest);
        }
        public static string GenerateUpdateADAMessageRequest(long custid = 0, string password = null, string adaMessage = null)
        {
            LinkRequest myRequest = RandomBuilder.BuildLinkDeviceUIRequest(false);
            if (myRequest.Actions[0].DALActionRequest.DeviceUIRequest == null)
            {
                myRequest.Actions[0].DALActionRequest.DeviceUIRequest = new LinkDeviceUIRequest();
            }
            myRequest.Actions[0].DALActionRequest.DeviceUIRequest.DisplayText = new List<string>
            {
                adaMessage
            };
            myRequest.Actions[0].DALActionRequest.DeviceUIRequest.UIAction = LinkDeviceUIActionType.Display;

            if (!string.IsNullOrWhiteSpace(MessageID))
            {
                myRequest.MessageID = MessageID;
            }

            myRequest.TCCustID = custid > 0 ? custid : myRequest.TCCustID;
            myRequest.TCPassword = password ?? myRequest.TCPassword;
            SetAdaModeRequest(myRequest.Actions[0].DALRequest);
            Utilities.SetSessionID(myRequest, SessionID);
            return JsonConvert.SerializeObject(myRequest);
        }
        public static void SetAdaModeRequest(LinkDALRequest request)
        {
            LinkRequestDALStatus.SetDALRequest(request);
            MessageID = null;
        }
    }
}
