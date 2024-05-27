using BuildWise.Interfaces.Repository;
using BuildWise.Interfaces.Repository.User;
using BuildWise.Interfaces.Repository.Product;
using System.Drawing;
using static BuildWise.Interface.DbConnection.IBaseConnection;

namespace BuildWise.Repository
{
    public class PublicUnitOfWork : IPublicUnitOfWork
    {
        private readonly IPublicConnection _conn;
        private readonly IServiceProvider _serviceProvider;

        public PublicUnitOfWork(
            IPublicConnection conn,
            IServiceProvider serviceProvider)
        {
            _conn = conn;
            _serviceProvider = serviceProvider;
        }
        public IUserRepository User => _serviceProvider.GetRequiredService<IUserRepository>();
        public IPublicConnection Conn => _conn;
    }
}
