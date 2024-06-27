using BuildWise.AutoMapper.Person;
using BuildWise.AutoMapper.Product;
using BuildWise.Payload.Construction;
using BuildWise.Payload.Person;
using BuildWise.Payload.Product;
using BuildWise.Payload.Report;
using BuildWise.Payload.Sale;
using BuildWise.Payload.ServiceOrder;
using BuildWise.Payload.User;
using BuildWise.Services.Command.Product;
using BuildWise.Validator.Construction;
using BuildWise.Validator.Person;
using BuildWise.Validator.Product;
using BuildWise.Validator.Report;
using BuildWise.Validator.Sale;
using BuildWise.Validator.ServiceOrder;
using BuildWise.Validator.User;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace BuildWise.Installer
{
    public static class FluentValidationInstaller
    {
        public static void InstallValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<PersonInsertPayload>, PersonInsertPayloadValidator>();
            services.AddScoped<IValidator<PersonUpdatePayload>, PersonUpdatePayloadValidator>();
            services.AddScoped<IValidator<PersonGetByIdPayload>, PersonGetByIdPayloadValidator>();
            services.AddScoped<IValidator<PersonDeletePayload>, PersonDeletePayloadValidator>();
            services.AddScoped<IValidator<ProductInsertPayload>, ProductInsertPayloadValidator>();
            services.AddScoped<IValidator<ProductUpdatePayload>, ProductUpdatePayloadValidator>();
            services.AddScoped<IValidator<ProductGetByIdPayload>, ProductGetByIdPayloadValidator>();
            services.AddScoped<IValidator<ProductDeletePayload>, ProductDeletePayloadValidator>();
            services.AddScoped<IValidator<ConstructionInsertPayload>, ConstructionInsertPayloadValidator>();
            services.AddScoped<IValidator<ConstructionUpdatePayload>, ConstructionUpdatePayloadValidator>();
            services.AddScoped<IValidator<ConstructionGetByIdPayload>, ConstructionGetByIdPayloadValidator>();
            services.AddScoped<IValidator<ConstructionDeletePayload>, ConstructionDeletePayloadValidator>();
            services.AddScoped<IValidator<ConstructionFinishPayload>, ConstructionFinishPayloadValidator>();
            services.AddScoped<IValidator<SaleInsertPayload>, SaleInsertPayloadValidator>();
            services.AddScoped<IValidator<SaleUpdatePayload>, SaleUpdatePayloadValidator>();
            services.AddScoped<IValidator<SaleGetByIdPayload>, SaleGetByIdPayloadValidator>();
            services.AddScoped<IValidator<SaleDeletePayload>, SaleDeletePayloadValidator>();
            services.AddScoped<IValidator<SaleFinishPayload>, SaleFinishPayloadValidator>();            
            services.AddScoped<IValidator<SaleProductInsertPayload>, SaleProductInsertPayloadValidator>();            
            services.AddScoped<IValidator<SaleServiceOrderInsertPayload>, SaleServiceOrderInsertPayloadValidator>();            
            services.AddScoped<IValidator<SaleProductUpdatePayload>, SaleProductUpdatePayloadValidator>();          
            services.AddScoped<IValidator<SaleServiceOrderUpdatePayload>, SaleServiceOrderUpdatePayloadValidator>();           
            services.AddScoped<IValidator<ServiceOrderInsertPayload>, ServiceOrderInsertPayloadValidator>();          
            services.AddScoped<IValidator<ServiceOrderUpdatePayload>, ServiceOrderUpdatePayloadValidator>();          
            services.AddScoped<IValidator<ServiceOrderGetByIdPayload>, ServiceOrderGetByIdPayloadValidator>();          
            services.AddScoped<IValidator<ServiceOrderDeletePayload>, ServiceOrderDeletePayloadValidator>();          
            services.AddScoped<IValidator<UserInsertPayload>, UserInsertPayloadValidator>();            
        }
    }
}
