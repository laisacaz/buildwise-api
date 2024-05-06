namespace BuildWise.DTO.Product
{
    public class ProductPagedSearchDTO
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal StockQuantity { get; set; }
    }
}
