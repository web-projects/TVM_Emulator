using Common.LoggerManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using TVMEmulator.api;
using TVMEmulator.api.Session;
using TVMEmulator.emulation.ADA;
using TVMEmulator.forms;
using TVMEmulator.helpers;
using TVMEmulator.helpers.builders;
using TVMEmulator.helpers.requests;
using TVMEmulator.helpers.session;

namespace TVMEmulator.emulation
{
    internal class SessionEmulation
    {
        private string DEVICES_DISPLAY_FORMAT = "{0}: {1} - {2}";
        private SSESubscriber _subscriber;

        public static List<LinkRequest> LinkRequestsSent = new List<LinkRequest>();

        public static long CustID;
        public static string custId;
        public static string Password;
        public static string SessionId;

        public string endpoint = ConfigurationManager.AppSettings["ReceiverURL"] ?? "http://localhost:5112";
        public string defCustID = ConfigurationManager.AppSettings["TCCustId"] ?? "1117600";
        public string defPassword = ConfigurationManager.AppSettings["TCPassword"] ?? "ipa1234";

        public RefreshEventFunctions refreshEvents;

        public string ServerUrl { get; }
        private string _serverURL;

        private ucTimings timingControl;
        private SessionConnection sessionConnection;
        private string sessionRefreshContent;

        private AdaMode adaMode;

        public SessionEmulation(RefreshEventFunctions eventRef)
            => refreshEvents = eventRef;

        public void StartEmulation(string custid, string password, string action)
        {
            LinkRequestsSent = new List<LinkRequest>();

            if (!endpoint.Contains("PostRequest"))
            {
                endpoint += "/PostRequest";
            }

            string jsonRequest = GenerateRequest(custid, password, action);

            SubmitRequest(jsonRequest);
        }

        public void SetTimingControls(ucTimings timingControl)
            => this.timingControl = timingControl;

        public string GetSessionEmulationRefreshContent()
            => sessionRefreshContent;

        private string GenerateRequest(string custid, string password, string action)
        {
            long.TryParse(custid, out long tempCustid);

            if (tempCustid <= 0)
            {
                MessageBox.Show("Invalid Custid");
                return "Invalid Custid";
            }

            CustID = tempCustid;
            custId = custid;
            Password = password;
            LinkRequestGetSession.MessageID = RandomBuilder.BuildRandomString(7) + "TH_Sess";
            string output = "ERROR";

            switch (action)
            {
                case "Initialize":
                    output = LinkRequestGetSession.GenerateGetSessionRequest(null, CustID, Password);
                    Logger.info("Session Initialization Request.");
                    break;
                default:
                    MessageBox.Show($"Action: {action} is not yet implemented.");
                    break;
            }

            return output;
        }

        private void SubmitRequest(string jsonRequest)
        {
            try
            {
                sessionRefreshContent = $"Request Submitted to '{endpoint}', awaiting results...";
                refreshEvents?.Invoke();

                LinkRequest curRequest = Utilities.DeserializeLinkRequest(jsonRequest);
                
                if (curRequest == null)
                {
                    LoadResponseRTB("Error in Test Harness.\n\n\nUnable to create LinkRequest from the Text in the Request Box!\n\n\nPlease double check it is properly formatted.");
                    MessageBox.Show("Error Parsing LinkRequest.  Please use a different tool to submit poorly formatted LinkRequests.");
                    return;
                }
                SessionData.RegisterMessage(curRequest.MessageID, LoadResponseRTB);
                LinkRequestsSent.Add(curRequest);
                _ = Utilities.SendRequest(endpoint, jsonRequest, timingControl);
            }
            catch (Exception xcp)
            {
                MessageBox.Show("Error: " + xcp.Message);
                System.Diagnostics.Debug.WriteLine(xcp.Message);
            }
        }

        public void LoadResponseRTB(string theJson)
        {
            sessionRefreshContent = theJson;
            refreshEvents?.Invoke();

            ParseResponseForImportantInformation(theJson);
        }

        private void ParseResponseForImportantInformation(string theJson)
        {
            LinkRequest request = null;
            var response = Utilities.DeserializeLinkResponse(theJson);

            if (response.MessageID != null)
            {
                foreach (LinkRequest req in LinkRequestsSent)
                {
                    if (req.MessageID.Equals(response.MessageID))
                    {
                        request = req;
                        break;
                    }
                }
                LinkRequestsSent.Remove(request);
            }

            if (request != null)
            {
                if (request.Actions[0].Action == LinkAction.Session
                    && request.Actions[0].SessionRequest.SessionAction == LinkSessionActionType.Initialize)
                {
                    SessionId = response?.Responses?[0]?.SessionResponse?.SessionID;
                    SessionData.SetSessionID(SessionId);

                    int count = response?.Responses?[0]?.DALResponse?.Devices?.Count ?? 0;
                    if (count > 0)
                    {
                        Dictionary<string, bool> Devices = new Dictionary<string, bool>();

                        foreach (var device in response?.Responses?[0]?.DALResponse.Devices)
                        {
                            if (device?.Features is null)
                            {
                                continue;
                            }
                            if (!string.IsNullOrEmpty(device.Manufacturer) && !string.IsNullOrEmpty(device.Model) && !string.IsNullOrEmpty(device.SerialNumber))
                            {
                                bool adaDevice = device.Features != null && device.Features.Contains(Features.ADA.GetStringValue());
                                Devices.Add(string.Format(DEVICES_DISPLAY_FORMAT, device.Manufacturer, device.Model, device.SerialNumber), adaDevice);
                            }
                        }
                        SessionData.SetDevices(Devices);
                    }
                }
            }
        }

        #region --- ADA MODE IMPLEMENTATION ---
        public void StartAdaMode()
        {
            adaMode = new AdaMode(custId, Password, SessionId);
            string jsonRequest = adaMode.GenerateRequest(AdaActionCode.StartAdaMode.GetStringValue(), "Pay Now?");
            SubmitRequest(jsonRequest);
            Logger.info($"{AdaActionCode.StartAdaMode.GetStringValue()} Request.");
        }

        public void UpdateMessage(string message)
        {
            string jsonRequest = adaMode.GenerateRequest(AdaActionCode.UpdateMessage.GetStringValue(), message);
            SubmitRequest(jsonRequest);
            Logger.info($"ADA {AdaActionCode.UpdateMessage.GetStringValue()} Request.");
        }

        public void StopAdaMode()
        {
            string jsonRequest = adaMode.GenerateRequest(AdaActionCode.EndAdaMode.GetStringValue(), "");
            SubmitRequest(jsonRequest);
            Logger.info($"{AdaActionCode.EndAdaMode.GetStringValue()} Request.");
        }

        #endregion --- ADA MODE IMPLEMENTATION ---
    }
}
