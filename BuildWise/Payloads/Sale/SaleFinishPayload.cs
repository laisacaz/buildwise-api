using static BuildWise.Enum.SaleEnum;

namespace BuildWise.Payload.Sale
{
    public class SaleFinishPayload
    {
        private int id;
        public int GetId() { return id; }
        public void SetId(int id) { this.id = id; }
        public ESaleReceivementMethod ReceivementMethod { get; set; }
        public decimal? Discount { get; set; }
        public decimal PaidAmount { get; set; } 
        public decimal? Increase { get; set; }
    }
}
