
using BuildWise.Provider;

namespace BuildWise.Installer
{
    public static class NewtonsoftjsonInstaller
    {
        public static IMvcBuilder InstallNewtonsoftjson(this IMvcBuilder services)
        {
            services
               .AddNewtonsoftJson(options =>
               {
                   Newtonsoft.Json.JsonSerializerSettings settings = options.SerializerSettings;
                   JsonProvider.SetNewtonSoft(ref settings);
               });

            return services;
        }
    }
}
