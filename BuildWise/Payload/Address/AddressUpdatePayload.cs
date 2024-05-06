namespace BuildWise.Payload.Address
{
    public class AddressUpdatePayload
    {
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string District { get; set; }
    }
}
