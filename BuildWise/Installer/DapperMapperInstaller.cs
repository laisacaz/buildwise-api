using BuildWise.DbMapper.Construction;
using BuildWise.DbMapper.Person;
using BuildWise.DbMapper.Sale;
using BuildWise.DbMapper.Service;
using BuildWise.DbMapper.User;
using BuildWise.Mapper.Product;
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;

namespace BuildWise.Installer
{
    public static class DapperMapperInstaller
    {
        public static void InstallDapperMappers()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new ProductMapper());
                config.AddMap(new PersonMapper());
                config.AddMap(new AddressMapper());
                config.AddMap(new ConstructionMapper());
                config.AddMap(new SaleProductMapper());
                config.AddMap(new SaleMapper());
                config.AddMap(new ServiceOrderMapper());
                config.AddMap(new UserMapper());
                config.ForDommel();
            });

        }
    }
}
