namespace BuildWise.Payload.Product
{
    public class ProductUpdatePayload
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
        public string? Description { get; set; }
        public string? Reference { get; set; }
        public decimal StockQuantity { get; set; }
        public string? BarCode { get; set; }
        public decimal Price { get; set; }
        public decimal? Cost { get; set; }
    }
}
