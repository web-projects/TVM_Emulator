namespace TVMEmulator.api.Payment
{
    public class LinkPayerRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ZIPCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Phone { get; set; }
    }
}
