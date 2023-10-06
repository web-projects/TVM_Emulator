namespace TVMEmulator.api.Workflows
{
    public class LinkIIASRequest
    {
        public long HealthCareAmount { get; set; }
        public long PrescriptionAmount { get; set; }
        public long VisionAmount { get; set; }
        public long ClinicAmount { get; set; }
        public long DentalAmount { get; set; }
    }
}
