using static BuildWise.Enum.ProductEnum;

namespace BuildWise.Payload.Product
{
    public class ProductPagedSearchPayload : BasePagedSearchPayload
    {
        public EProductSearchType SearchType { get; set; }
        public string? Search { get; set;} 
    }
}
