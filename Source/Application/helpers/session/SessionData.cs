using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using TVMEmulator.forms;
using TVMEmulator.helpers.builders;
using TVMEmulator.Helpers.Responses;

namespace TVMEmulator.helpers.session
{
    public delegate void RefreshEventFunctions();
    public delegate void RichTextBoxFiller(string theData);

    public class SessionData
    {
        private static SessionConnection _sseHandler;
        private static string SSEURL = ConfigurationManager.AppSettings["ReceiverURL"] ?? "http://localhost:5112";
        public string PostURL = $"{SSEURL}/PostRequest";
        private List<EventData> _messages;
        public RefreshEventFunctions refreshEvents;
        private static SessionData _lastSessionData;
        private Dictionary<string, RichTextBoxFiller> _fillers;
        public string SessionID;
        public static Dictionary<string, bool> Devices;

        private object lockObj = new object();

        private SessionData()
        {
            _messages = new List<EventData>();

            if (_fillers == null)
            {
                _fillers = new Dictionary<string, RichTextBoxFiller>();
            }
            _lastSessionData = this;
        }
        
        public SessionData(RefreshEventFunctions eventRef)
            : this()
        {
            _messages = new List<EventData>();
            refreshEvents = eventRef;
        }

        public string[] GetPossibleEndPoints()
        {
            string receivers = ConfigurationManager.AppSettings["ReceiverURLs"];
            if (string.IsNullOrWhiteSpace(receivers))
            {
                return new string[] { PostURL };
            }

            string[] receiverArray = receivers.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < receiverArray.Length; i++)
            {
                receiverArray[i] = $"{receiverArray[i].Trim()}/PostRequest";
            }

            return receiverArray;
        }

        public static void RegisterMessage(string messageID, RichTextBoxFiller LoadFunction)
        {
            if (_lastSessionData != null)
            {
                _lastSessionData._fillers[messageID] = LoadFunction;
            }
        }

        public static void SetSessionID(string sessionID)
        {
            if (_lastSessionData != null && !string.IsNullOrWhiteSpace(sessionID))
            {
                RegisterSession(sessionID);
                _lastSessionData.refreshEvents?.Invoke();
            }
        }

        public static string GetSessionID()
        {
            return _lastSessionData?.SessionID;
        }

        public static void SetDevices(Dictionary<string, bool> devices)
        {
            Devices = devices;
        }

        public static Dictionary<string, bool> GetDevices()
        {
            return Devices;
        }

        public static void RegisterSession(string sessionID)
        {
            if (_lastSessionData != null)
            {
                if (sessionID != _lastSessionData.SessionID)
                {
                    _sseHandler = new SessionConnection(SSEURL, _lastSessionData.SawEvent, _lastSessionData.EventServerDisconnected);
                    _lastSessionData.SessionID = sessionID;
                    _sseHandler.OnStart(_lastSessionData.SessionID);
                }
            }
        }

        public static void ResetMessages()
        {
            if (_lastSessionData != null)
            {
                _lastSessionData._messages = new List<EventData>();
            }
        }

        public void EventServerDisconnected(string serverURL)
        {
            System.Diagnostics.Debug.WriteLine($"Disconnected from server {serverURL}");
        }

        public void ClearButtonData()
        {
            _messages = new List<EventData>();
        }

        public void SawEvent(string contents)
        {
            TCLinkResponse responses = JsonConvert.DeserializeObject<TCLinkResponse>(contents);
            if (responses.Responses == null)
            {
                Dictionary<string, string> sseData = JsonConvert.DeserializeObject<Dictionary<string, string>>(contents);
                StoreMessage(sseData["data"], "Event");
            }
            else
            {
                StoreMessage(contents, "Event");
            }
            refreshEvents?.Invoke();
        }

        private void StoreMessage(string content, string messType)
        {
            var responses = JsonConvert.DeserializeObject<TCLinkResponse>(content);
            string type = responses.Responses[0].EventResponse.EventCode;
            lock (lockObj)
            {
                int id = _messages.Count();
                var message = new EventData() { buttonID = id, contents = content, name = $"{id} - {type}", type = "Event" };
                _messages.Add(message);
            }
        }

        private void SawOutputPrivate(string fname, string contents)
        {
            try
            {
                var tcLinkResponse = JsonConvert.DeserializeObject<TCLinkResponse>(contents);
                contents = JsonConvert.SerializeObject(tcLinkResponse);
            }
            catch (Exception xcp)
            {
                JsonMessageBox.Show("Error", xcp.Message + "\r\nContents: " + contents);
                contents = JsonHelper.FormatJson(contents);
            }

            TCLinkResponse response = Utilities.DeserializeLinkResponse(contents);

            if (response is null)
            {
                return;
            }

            for (int i = _fillers.Count - 1; i >= 0 && i < _fillers.Count; i--)
            {
                string key = _fillers.Keys.ToArray()[i];
                if (key.Equals(response.MessageID))
                {
                    try
                    {
                        if (_fillers[key] != null)
                        {
                            _fillers[key].Invoke(contents);
                        }
                    }
                    catch (Exception xcp)
                    {
                        JsonMessageBox.Show("Error", xcp.Message);
                    }
                    _fillers.Remove(key);
                    return;
                }
            }

            JsonMessageBox.Show("Unknown Destination for Message", contents);
        }

        public static void SawOutput(string fname, string contents)
        {
            _lastSessionData?.SawOutputPrivate(fname, contents);
        }

        public List<EventData> GetEventData(string messType = "Event")
        {
            List<EventData> data = new List<EventData>();
            lock (lockObj)
            {
                foreach (var bdata in _messages)
                {
                    if (bdata.type.Equals(messType, StringComparison.OrdinalIgnoreCase))
                        data.Add(bdata);
                }
            }
            return data;
        }

        public EventData GetLastEventMatching(string type, string content)
        {
            lock (lockObj)
            {
                for (int i = _messages.Count - 1; i >= 0; i--)
                {
                    var bdata = _messages[i];
                    if (bdata.type.Equals(type, StringComparison.OrdinalIgnoreCase) && bdata.contents.Contains(content))
                    {
                        return bdata;
                    }
                }
            }

            return null;
        }
    }
}
