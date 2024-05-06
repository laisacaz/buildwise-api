using BuildWise.DTO;
using BuildWise.DTO.Person;
using BuildWise.Interface.Repository;
using static BuildWise.Enums.PersonEnum;

namespace BuildWise.Interfaces.Repository.Person
{
    public interface IPersonRepository : IBaseRepository<Entities.Person>
    {
        IPersonAddressRepository Address{ get; }
        Task<SearchDTO<PersonPagedSearchDTO>> PagedSearch(
            string search,
            EPersonSearchType SearchType,
            int? id,
            int limit,
            int offset);
        Task<List<PersonSimpleDTO>> GetAllActivesSimple();
        Task<List<PersonSimpleDTO>> GetAllSimple();
        Task<PersonSimpleDTO> GetSimple(int id);
    }
}
