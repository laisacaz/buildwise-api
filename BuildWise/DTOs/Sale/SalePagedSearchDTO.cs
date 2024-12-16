using BuildWise.Extensions;
using static BuildWise.Enum.SaleEnum;

namespace BuildWise.DTO.Sale
{
    public class SalePagedSearchDTO
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public ESaleStatus Status { get; set; }
        public string StatusDescription => Status.ToDescription();
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
