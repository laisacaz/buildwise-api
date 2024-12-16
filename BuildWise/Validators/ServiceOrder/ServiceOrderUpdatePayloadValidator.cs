using BuildWise.Interfaces.Repository;
using BuildWise.Payload.ServiceOrder;
using FluentValidation;

namespace BuildWise.Validator.ServiceOrder
{
    public class ServiceOrderUpdatePayloadValidator : AbstractValidator<ServiceOrderUpdatePayload>
    {
        public ServiceOrderUpdatePayloadValidator(IUnitOfWork uow)
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.GetId())
                .GreaterThan(0)
                    .WithMessage("Serviço deve existir")
                    .WithErrorCode("ME0009")

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
                    .WithErrorCode("ME0009");       

            RuleFor(x => x.Description)
                .NotEmpty()
                    .WithMessage("Informar descrição")
                    .WithErrorCode("ME0003")
                .NotNull()
                    .WithMessage("Informar descrição")
                    .WithErrorCode("ME0003")
                .MaximumLength(50)
                    .WithMessage("Descrição deve ter no máximo 60 caracteres")
                    .WithErrorCode("ME0004");      

            RuleFor(x => x.Price)
                .GreaterThan(0)
                    .WithMessage("Preço do serviço não pode ser menor que 0")
                    .WithErrorCode("ME0007");
        }
    }
}
