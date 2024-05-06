using static BuildWise.Enum.ConstructionEnum;
using static BuildWise.Enum.SaleEnum;

namespace BuildWise.Payload.Construction
{
    public class ConstructionPagedSearchPayload : BasePagedSearchPayload
    {
        public int? Id { get; set; } = null;
        public EStatusConstruction? Status { get; set; }
        public EConstructionSearchType SearchType { get; set; }
        public string? Search { get; set; }
    }
}
