using BuildWise.DTO.Product;
using BuildWise.DTO.Report.Product;
using BuildWise.Entities;
using BuildWise.Interface.Repository;
using static BuildWise.Enum.SaleEnum;

namespace BuildWise.Interfaces.Repository.Construction
{
    public interface ISaleProductRepository : IBaseRepository<SaleProduct>
    {
        Task<List<SaleProductDTO>> GetSaleFullProducts(ESaleStatus status, int saleId);
        Task<List<ProductRankingDTO>> GetRankingProducts();
    }
}
