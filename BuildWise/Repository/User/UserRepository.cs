using BuildWise.Interfaces.Repository.NovaPasta;
using static BuildWise.Interface.DbConnection.IBaseConnection;

namespace BuildWise.Repository.User
{
    public class UserRepository : BaseRepository<Entities.User>, IUserRepository
    {
        public UserRepository(IPublicConnection conn) : base(conn.GetConnection(), conn.GetTransaction())
        {
        }
    }
}
