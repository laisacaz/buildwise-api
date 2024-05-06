namespace BuildWise.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public decimal StockQuantity { get; set; }
        public string BarCode { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
    }
}
