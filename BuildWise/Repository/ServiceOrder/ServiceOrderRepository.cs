using BuildWise.Interface.DbConnection;
using BuildWise.Interfaces.Repository;
using BuildWise.Interfaces.Repository.Sale;
using BuildWise.Interfaces.Repository.ServiceOrder;

namespace BuildWise.Repository.ServiceOrder
{
    public class ServiceOrderRepository : BaseRepository<Entities.ServiceOrder>, IServiceOrderRepository
    {
        public ServiceOrderRepository(
            IBaseConnection conn,
            IUnitOfWork uow) : base(conn.GetConnection(), conn.GetTransaction())
        {
            
        }
    }
}
