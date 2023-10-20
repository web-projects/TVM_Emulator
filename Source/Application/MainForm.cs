using Common.LoggerManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TVMEmulator.emulation;
using TVMEmulator.helpers;
using TVMEmulator.helpers.extensions;
using TVMEmulator.helpers.responses;
using TVMEmulator.helpers.session;
using TVMEmulator.Helpers.Responses;
using TVMEmulator.Properties;

namespace TVMEmulator
{
    public partial class MainForm : Form
    {
        private string messageWrapLength = ConfigurationManager.AppSettings["MessageWrapLength"] ?? "260";
        private readonly int lineWrapLength;

        private SessionEmulation sessionEmulation;
        private SessionData sessionData;

        private string sessionId;
        private string adaMessage;
        private string adaKeyPressed;

        public MainForm()
        {
            lineWrapLength = Convert.ToInt16(messageWrapLength);
            InitializeComponent();
            SetEnvironment();
        }

        private void SetEnvironment()
        {
            sessionData = new SessionData(RefreshEvent);
        }

        private void AdaModeExecute(object sender, EventArgs e)
        {
            if (adaModeBtn.Text == "Start")
            {
                adaModeStart();
            }
            else
            {
                adaModeStop();
            }
        }

        private void adaModeStart()
        {
            SetControlEnable(false);
            sessionIdLbl.Text = string.Empty;
            mainOutput.Text = string.Empty;
            adaKeyPressed = string.Empty;
            adaMessage = adaMessageTxt.Text;
            sessionEmulation = new SessionEmulation(RefreshEventWindow);
            sessionEmulation.SetTimingControls(ucTimings1);
            sessionData.ClearButtonData();
            sessionEmulation.StartEmulation(custidTB.Text, passwordTB.Text, "Initialize");
        }

        private void adaModeStop()
        {
            SetControlEnable(true);
            sessionEmulation.StopAdaMode();
        }

        private void SetControlEnable(bool mode)
        {
            receiverPort.Enabled = mode;
            adaMessageTxt.Enabled = mode;
            adaModeBtn.Text = mode ? "Start" : "Stop";
        }

        private string GetAdaMessage()
        {
            if (adaMessageTxt.InvokeRequired)
            {
                adaMessageTxt.Invoke(new MethodInvoker(
                    delegate { GetAdaMessage(); }
                ));
            }
            return adaMessageTxt.Text;
        }

        private void SetAdaMessage(string message)
        {
            if (adaMessageTxt.InvokeRequired)
            {
                adaMessageTxt.Invoke(new MethodInvoker(
                    delegate { SetAdaMessage(message); }
                ));
                return;
            }
            adaMessageTxt.Text = message;
        }

        public void RefreshEvent()
        {
            if (string.IsNullOrWhiteSpace(sessionIdLbl.Text))
            {
                sessionId = sessionData.SessionID;

                if (string.Compare(sessionId, sessionIdLbl.Text) != 0)
                {
                    sessionIdLbl.Text = sessionId;
                }
            }

            List<EventData> events = sessionData.GetEventData("Event");
            if (events.Count > 0)
            {
                EventData eventData = events.Last();
                // check for key press and send a message update
                if (eventData is { })
                {
                    RefreshEventWindowAndLogInfo(eventData.contents);

                    if (eventData.name.Contains("DEVICE_KEY_PRESSED"))
                    {
                        TCLinkResponse tcLinkResponse = Utilities.DeserializeLinkResponse(eventData.contents);

                        // Got a key press
                        Response response = tcLinkResponse.Responses.First();
                        EventResponse eventResponse = response.EventResponse;
                        string keyPressed = eventResponse.EventData.First();
                        int keyValue = (int)((DeviceKeys)Enum.Parse(typeof(DeviceKeys), keyPressed));
                        // Send new message to device with key just pressed
                        if (keyValue >= 0)
                        {
                            string keyValueString = keyPressed.Replace("KEY_", "");
                            Logger.info($"DEVICE_KEY_PRESSED: {keyValueString}");
                            adaKeyPressed += keyValueString;
                            string message = $"{adaMessage}{adaKeyPressed}";
                            SetAdaMessage(message);
                            sessionEmulation.UpdateMessage(message);
                        }
                    }
                }
            }
        }

