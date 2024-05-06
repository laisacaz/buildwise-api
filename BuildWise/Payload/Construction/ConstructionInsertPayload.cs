using BuildWise.Payload.Address;

namespace BuildWise.Payload.Construction
{
    public class ConstructionInsertPayload
    {
        public int ClientId { get; set; }
        public string? Observation { get; set; }
        public AddressInsertPayload? Address { get; set; } = null;
    }
}
