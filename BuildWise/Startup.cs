using BuildWise.DbConnection;
using BuildWise.ExceptionMiddleware;
using BuildWise.Installer;
using BuildWise.Interface.DbConnection;
using BuildWise.Interfaces.Repository;
using BuildWise.Interfaces.Repository.Person;
using BuildWise.Interfaces.Repository.Product;
using BuildWise.Interfaces.Service.Report;
using BuildWise.Repository;
using BuildWise.Repository.Construction;
using BuildWise.Repository.Person;
using BuildWise.Repository.Product;
using BuildWise.Repository.Sale;
using BuildWise.Repository.ServiceOrder;
using BuildWise.Services.Service.Report;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;

namespace BuildWise
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors();

            if (env.IsDevelopment()) { 

            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "BuildWiser v1");
            });
            }
            app.UseRouting();

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseEndpoints(endpoints => endpoints.MapControllers());

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers(options =>
                {

                })
                .InstallNewtonsoftjson();

            services.AddSwaggerGen(c =>
            {                
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BuildWiser", Version = "v1" });
            });
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.InstallMediatR();
            services.InstallAutoMapper();
            services.AddFluentValidationRulesToSwagger();
            DapperMapperInstaller.InstallDapperMappers();
            services.AddScoped<IBaseConnection, PostgresConnection>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<Interfaces.Repository.Construction.IConstructionRepository, ConstructionRepository>();
            services.AddScoped<IReportService, OpenFastReportService>();
            services.AddScoped<Interfaces.Repository.Sale.ISaleRepository, SaleRepository>();
            services.AddScoped<Interfaces.Repository.ServiceOrder.IServiceOrderRepository, ServiceOrderRepository>();
            services.InstallValidators();
        }
    }
}
