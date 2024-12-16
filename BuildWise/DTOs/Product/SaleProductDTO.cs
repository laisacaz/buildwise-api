using AutoMapper.Execution;

namespace BuildWise.DTO.Product
{
    public class SaleProductDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string BarCode { get; set; }
        public decimal StockQuantitySale { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
