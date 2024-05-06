using BuildWise.Consts;
using BuildWise.Interface.DbConnection;
using Npgsql;
using System.Data;

namespace BuildWise.DbConnection
{
    public class PostgresConnection : IBaseConnection
    {
        protected NpgsqlTransaction _transaction;
        private readonly NpgsqlConnection _conn;

        public PostgresConnection(IConfiguration configuration)
        {
            string connstring = configuration.GetConnectionString(ConnectionStrings.Postgres);            
            string SearchPath = "Search Path = client";
            string url = $"{connstring}; {SearchPath}; Timezone = UTC; Include Error Detail = true";
            _conn = new NpgsqlConnection(url);
        }     

        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }

            await _conn.CloseAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); 
        }

        public IDbConnection GetConnection()
        {
            return _conn;
        }

        public IDbTransaction GetTransaction()
        {
            return _transaction;
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }

            await _conn.CloseAsync();
        }

        public async Task StartTransactionAsync()
        {
            await _conn.OpenAsync();
            _transaction = await _conn.BeginTransactionAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            _conn.Close();
            _conn.Dispose();
            if (_transaction != null)
            {
                _transaction.Dispose();
            }
        }
    }
}
