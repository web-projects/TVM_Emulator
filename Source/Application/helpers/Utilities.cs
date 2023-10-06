using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TVMEmulator.api;
using TVMEmulator.api.Payment;
using TVMEmulator.forms;
using TVMEmulator.helpers.session;
using TVMEmulator.Helpers.Responses;
using static TVMEmulator.helpers.builders.RandomBuilder;

namespace TVMEmulator.helpers
{
    public static class Utilities
    {
        public static void SetSessionID(LinkRequest myRequest, string SessionID)
        {
            foreach (var action in myRequest.Actions)
            {
                action.SessionID = SessionID;
            }
        }

        public static int NumberOfCents(string dollarAmount)
        {
            if (double.TryParse(dollarAmount, out double amount))
            {
                return (int)Math.Floor((amount * 100) + 0.1);  //Fix a rounding error, with floor 13.999999 goes to 13
            }
            return 0;
        }

        public static int IntFromString(string origValue, int defValue)
        {
            if (int.TryParse(origValue, out int output))
                return output;
            return defValue;
        }

        public static long LongFromString(string origValue, long defValue)
        {
            if (long.TryParse(origValue, out long output))
                return output;
            return defValue;
        }

        public static LinkRequest DeserializeLinkRequest(string theJSON)
        {
            LinkRequest theRequest = null;
            try
            {
                theRequest = JsonConvert.DeserializeObject<LinkRequest>(theJSON);
            }
            catch (Exception xcp)
            {
                System.Diagnostics.Debug.WriteLine(xcp.Message);
            }
            if (string.IsNullOrWhiteSpace(theRequest.MessageID))
            {
                theRequest.MessageID = BuildRandomString(15);
            }
            return theRequest;
        }

        public static string GetIPv4()
        {
            try
            {
                string response;
                using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("10.0.2.4", 65530);  // Random IP and port
                    var endPoint = socket.LocalEndPoint as IPEndPoint;
                    response = endPoint?.Address.ToString();
                }
                return response;
            }
            catch
            {
                return "127.0.0.1";
            }
        }

        public static string GetIPv6()
        {
            var response = string.Empty;
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    response = ip.ToString();
                    break;
                }
            }
            return response;
        }

        public static string GetHostName()
        {
            return Dns.GetHostName();
        }

        public static TCLinkResponse DeserializeLinkResponse(string theJSON)
        {
            TCLinkResponse theResponse = null;
            try
            {
                theResponse = JsonConvert.DeserializeObject<TCLinkResponse>(theJSON);
            }
            catch (Exception xcp)
            {
                System.Diagnostics.Debug.WriteLine(xcp.Message);
            }
            return theResponse;
        }

        private static async Task<string> CallWebService(string endpoint, string contents, int timeout = 100, string requestId = "")
        {
            using HttpClient client = HttpClientFactory.Create();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));

            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            client.DefaultRequestHeaders.Add(Constants.IPARequestedRequestId, requestId);

            client.Timeout = new TimeSpan(0, 0, 0, timeout);

            using StringContent content = new StringContent(contents, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("text/json");

            using HttpResponseMessage httpResponse = await client.PostAsync(endpoint, content).ConfigureAwait(false);
            return await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        public static async Task SendRequest(string endpoint, string contents, ucTimings timingControl, int timeout = 100)
        {
            if (endpoint?.StartsWith("http", StringComparison.OrdinalIgnoreCase) ?? false)
            {
                try
                {
                    timingControl.StartTracking();

                    string lastRequestId = RequestEventSink.Instance.GenerateRequestId();
                    string response = await CallWebService(endpoint, contents, timeout, lastRequestId);

                    SessionData.SawOutput("SSEOutput", response);

                    timingControl.StopTracking();
                }
                catch (Exception xcp)
                {
                    JsonMessageBox.Show("Error", xcp.Message);
                }
                finally
                {
                    timingControl.StopTracking();
                }
                return;
            }
            System.Diagnostics.Debug.WriteLine($"Unable to send message to {endpoint} :: {contents}");
        }

        public static LinkReferenceInformation lastReferenceInformation;

        public static LinkReferenceInformation ReferenceInformation(bool newValue = true, string merchantTransactionId = null, string ticket = null)
        {
            if (newValue || lastReferenceInformation is null)
            {
                lastReferenceInformation = new LinkReferenceInformation()
                {
                    MerchantTransactionID = string.IsNullOrWhiteSpace(merchantTransactionId) ? BuildRandomString(15) : merchantTransactionId,
                    Ticket = string.IsNullOrWhiteSpace(ticket) ? null : ticket
                };
            }
            return lastReferenceInformation;
        }
    }
}
