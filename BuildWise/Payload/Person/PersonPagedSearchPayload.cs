using static BuildWise.Enum.ProductEnum;
using static BuildWise.Enums.PersonEnum;

namespace BuildWise.Payload.Person
{
    public class PersonPagedSearchPayload : BasePagedSearchPayload
    {
        public EPersonSearchType SearchType { get; set;}
        public string? Search { get; set;}
        public int? Id { get; set;}
    }
}
