using static BuildWise.Enum.ConstructionEnum;
using static BuildWise.Enum.SaleEnum;

namespace BuildWise.Payload.Sale
{
    public class SalePagedSearchPayload : BasePagedSearchPayload
    {
        public int? Id { get; set; } = null;
        public ESaleStatus? Status { get; set; }
        public ESaleSearchType SearchType { get; set; }
        public string? Search { get; set; }
    }
}
