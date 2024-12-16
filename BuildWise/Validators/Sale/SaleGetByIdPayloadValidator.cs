using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Sale;
using FluentValidation;
using FluentValidation.Results;

namespace BuildWise.Validator.Sale
{
    public class SaleGetByIdPayloadValidator : AbstractValidator<SaleGetByIdPayload>
    {
        public SaleGetByIdPayloadValidator(IUnitOfWork uow)
        {
            RuleFor(x => x.Id)
              .GreaterThan(0)
                  .WithMessage("Venda deve existir")
                  .WithErrorCode("ME0030")
              .MustAsync(async (payload, cod, context, cancellation) =>
              {
                  Entities.Sale sale = await uow.Sale.GetByIdAsync(cod);

                  if (sale == null)
                  {
                      return false;
                  }
                  return true;
              })
                .WithMessage("Venda deve existir")
                .WithErrorCode("ME0030"); 
        }
    }
}
