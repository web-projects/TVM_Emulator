using System.Collections.Generic;
using TVMEmulator.Helpers.Responses;

namespace TVMEmulator.helpers.responses
{
    public class SessionResponse
    {
        public string SessionID { get; set; }
        public List<ErrorValue> Errors { get; set; }
    }
}
