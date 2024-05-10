using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Product;
using BuildWise.Payload.ServiceOrder;
using FluentValidation;

namespace BuildWise.Validator.ServiceOrder
{
    public class ServiceOrderDeletePayloadValidator : AbstractValidator<ServiceOrderDeletePayload>
    {
        public ServiceOrderDeletePayloadValidator(IUnitOfWork uow)
        {
            RuleFor(x => x.Id)
             .GreaterThan(0)
                 .WithMessage("Serviço deve existir")
                 .WithErrorCode("ME0011")
             .MustAsync(async (payload, cod, context, cancellation) =>
             {
                 Entities.ServiceOrder serviceOrder = await uow.ServiceOrder.GetByIdAsync(cod);

                 if (serviceOrder == null)
                 {
                     return false;
                 }
                 if (serviceOrder.Status is false)
                 {
                     return false;
                 }
                 return true;
             })
                 .WithMessage("Serviço deve existir")
                 .WithErrorCode("ME0011");
        }
    }
}
