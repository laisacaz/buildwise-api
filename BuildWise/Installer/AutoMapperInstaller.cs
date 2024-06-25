using BuildWise.AutoMapper.Cashier;
using BuildWise.AutoMapper.Construction;
using BuildWise.AutoMapper.Person;
using BuildWise.AutoMapper.Product;
using BuildWise.AutoMapper.Sale;
using BuildWise.AutoMapper.ServiceOrder;
using BuildWise.AutoMapper.User;
using BuildWise.Services.Command.Product;

namespace BuildWise.Installer
{
    public static class AutoMapperInstaller
    {
        public static void InstallAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProductInsertCommand));
            services.AddAutoMapper(typeof(ProductMap));
            services.AddAutoMapper(typeof(PersonMap));
            services.AddAutoMapper(typeof(ConstructionMap));
            services.AddAutoMapper(typeof(SaleMap));
            services.AddAutoMapper(typeof(ServiceOrderMap));
            services.AddAutoMapper(typeof(UserMap));
            services.AddAutoMapper(typeof(CashierMap));
        }
    }
}
