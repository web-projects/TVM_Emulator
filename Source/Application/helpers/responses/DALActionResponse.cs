using System.Collections.Generic;
using TVMEmulator.Helpers.Responses;

namespace TVMEmulator.helpers.responses
{
    public class DALActionResponse
    {
        public List<ErrorValue> Errors { get; set; }
        public string Status { get; set; }
        public string Value { get; set; }
        public bool? CardPresented { get; set; }
    }
}
