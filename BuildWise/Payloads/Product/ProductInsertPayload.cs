namespace BuildWise.Payload.Product
{
    public class ProductInsertPayload
    {
        public string? Description { get; set; } 
        public string? Reference { get; set; } 
        public decimal StockQuantity { get; set; }
        public string? BarCode { get; set; }
        public decimal Price { get; set; }
        public decimal? Cost { get; set; } 

    }
}
