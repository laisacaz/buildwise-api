using BuildWise.Interface.DbConnection;
using BuildWise.Interfaces.Repository;
using BuildWise.Interfaces.Repository.Construction;
using BuildWise.Interfaces.Repository.Person;
using BuildWise.Interfaces.Repository.Product;
using BuildWise.Interfaces.Repository.Sale;

namespace BuildWise.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IBaseConnection _conn;
        private readonly IServiceProvider _serviceProvider;
        public UnitOfWork(
            IBaseConnection conn, 
            IServiceProvider serviceProvider)
        {
            _conn = conn;
            _serviceProvider = serviceProvider;
        }

        public IProductRepository Product => _serviceProvider.GetRequiredService<IProductRepository>();
        public IPersonRepository Person => _serviceProvider.GetRequiredService<IPersonRepository>();
        public IConstructionRepository Construction => _serviceProvider.GetRequiredService<IConstructionRepository>();
        public ISaleRepository Sale => _serviceProvider.GetRequiredService<ISaleRepository>();

        public IBaseConnection Conn => _conn;
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
