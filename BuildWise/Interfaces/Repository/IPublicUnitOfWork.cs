using BuildWise.Interfaces.Repository.NovaPasta;
using BuildWise.Interfaces.Repository.Product;
using static BuildWise.Interface.DbConnection.IBaseConnection;

namespace BuildWise.Interfaces.Repository
{
    public interface IPublicUnitOfWork
    {
        IUserRepository User { get; }
        IPublicConnection Conn { get;  }
    }
}