        public void RefreshEventWindow()
        {
            if (mainOutput.InvokeRequired)
            {
                mainOutput.Invoke(new MethodInvoker(
                    delegate { RefreshEventWindow(); }
                ));
                return;
            }
            string jsonResponse = sessionEmulation?.GetSessionEmulationRefreshContent();
            Logger.info(PrepareMessageforLogging(jsonResponse));
            mainOutput.Text += jsonResponse + "\r\n";
            mainOutput.ScrollToBottom();
            StatusResponseUpdate(jsonResponse);
        }

        public void RefreshEventWindow(string message)
        {
            if (mainOutput.InvokeRequired)
            {
                mainOutput.Invoke(new MethodInvoker(
                    delegate { RefreshEventWindow(message); }
                ));
                return;
            }
            mainOutput.Text += message + "\r\n";
            mainOutput.ScrollToBottom();
        }

        private void StatusResponseUpdate(string jsonResponse)
        {
            if (jsonResponse.Contains("Request Submitted"))
            {
                toolStripStatusLabel1.Text = "Waiting for response ...";
            }
            else if (jsonResponse.Contains("DALResponse"))
            {
                TCLinkResponse linkResponse = Utilities.DeserializeLinkResponse(jsonResponse);

                foreach (Response response in linkResponse.Responses)
                {
                    // Initialize Response: check for errors
                    if (response.DALResponse.Devices is { })
                    {
                        foreach (DeviceResponse deviceResponse in response.DALResponse.Devices)
                        {
                            if (deviceResponse.Errors is { })
                            {
                                foreach (ErrorValue error in deviceResponse.Errors)
                                {

                                    toolStripStatusLabel1.Text = $"ERROR: {error.Code}";

                                }
                            }
                        }

                        // get the session id 
                        if (response.SessionResponse != null)
                        {
                            sessionId = response.SessionResponse.SessionID;
                            break;
                        }
                    }
                    else
                    {
                        if (response.DALActionResponse is { } actionResponse)
                        {
                            toolStripStatusLabel1.Text = $"RESPONSE: {actionResponse.Status} -> {actionResponse.Value}";
                            break;
                        }
                    }
                }
            }
            else if (jsonResponse.Contains("DALActionResponse"))
            {
                TCLinkResponse linkResponse = Utilities.DeserializeLinkResponse(jsonResponse);
                Response response = linkResponse.Responses.First();
                if (response is { } && response.DALActionResponse is { } actionResponse)
                {
                    toolStripStatusLabel1.Text = $"RESPONSE: {actionResponse.Status}";
                }
            }
        }

        private string PrepareMessageforLogging(string message)
        {
            string temp = Regex.Replace(message, @"\s+", " ");
            StringBuilder worker = new StringBuilder(temp.Replace("\n", "").Replace("\r", ""));
            int i = 0;

            try
            {
                for (i = lineWrapLength; i < worker.Length;)
                {
                    worker.Insert(i, "\r\n");
                    i += lineWrapLength;
                    if (i >= worker.Length)
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {

            }

            return worker.ToString();
        }

        private void OnSessionIdTextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(sessionIdLbl.Text) && sessionEmulation is { })
            {
                // Start ADA Mode
                sessionEmulation.StartAdaMode();
            }
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            if (Settings.Default.MainForm_HasSetDefaults)
            {
                this.WindowState = Settings.Default.MainForm_WindowState;
                this.Location = Settings.Default.MainForm_Location;
                this.Size = Settings.Default.MainForm_Size;
            }
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.MainForm_WindowState = this.WindowState;

            if (this.WindowState == FormWindowState.Normal)
            {
                Settings.Default.MainForm_Location = this.Location;
                Settings.Default.MainForm_Size = this.Size;
            }
            else
            {
                Settings.Default.MainForm_Location = this.RestoreBounds.Location;
                Settings.Default.MainForm_Size = this.RestoreBounds.Size;
            }

            Settings.Default.MainForm_HasSetDefaults = true;

            Settings.Default.Save();
        }

        private void ClearLog(object sender, EventArgs e)
        {
            mainOutput.Text = string.Empty;
            Logger.ClearLog();
        }

        private void RefreshEventWindowAndLogInfo(string message)
        {
            Logger.info(message);
            RefreshEventWindow(message);
        }
    }
}
