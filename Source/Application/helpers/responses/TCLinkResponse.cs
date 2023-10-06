using System.Collections.Generic;
using TVMEmulator.helpers.responses;

namespace TVMEmulator.Helpers.Responses
{
    public class TCLinkResponse
    {
        public string MessageID { get; set; }
        public List<Response> Responses { get; set; }
        public List<ErrorValue> Errors { get; set; }
    }
}
