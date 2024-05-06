using BuildWise.DTO.Sale;
using BuildWise.DTO;
using BuildWise.Interface.Repository;
using BuildWise.Interfaces.Repository.Construction;
using static BuildWise.Enum.SaleEnum;
using BuildWise.DTO.Report.Sale;

namespace BuildWise.Interfaces.Repository.Sale
{
    public interface ISaleRepository : IBaseRepository<Entities.Sale>
    {
        ISaleProductRepository Product { get; } 
        Task<SearchDTO<SalePagedSearchDTO>> PagedSearch(
            string? search,
            ESaleSearchType? searchType,
            int? id,
            ESaleStatus? status,
            int limit,
            int offset);

        Task<List<SaleByDateDTO>> GetAllByDate(
            DateTime startDate,
            DateTime endDate);
    }
}
