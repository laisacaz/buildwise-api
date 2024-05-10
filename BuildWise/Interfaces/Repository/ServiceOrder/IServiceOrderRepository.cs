using BuildWise.DTO.ServiceOrder;
using BuildWise.DTO;
using BuildWise.Interface.Repository;

namespace BuildWise.Interfaces.Repository.ServiceOrder
{
    public interface IServiceOrderRepository : IBaseRepository<Entities.ServiceOrder>
    {
        Task<SearchDTO<ServiceOrderPagedSearchDTO>> PagedSearch(
            string search,
            int limit,
            int offset);
    }
}
