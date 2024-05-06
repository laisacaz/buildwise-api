using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Sale;
using FluentValidation;
using static BuildWise.Enum.SaleEnum;

namespace BuildWise.Validator.Sale
{
    public class SaleDeletePayloadValidator : AbstractValidator<SaleDeletePayload>
    {
        public SaleDeletePayloadValidator(IUnitOfWork uow)
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                    .WithMessage("Venda deve existir")
                    .WithErrorCode("ME0011")
                .MustAsync(async (payload, cod, context, cancellation) =>
                {
                Entities.Sale sale = await uow.Sale.GetByIdAsync(cod);

                if (sale == null)
                {
                    return false;
                }
                if (sale.Status is ESaleStatus.Canceled)
                {
                    return false;
                }
                    return true;
                })
                    .WithMessage("Venda deve existir")
                    .WithErrorCode("ME0011");
        }
    }
}
