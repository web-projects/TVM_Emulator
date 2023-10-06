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
using TVMEmulator.helpers.session;

namespace TVMEmulator
{
    public partial class Form1 : Form
    {
        private string messageWrapLength = ConfigurationManager.AppSettings["MessageWrapLength"] ?? "260";
        private readonly int lineWrapLength;

        private SessionEmulation sessionEmulation;
        private SessionData sessionData;

        private string sessionId;
        private string adaMessage;
        private string adaKeyPressed;

        public Form1()
        {
            InitializeComponent();
            sessionData = new SessionData(RefreshEvent);
            lineWrapLength = Convert.ToInt16(messageWrapLength);
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
                if (eventData is { } && eventData.name.Contains("DEVICE_KEY_PRESSED"))
                {
                    Helpers.Responses.TCLinkResponse tcLinkResponse = Utilities.DeserializeLinkResponse(eventData.contents);

                    // Got a key press
                    helpers.responses.Response response = tcLinkResponse.Responses.First();
                    helpers.responses.EventResponse eventResponse = response.EventResponse;
                    string keyPressed = eventResponse.EventData.First();
                    int keyValue = (int)((DeviceKeys)Enum.Parse(typeof(DeviceKeys), keyPressed));
                    // Send new message to device with key just pressed
                    if (keyValue > 0)
                    {
                        Logger.info($"DEVICE_KEY_PRESSED: {keyValue}");
                        adaKeyPressed += $"{keyValue}";
                        string message = $"{adaMessage}{adaKeyPressed}";
                        SetAdaMessage(message);
                        sessionEmulation.UpdateMessage(message);
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
            string message = sessionEmulation?.GetSessionEmulationRefreshContent();
            Logger.info(PrepareMessageforLogging(message));
            mainOutput.Text += message + "\r\n";
            mainOutput.ScrollToBottom();
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
    }
}
