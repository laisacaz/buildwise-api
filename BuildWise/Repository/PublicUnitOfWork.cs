using System.Drawing;
using static BuildWise.Interface.DbConnection.IBaseConnection;

namespace BuildWise.Repository
{
    public class IPublicUnitOfWork
    {
        private readonly IPublicConnection _conn;
        private readonly IServiceProvider _serviceProvider;

        public IPublicUnitOfWork(
            IPublicConnection conn,
            IServiceProvider serviceProvider)
        {
            _conn = conn;
            _serviceProvider = serviceProvider;
        }
        public IPublicConnection Conn => _conn;
    }
}
