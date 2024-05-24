using BuildWise.DTO.Product;
using BuildWise.DTO.Report.Product;
using BuildWise.Entities;
using BuildWise.Interface.DbConnection;
using BuildWise.Interfaces.Repository;
using BuildWise.Interfaces.Repository.Construction;
using Dapper;
using FastReport;
using static BuildWise.Enum.SaleEnum;
using static BuildWise.Interface.DbConnection.IBaseConnection;
using static Dapper.SqlMapper;

namespace BuildWise.Repository.Sale
{
    public class SaleProductRepository : BaseRepository<SaleProduct>, ISaleProductRepository
    {
        private readonly IClientConnection _conn;
        private readonly IUnitOfWork _uow;
        public SaleProductRepository(IClientConnection conn,
            IUnitOfWork uow) : base(conn.GetConnection(), conn.GetTransaction())
        {
            _conn = conn;
            _uow = uow;
        }

        public async Task<List<SaleProductDTO>> GetSaleFullProducts(ESaleStatus status, int saleId)
        {
            DynamicParameters parameters = new DynamicParameters();

            string sql = @"Select 
                                client.sale_product.id as Id,
                                client.pro_product.description as Description,
                                client.pro_product.reference as Reference,
                                client.pro_product.barcode as BarCode,
                                client.pro_product.price as Price,
                                client.sale_product.stock_quantity as StockQuantitySale,
                                client.pro_product.id as ProductId
                            from client.sale_product
                            join client.pro_product on pro_product.id = sale_product.id_product
                            where client.sale_product.id_sale = @saleId";

            if(status is not ESaleStatus.Finalized)
            {
                sql += " and client.pro_product.status = true";
            }
            parameters.Add("saleId", saleId);

            GridReader? results = await _conn.GetConnection().QueryMultipleAsync(
                sql,
               parameters);
            List<SaleProductDTO> data = results.Read<SaleProductDTO>().ToList();

            return data;
        }
        public async Task<List<ProductRankingDTO>> GetRankingProducts()
        {
           
            string sql = @"Select 
                                sum(client.sale_product.stock_quantity) AS SaledTimes,
                                client.pro_product.description as Description,
                                client.pro_product.reference as Reference,
                                client.pro_product.barcode as BarCode,
                                client.pro_product.price as Price,
                                client.pro_product.cost as Cost,
                                client.pro_product.id as ProductId
                            from client.sale_product
                            join client.pro_product on pro_product.id = client.sale_product.id_product
                            join client.sale on sale_product.id_sale  = client.sale.id  
                            where client.sale.status <> 1
                            and client.pro_product.status = true
                            group by (client.pro_product.id, client.pro_product.description,
                            client.pro_product.barcode, client.pro_product.reference, client.pro_product.price)
                            order by(SaledTimes) desc";
            

            GridReader? results = await _conn.GetConnection().QueryMultipleAsync(
                sql);
            List<ProductRankingDTO> data = results.Read<ProductRankingDTO>().ToList();

            return data;
        }
    }
}
