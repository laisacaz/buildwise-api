﻿using BuildWise.Interface.DbConnection;
using BuildWise.Interfaces.Repository.Construction;
using BuildWise.Interfaces.Repository.Person;
using BuildWise.Interfaces.Repository.Product;
using BuildWise.Interfaces.Repository.Sale;
using BuildWise.Interfaces.Repository.ServiceOrder;

namespace BuildWise.Interfaces.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }
        IPersonRepository Person { get; }
        IConstructionRepository Construction { get; }
        ISaleRepository Sale { get; }
        IServiceOrderRepository ServiceOrder { get; }
        IBaseConnection Conn { get; }
    }
}
