using static BuildWise.Enum.SaleEnum;

namespace BuildWise.Payload.Sale
{
    public class SaleUpdatePayload
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
        public int SellerId { get; set; }
        public int ConstructionId { get; set; }
        public List<SaleProductUpdatePayload> Products { get; set; }
        public string? Observation { get; set; }
    }
}
