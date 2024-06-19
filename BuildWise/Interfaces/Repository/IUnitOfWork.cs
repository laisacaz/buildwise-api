using BuildWise.Interface.DbConnection;
using BuildWise.Interfaces.Repository.Cashier;
using BuildWise.Interfaces.Repository.Construction;
using BuildWise.Interfaces.Repository.Person;
using BuildWise.Interfaces.Repository.Product;
using BuildWise.Interfaces.Repository.Sale;
using BuildWise.Interfaces.Repository.ServiceOrder;
using static BuildWise.Interface.DbConnection.IBaseConnection;

namespace BuildWise.Interfaces.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }
        IPersonRepository Person { get; }
        IConstructionRepository Construction { get; }
        ISaleRepository Sale { get; }
        IServiceOrderRepository ServiceOrder { get; }
        ICashierRepository Cashier { get; }
        IClientConnection Conn { get; }
    }
}
