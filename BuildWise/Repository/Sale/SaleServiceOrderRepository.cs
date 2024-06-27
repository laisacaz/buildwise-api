using BuildWise.DTO.Product;
using BuildWise.DTO.Report.Product;
using BuildWise.DTO.Report.Service;
using BuildWise.DTO.Sale;
using BuildWise.Entities;
using BuildWise.Interfaces.Repository;
using BuildWise.Interfaces.Repository.Sale;
using Dapper;
using static BuildWise.Enum.SaleEnum;
using static BuildWise.Interface.DbConnection.IBaseConnection;
using static Dapper.SqlMapper;

namespace BuildWise.Repository.Sale
{
    public class SaleServiceOrderRepository :  BaseRepository<SaleServiceOrder>, ISaleServiceOrderRepository
    {
        private readonly IClientConnection _conn;
        public SaleServiceOrderRepository(
            IClientConnection conn,
            IUnitOfWork uow) : base(conn.GetConnection(), conn.GetTransaction())
        {
            _conn = conn;
        }
        public async Task<List<SaleServiceOrderDTO>> GetSaleFullServiceOrders(ESaleStatus status, int saleId)
        {
            DynamicParameters parameters = new DynamicParameters();

            string sql = @"Select 
                                client.sale_service.id as Id,
                                client.se_service.description as Description,
                                client.se_service.price as Price,
                                client.sale_service.stock_quantity as StockQuantitySale,
                                client.se_service.id as ServiceId
                            from client.sale_service
                            join client.se_service on se_service.id = sale_service.id_service
                            where client.sale_service.id_sale = @saleId";

            if (status is not ESaleStatus.Finalized)
            {
                sql += " and client.se_service.status = true";
            }
            parameters.Add("saleId", saleId);

            GridReader? results = await _conn.GetConnection().QueryMultipleAsync(
                sql,
               parameters);
            List<SaleServiceOrderDTO> data = results.Read<SaleServiceOrderDTO>().ToList();

            return data;
        }

        public async Task<List<ServiceRankingDTO>> GetRankingServices()
        {

            string sql = @"Select 
                                sum(client.sale_service.stock_quantity) AS SaledTimes,
                                client.se_service.description as Description,
                                client.se_service.price as Price,
                                client.se_service.id as ServiceId
                            from client.sale_service
                            join client.se_service on se_service.id = client.sale_service.id_service
                            join client.sale on sale_service.id_sale  = client.sale.id  
                            where client.sale.status <> 1
                            and client.se_service.status = true
                            group by (client.se_service.id, client.se_service.description, client.se_service.price)
                            order by(SaledTimes) desc";


            GridReader? results = await _conn.GetConnection().QueryMultipleAsync(
                sql);
            List<ServiceRankingDTO> data = results.Read<ServiceRankingDTO>().ToList();

            return data;
        }
    }
}
