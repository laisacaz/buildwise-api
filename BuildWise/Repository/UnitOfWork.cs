﻿using BuildWise.Interface.DbConnection;
using BuildWise.Interfaces.Repository;
using BuildWise.Interfaces.Repository.Cashier;
using BuildWise.Interfaces.Repository.Construction;
using BuildWise.Interfaces.Repository.Person;
using BuildWise.Interfaces.Repository.Product;
using BuildWise.Interfaces.Repository.Sale;
using BuildWise.Interfaces.Repository.ServiceOrder;
using static BuildWise.Interface.DbConnection.IBaseConnection;

namespace BuildWise.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IClientConnection _conn;
        private readonly IServiceProvider _serviceProvider;
        public UnitOfWork(
            IClientConnection conn, 
            IServiceProvider serviceProvider)
        {
            _conn = conn;
            _serviceProvider = serviceProvider;
        }

        public IProductRepository Product => _serviceProvider.GetRequiredService<IProductRepository>();
        public IPersonRepository Person => _serviceProvider.GetRequiredService<IPersonRepository>();
        public IConstructionRepository Construction => _serviceProvider.GetRequiredService<IConstructionRepository>();
        public ISaleRepository Sale => _serviceProvider.GetRequiredService<ISaleRepository>();
        public IServiceOrderRepository ServiceOrder => _serviceProvider.GetRequiredService<IServiceOrderRepository>();
        public ICashierRepository Cashier => _serviceProvider.GetRequiredService<ICashierRepository>();

        public IClientConnection Conn => _conn;
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
