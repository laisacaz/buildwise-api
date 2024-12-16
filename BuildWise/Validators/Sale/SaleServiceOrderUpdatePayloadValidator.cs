using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Sale;
using FluentValidation;

namespace BuildWise.Validator.Sale
{
    public class SaleServiceOrderUpdatePayloadValidator : AbstractValidator<SaleServiceOrderUpdatePayload>
    {
        public SaleServiceOrderUpdatePayloadValidator(IUnitOfWork uow)
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.ServiceId)
                .GreaterThan(0)
                    .WithMessage("Serviço deve existir")
                    .WithErrorCode("ME0009")

                .MustAsync(async (payload, cod, context, cancellation) =>
                {
                    Entities.ServiceOrder service = await uow.ServiceOrder.GetByIdAsync(cod);

                    if (service == null)
                    {
                        return false;
                    }
                    if (service.Status is false)
                    {
                        return false;
                    }
                    return true;
                })
                    .WithMessage("Serviço deve existir")
                    .WithErrorCode("ME0009");

            RuleFor(x => x.StockQuantity)
                .GreaterThan(0)
                    .WithMessage("Quantidade do serviço deve ser maior que zero")
                    .WithErrorCode("ME0028");
        }
    }
}
