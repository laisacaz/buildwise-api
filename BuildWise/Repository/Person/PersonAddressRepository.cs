using BuildWise.Entities;
using BuildWise.Interface.DbConnection;
using BuildWise.Interfaces.Repository;
using BuildWise.Interfaces.Repository.Person;
using Dapper;
using System.Data;
using static BuildWise.Interface.DbConnection.IBaseConnection;

namespace BuildWise.Repository.Person
{
    public class PersonAddressRepository : BaseRepository<Address>, IPersonAddressRepository
    {
        private readonly IClientConnection _conn;
        private readonly IUnitOfWork _uow;
        public PersonAddressRepository(IClientConnection conn,
            IUnitOfWork uow) : base(conn.GetConnection(), conn.GetTransaction())
        {
            _conn = conn;
            _uow = uow;
        }

    }
}
