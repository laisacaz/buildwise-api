using BuildWise.DTO.Product;
using BuildWise.DTO.Report.Service;
using BuildWise.DTO.Sale;
using BuildWise.Entities;
using BuildWise.Interface.Repository;
using static BuildWise.Enum.SaleEnum;

namespace BuildWise.Interfaces.Repository.Sale
{
    public interface ISaleServiceOrderRepository : IBaseRepository<SaleServiceOrder>
    {
        Task<List<SaleServiceOrderDTO>> GetSaleFullServiceOrders(ESaleStatus status, int saleId);
        Task<List<ServiceRankingDTO>> GetRankingServices();
    }
}
