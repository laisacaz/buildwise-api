using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Sale;
using FluentValidation;

namespace BuildWise.Validator.Sale
{
    public class SaleProductUpdatePayloadValidator : AbstractValidator<SaleProductUpdatePayload>
    {
        public SaleProductUpdatePayloadValidator(IUnitOfWork uow)
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                    .WithMessage("Produto deve existir")
                    .WithErrorCode("ME0009")

                .MustAsync(async (payload, cod, context, cancellation) =>
                {
                    Entities.Product product = await uow.Product.GetByIdAsync(cod);

                    if (product == null)
                    {
                        return false;
                    }
                    if (product.Status is false)
                    {
                        return false;
                    }
                    return true;
                })
                    .WithMessage("Produto deve existir")
                    .WithErrorCode("ME0009");

            RuleFor(x => x.StockQuantity)
                .GreaterThan(0)
                    .WithMessage("Quantidade do produto deve ser maior que zero")
                    .WithErrorCode("ME0028");
        }
    }
}
