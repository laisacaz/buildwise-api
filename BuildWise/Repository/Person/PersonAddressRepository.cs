using BuildWise.Entities;
using BuildWise.Interface.DbConnection;
using BuildWise.Interfaces.Repository;
using BuildWise.Interfaces.Repository.Person;
using Dapper;
using System.Data;

namespace BuildWise.Repository.Person
{
    public class PersonAddressRepository : BaseRepository<Address>, IPersonAddressRepository
    {
        private readonly IBaseConnection _conn;
        private readonly IUnitOfWork _uow;
        public PersonAddressRepository(IBaseConnection conn,
            IUnitOfWork uow) : base(conn.GetConnection(), conn.GetTransaction())
        {
            _conn = conn;
            _uow = uow;
        }

    }
}
