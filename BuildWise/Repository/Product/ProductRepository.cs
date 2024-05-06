using BuildWise.DTO;
using BuildWise.DTO.Product;
using BuildWise.Extensions;
using BuildWise.Interface.DbConnection;
using BuildWise.Interfaces.Repository;
using BuildWise.Interfaces.Repository.Product;
using Dapper;
using static BuildWise.Enum.ProductEnum;
using static Dapper.SqlMapper;

namespace BuildWise.Repository.Product
{
    public class ProductRepository : BaseRepository<Entities.Product>, IProductRepository
    {
        private readonly IBaseConnection _conn;

        public ProductRepository(IBaseConnection conn) : base(conn.GetConnection(), conn.GetTransaction())
        {
            _conn = conn;
        }

        public async Task<SearchDTO<ProductPagedSearchDTO>> PagedSearch(
            string search,
            EProductSearchType SearchType,
            int limit,
            int offset)
        {
            DynamicParameters parameters = new();

            string columnSql = @"Select 
                                    client.pro_product.id as Id,
                                    client.pro_product.description as Description,
                                    client.pro_product.reference as Reference,
                                    client.pro_product.stock_quantity as StockQuantity,
                                    client.pro_product.price as Price        
                                from client.pro_product";

            string whereSql = @"where client.pro_product.status = true";


            if (EProductSearchType.Reference == SearchType && search.IsFill())
            {
                whereSql += " and public.unaccent(client.pro_product.reference) ILIKE public.unaccent(CONCAT('%', @search, '%'))";
                parameters.Add("search", search);
            }
            if (EProductSearchType.Description == SearchType && search.IsFill())
            {
                whereSql += " and public.unaccent(client.pro_product.description) ILIKE public.unaccent(CONCAT('%', @search, '%'))";
                parameters.Add("search", search);
            }
            {

                string pagedSearchSql = @$"{columnSql} {whereSql} limit {limit} offset {offset}";
                string selectRecordCount = $"select count(id) from client.pro_product {whereSql}";

                GridReader? results = await _conn.GetConnection().QueryMultipleAsync($"{pagedSearchSql}; {selectRecordCount}",
                parameters);
                List<ProductPagedSearchDTO> data = results.Read<ProductPagedSearchDTO>().ToList();
                int totalRecordCount = results.ReadFirst<int>();

                return new SearchDTO<ProductPagedSearchDTO>(data, totalRecordCount);
            }
        }
    }
}
