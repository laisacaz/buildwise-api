using BuildWise.DTO;
using BuildWise.DTO.Person;
using BuildWise.DTO.Product;
using BuildWise.Extensions;
using BuildWise.Interface.DbConnection;
using BuildWise.Interfaces.Repository;
using BuildWise.Interfaces.Repository.Person;
using BuildWise.Interfaces.Repository.Product;
using Dapper;
using System.Collections.Generic;
using static BuildWise.Enums.PersonEnum;
using static BuildWise.Interface.DbConnection.IBaseConnection;
using static Dapper.SqlMapper;

namespace BuildWise.Repository.Person
{
    public class PersonRepository : BaseRepository<Entities.Person>, IPersonRepository
    {
        private readonly IClientConnection _conn;
        private readonly IUnitOfWork _uow;

        public PersonRepository(IClientConnection conn,
             IUnitOfWork uow) : base(conn.GetConnection(), conn.GetTransaction())
        {
            _conn = conn;
            _uow = uow;
        }
        public IPersonAddressRepository Address => new PersonAddressRepository(_conn, _uow);

        public async Task<SearchDTO<PersonPagedSearchDTO>> PagedSearch(
           string search,
           EPersonSearchType SearchType,
           int? id,
           int limit,
           int offset)
        {
            DynamicParameters parameters = new();

            string columnSql = @"Select 
                                    client.pe_person.id as Id,
                                    client.pe_person.name as Name,
                                    client.pe_person.identity_number as IdentityNumber
                                from client.pe_person";
            string whereSql = @"where client.pe_person.status = true";

            if (EPersonSearchType.Name == SearchType && search.IsFill()) 
            { 
                whereSql += " and public.unaccent(client.pe_person.name) ILIKE public.unaccent(CONCAT('%', @search, '%'))";
                parameters.Add("search", search);
            } 

            if(EPersonSearchType.Id == SearchType && id > 0)
            {
                whereSql += " and client.pe_person.id = @id";
                parameters.Add("id", id);
            }
            string orderBy = @"order by client.pe_person.id desc";
            string pagedSearchSql = @$"{columnSql} {whereSql} {orderBy} limit {limit} offset {offset}";
            string selectRecordCount = $"select count(id) from client.pe_person {whereSql}";

            GridReader? results = await _conn.GetConnection().QueryMultipleAsync($"{pagedSearchSql}; {selectRecordCount}",
               parameters);
            List<PersonPagedSearchDTO> data = results.Read<PersonPagedSearchDTO>().ToList();
            int totalRecordCount = results.ReadFirst<int>();

            return new SearchDTO<PersonPagedSearchDTO>(data, totalRecordCount);
        }

        public async Task<List<PersonSimpleDTO>> GetAllActivesSimple()
        {
            string sql = @" Select
                                client.pe_person.name as Name,
                                client.pe_person.id as Id
                            from client.pe_person
                            where client.pe_person.status = true";

            IEnumerable<PersonSimpleDTO>? dto = await _conn.GetConnection().QueryAsync<PersonSimpleDTO>(sql, _conn.GetTransaction());
            return dto.ToList();
        }
        public async Task<List<PersonSimpleDTO>> GetAllSimple()
        {
            string sql = @" Select
                                client.pe_person.name as Name,
                                client.pe_person.id as Id,                                
                                client.pe_person.status as Status
                            from client.pe_person";

            IEnumerable<PersonSimpleDTO>? dto = await _conn.GetConnection().QueryAsync<PersonSimpleDTO>(sql, _conn.GetTransaction());
            return dto.ToList();
        }
        public async Task<PersonSimpleDTO> GetSimple(int id)
        {
            DynamicParameters parameters = new DynamicParameters();

            string sql = @" Select
                                client.pe_person.name as Name,
                                client.pe_person.id as Id,
                                client.pe_person.status as Status
                            from client.pe_person
                            where client.pe_person.id = @id";

            parameters.Add("id", id);

            PersonSimpleDTO dto = await _conn.GetConnection().QueryFirstAsync<PersonSimpleDTO>(sql,
                parameters,
                _conn.GetTransaction());
            return dto;
        }       
    }
}
