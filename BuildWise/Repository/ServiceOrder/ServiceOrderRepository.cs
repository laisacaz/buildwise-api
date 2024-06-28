using BuildWise.DTO.Product;
using BuildWise.DTO;
using BuildWise.Extensions;
using BuildWise.Interface.DbConnection;
using BuildWise.Interfaces.Repository;
using BuildWise.Interfaces.Repository.Sale;
using BuildWise.Interfaces.Repository.ServiceOrder;
using Dapper;
using static BuildWise.Enum.ProductEnum;
using static Dapper.SqlMapper;
using BuildWise.DTO.ServiceOrder;
using static BuildWise.Interface.DbConnection.IBaseConnection;

namespace BuildWise.Repository.ServiceOrder
{
    public class ServiceOrderRepository : BaseRepository<Entities.ServiceOrder>, IServiceOrderRepository
    {
        private readonly IClientConnection _conn;

        public ServiceOrderRepository(
            IClientConnection conn) : base(conn.GetConnection(), conn.GetTransaction())
        {
            _conn = conn;
        }
        public async Task<SearchDTO<ServiceOrderPagedSearchDTO>> PagedSearch(
            string search,
            int limit,
            int offset)
        {
            DynamicParameters parameters = new();

            string columnSql = @"Select 
                                    client.se_service.id as Id,
                                    client.se_service.description as Description,                                                                        
                                    client.se_service.price as Price        
                                from client.se_service";

            string whereSql = @"where client.se_service.status = true";

            if (search.IsFill())
            {
                whereSql += " and public.unaccent(client.se_service.description) ILIKE public.unaccent(CONCAT('%', @search, '%'))";
                parameters.Add("search", search);
            }
            string orderBy = "order by  client.se_service.id desc";
            {

                string pagedSearchSql = @$"{columnSql} {whereSql} {orderBy} limit {limit} offset {offset}";
                string selectRecordCount = $"select count(id) from client.se_service {whereSql}";

                GridReader? results = await _conn.GetConnection().QueryMultipleAsync($"{pagedSearchSql}; {selectRecordCount}",
                parameters);
                List<ServiceOrderPagedSearchDTO> data = results.Read<ServiceOrderPagedSearchDTO>().ToList();
                int totalRecordCount = results.ReadFirst<int>();

                return new SearchDTO<ServiceOrderPagedSearchDTO>(data, totalRecordCount);
            }
        }
    }
}
