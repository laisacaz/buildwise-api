using BuildWise.Consts;
using BuildWise.Interface.DbConnection;
using Npgsql;
using System.Data;
using static BuildWise.Interface.DbConnection.IBaseConnection;

namespace BuildWise.DbConnection
{
    public class PGClientConnection : IClientConnection
    {
        protected NpgsqlTransaction _transaction;
        private readonly NpgsqlConnection _conn;

        public PGClientConnection(string connstring)
        {
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

    public class PGPublicConnection : IPublicConnection
    {
        protected NpgsqlTransaction _transaction;
        private readonly NpgsqlConnection _conn;

        public PGPublicConnection(string connstring)
        {
            string SearchPath = "Search Path = public";
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
            GC.SuppressFinalize(this); //https://learn.microsoft.com/pt-br/dotnet/fundamentals/code-analysis/quality-rules/ca1816
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
