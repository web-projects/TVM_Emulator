using System.Collections.Generic;

namespace TVMEmulator.helpers.responses
{
    public class EventResponse
    {
        public string EventType { get; set; }
        public string EventCode { get; set; }
        public string EventID { get; set; }
        public int? OrdinalID { get; set; }
        public List<string> EventData { get; set; }

        public string Description { get; set; }
    }
}
