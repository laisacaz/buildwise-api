using BuildWise.DTO.Person;

namespace BuildWise.DTO.Report.Sale
{
    public class SaleByDateDTO
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public int ClientId { get; set; }
        public PersonSimpleDTO Client { get; set; }
        public PersonSimpleDTO Seller { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TotalProducts { get; set; }
        public int Total { get; set; }
    }
}
