namespace BuildWise.Payload.Address
{
    public class AddressInsertPayload
    {
        public string? ZipCode { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? StreetNumber { get; set; }
        public string? District { get; set; }
    }
}
