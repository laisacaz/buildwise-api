namespace BuildWise.DTO.Report.Product
{
    public class ProductRankingDTO
    {                
        public int Id { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public string BarCode { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public decimal Total { get; set; }
        public decimal SaledTimes { get; set; }
    }
}
