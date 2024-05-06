using BuildWise.Extensions;
using static BuildWise.Enum.ConstructionEnum;

namespace BuildWise.DTO.Construction
{
    public class ConstructionPagedSearchDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ClientName { get; set; }
        public EStatusConstruction Status { get; set; }
        public string StatusDescription => Status.ToDescription();
    }
}
