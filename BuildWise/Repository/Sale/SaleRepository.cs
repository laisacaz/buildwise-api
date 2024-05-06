using BuildWise.DTO;
using BuildWise.DTO.Construction;
using BuildWise.DTO.Person;
using BuildWise.DTO.Report.Sale;
using BuildWise.DTO.Sale;
using BuildWise.Extensions;
using BuildWise.Interface.DbConnection;
using BuildWise.Interfaces.Repository;
using BuildWise.Interfaces.Repository.Construction;
using BuildWise.Interfaces.Repository.Sale;
using BuildWise.Repository.Construction;
using Dapper;
using static BuildWise.Enum.ConstructionEnum;
using static BuildWise.Enum.SaleEnum;
using static Dapper.SqlMapper;

namespace BuildWise.Repository.Sale
{
    public class SaleRepository : BaseRepository<Entities.Sale>, ISaleRepository
    {
        private readonly IBaseConnection _conn;
        private readonly IUnitOfWork _uow;

        public SaleRepository(
            IBaseConnection conn,
             IUnitOfWork uow) : base(conn.GetConnection(), conn.GetTransaction())
        {
            _conn = conn;
            _uow = uow;
        }
        public ISaleProductRepository Product => new SaleProductRepository(_conn, _uow);

        public async Task<SearchDTO<SalePagedSearchDTO>> PagedSearch(
            string? search,
            ESaleSearchType? searchType,
            int? id,
            ESaleStatus? status,
            int limit,
            int offset)
        {
            DynamicParameters parameters = new();

            string columnSql = @"Select 
                                    client.sale.id as Id,
                                    client.sale.created_at as CreatedAt,
                                    client.sale.status as Status,
                                    client.sale.total as Total,
                                    client.pe_person.name as ClientName
                                from client.sale";

            string whereSql = @" 
                                join client.pe_person on client.sale.id_client = client.pe_person.id 
                                where client.sale.status <> @statusCanceled";

            parameters.Add("statusCanceled", ESaleStatus.Canceled);
            //    parameters.Add("startRegistrationDate", startRegistrationDate);
            // parameters.Add("endRegistrationDate", endRegistrationDate);

            if (ESaleSearchType.ClientName == searchType && search.IsFill())
            {
                whereSql += " and public.unaccent(client.pe_person.name) ILIKE public.unaccent(CONCAT('%', @search, '%'))";
                parameters.Add("search", search);
            }

            if (ESaleSearchType.Id == searchType && id > 0)
            {
                whereSql += " and client.sale.id = @id";
                parameters.Add("id", id);
            }
            if (status.HasValue)
            {
                whereSql += " and client.sale.status = @status";
                parameters.Add("status", status);
            }

            string pagedSearchSql = @$"{columnSql} {whereSql} limit {limit} offset {offset}";
            string selectRecordCount = $"select count(client.sale.id) from client.sale {whereSql}";

            GridReader? results = await _conn.GetConnection().QueryMultipleAsync($"{pagedSearchSql}; {selectRecordCount}",
               parameters);
            List<SalePagedSearchDTO> data = results.Read<SalePagedSearchDTO>().ToList();
            int totalRecordCount = results.ReadFirst<int>();


            return new SearchDTO<SalePagedSearchDTO>(data, totalRecordCount);
        }

        public async Task<List<SaleByDateDTO>> GetAllByDate(
            DateTime startDate,
            DateTime endDate)
        {
            DynamicParameters parameters = new DynamicParameters();

            string sql = @" Select
                                client.sale.id as Id,
                                client.sale.id_client as ClientId,
                                client.sale.id_seller as SellerId,
                                client.sale.total as Total,
                                client.sale.total_items as TotalProducts,
                                client.sale.created_at as CreatedAt,

                                c.id as Id,
                                c.name as Name,

                                s.id as Id,
                                s.name as Name

                            from client.sale
                            join client.pe_person c on c.id = client.sale.id_client
                            join client.pe_person s on s.id = client.sale.id_seller

                            where client.sale.created_at between @startDate and @endDate
                            and client.sale.status <> @statusCanceled";

            parameters.Add("startDate", startDate);
            parameters.Add("endDate", endDate);
            parameters.Add("statusCanceled", ESaleStatus.Canceled);

            IEnumerable<SaleByDateDTO> dto = await _conn.GetConnection().QueryAsync<SaleByDateDTO, PersonSimpleDTO, PersonSimpleDTO, SaleByDateDTO>(
               sql,
               map: (saleDTO, clientDTO, sellerDTO) =>
               {
                   saleDTO.Client = clientDTO;
                   saleDTO.Seller = sellerDTO;
                   
                   return saleDTO;
               },
               parameters,
               splitOn: "id,id,id"
               );

            return dto.ToList();
        }
    }
}
