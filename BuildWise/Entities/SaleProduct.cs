namespace BuildWise.Entities
{
    public class SaleProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SaleId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal StockQuantity { get; set; }
    }
}
