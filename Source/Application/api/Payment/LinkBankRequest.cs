using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TVMEmulator.api.Payment
{
    public class LinkBankRequest
    {
        public string RoutingNumber { get; set; }
        public string AccountNumber { get; set; }
        public LinkBankAccountType? BankAccountType { get; set; }
        public LinkACHSecCode? SecCode { get; set; }
        public string CheckNumber { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum LinkBankAccountType
    {
        Personal,
        Business,
        //Savings               //Not supported currently
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum LinkACHSecCode
    {
        TEL,
        POP
    }
}
