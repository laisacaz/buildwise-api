using System.Data;

namespace BuildWise.Interface.DbConnection
{
    public interface IBaseConnection : IDisposable
    {
        public IDbConnection GetConnection();

        public IDbTransaction GetTransaction();

        Task CommitAsync();

        Task RollbackAsync();

        Task StartTransactionAsync();

        public interface IPublicConnection : IBaseConnection
        {
        }

        public interface IClientConnection : IBaseConnection
        {
        }
    }
}
