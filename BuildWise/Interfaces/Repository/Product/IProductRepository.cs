using BuildWise.DTO;
using BuildWise.DTO.Product;
using BuildWise.Interface.Repository;
using static BuildWise.Enum.ProductEnum;

namespace BuildWise.Interfaces.Repository.Product
{
    public interface IProductRepository : IBaseRepository<Entities.Product>
    {
        Task<SearchDTO<ProductPagedSearchDTO>> PagedSearch(
            string search,
            EProductSearchType SearchType,
            int limit,
            int offset);
    }
}
