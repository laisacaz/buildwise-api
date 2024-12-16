using BuildWise.Services.Command.Person;
using BuildWise.Services.Command.Product;
using MediatR;
using MediatR.Registration;
using System.Reflection;

namespace BuildWise.Installer
{
    public static class MediatRInstaller
    {
        public static void InstallMediatR(this IServiceCollection services)
        {
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(typeof(ProductInsertCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(PersonInsertCommand).Assembly);
            });
        }
    }
}
