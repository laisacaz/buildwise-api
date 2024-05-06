using BuildWise.DTO;
using BuildWise.DTO.Construction;
using BuildWise.Interface.Repository;
using BuildWise.Interfaces.Repository.Person;
using static BuildWise.Enum.ConstructionEnum;

namespace BuildWise.Interfaces.Repository.Construction
{
    public interface IConstructionRepository : IBaseRepository<Entities.Construction> 
    {
        Task<SearchDTO<ConstructionPagedSearchDTO>> PagedSearch(
           string search,
           EConstructionSearchType? searchType,
           int? id,
           EStatusConstruction? status,
           int limit,
           int offset);
    }
}
