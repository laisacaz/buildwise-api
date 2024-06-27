namespace BuildWise.DTO.Sale
{
    public class SaleServiceOrderDTO
    {
        public int Id { get; set; }
        public int ServiceOrderId { get; set; }
        public string Description { get; set; }
        public decimal StockQuantitySale { get; set; }
        public decimal Price { get; set; }
    }
}
