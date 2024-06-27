namespace BuildWise.Entities
{
    public class SaleServiceOrder
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int SaleId { get; set; }
        public int ServiceId { get; set; }
        public decimal StockQuantity { get; set; }
    }
}
