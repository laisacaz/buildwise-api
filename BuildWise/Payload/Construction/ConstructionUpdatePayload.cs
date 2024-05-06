using BuildWise.Payload.Address;
using BuildWise.Payload.Product;
using static BuildWise.Enum.ConstructionEnum;

namespace BuildWise.Payload.Construction
{
    public class ConstructionUpdatePayload
    {
        private int id;
        public void SetId(int id)
        {
            this.id = id;
        }
        public int GetId()
        {
            return id;
        }
        public int ClientId { get; set; }
        public string? Observation { get; set; }
        public AddressUpdatePayload? Address { get; set; }
        public EStatusConstruction Status { get; set; }
    }
}
