using BuildWise.DTO;
using BuildWise.DTO.Construction;
using BuildWise.DTO.Person;
using BuildWise.Extensions;
using BuildWise.Interface.DbConnection;
using BuildWise.Interfaces.Repository;
using BuildWise.Interfaces.Repository.Construction;
using Dapper;
using System.Drawing.Drawing2D;
using static BuildWise.Enum.ConstructionEnum;
using static Dapper.SqlMapper;

namespace BuildWise.Repository.Construction
{
    public class ConstructionRepository : BaseRepository<Entities.Construction>, IConstructionRepository
    {
        private readonly IBaseConnection _conn;

        public ConstructionRepository(IBaseConnection conn) : base(conn.GetConnection(), conn.GetTransaction())
        {
            _conn = conn;
        }
        public async Task<SearchDTO<ConstructionPagedSearchDTO>> PagedSearch(
            string search,
            EConstructionSearchType? searchType,
            int? id,
            EStatusConstruction? status,
            int limit,
            int offset)
        {
            DynamicParameters parameters = new();

            string columnSql = @"Select 
                                    client.con_construction.id as Id,
                                    client.con_construction.created_at as CreatedAt ,
                                    client.con_construction.status as Status,
                                    client.pe_person.name as ClientName
                                from client.con_construction";

            string whereSql = @"join client.pe_person on client.con_construction.id_client = client.pe_person.id
                                where client.con_construction.status <> @statusCanceled";
            parameters.Add("statusCanceled", EStatusConstruction.Canceled);
            // parameters.Add("startRegistrationDate", startRegistrationDate);
            // parameters.Add("endRegistrationDate", endRegistrationDate);

            if (EConstructionSearchType.ClientName == searchType && search.IsFill())
            {
                whereSql += " and public.unaccent(client.pe_person.name) ILIKE public.unaccent(CONCAT('%', @search, '%'))";                        
                parameters.Add("search", search);                                
            }

            if(EConstructionSearchType.Id == searchType && id > 0)
            {
                whereSql += " and client.con_construction.id = @id";
                parameters.Add("id", id);
            }

            if (status.HasValue)
            {
                whereSql += " and client.con_construction.status = @status";
                parameters.Add("status", status);

            }

            string pagedSearchSql = @$"{columnSql} {whereSql} limit {limit} offset {offset}";
            string selectRecordCount = $"select count(client.con_construction.id) from client.con_construction {whereSql}";

            GridReader? results = await _conn.GetConnection().QueryMultipleAsync($"{pagedSearchSql}; {selectRecordCount}",
               parameters);
            List<ConstructionPagedSearchDTO> data = results.Read<ConstructionPagedSearchDTO>().ToList();
            int totalRecordCount = results.ReadFirst<int>();


            return new SearchDTO<ConstructionPagedSearchDTO>(data, totalRecordCount);
        }
    }
}
