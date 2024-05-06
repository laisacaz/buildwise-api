using BuildWise.Entities;
using static BuildWise.Enum.SaleEnum;

namespace BuildWise.Payload.Sale
{
    public class SaleInsertPayload
    {
        public int ClientId { get; set; }
        public int SellerId { get; set; }
        public int? ConstructionId { get; set; }
        public List<SaleProductInsertPayload> Products { get; set; }
        public string? Observation { get; set; }
    }
}
