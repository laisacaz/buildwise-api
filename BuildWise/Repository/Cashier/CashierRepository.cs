using BuildWise.Interfaces.Repository.Cashier;
using BuildWise.Interfaces.Repository.Construction;
using static BuildWise.Interface.DbConnection.IBaseConnection;

namespace BuildWise.Repository.Cashier
{
    public class CashierRepository : BaseRepository<Entities.Cashier>, ICashierRepository
    {
        private readonly IClientConnection _conn;
        public CashierRepository(IClientConnection conn) : base(conn.GetConnection(), conn.GetTransaction())
        {
            _conn = conn;
        }
    }
}
