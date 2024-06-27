using BuildWise.Entities;
using BuildWise.Interfaces.Repository;
using BuildWise.Interfaces.Repository.Sale;
using static BuildWise.Interface.DbConnection.IBaseConnection;

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
    }
}
