using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Product;
using FluentValidation;

namespace BuildWise.Validator.Product
{
    public class ProductDeletePayloadValidator : AbstractValidator<ProductDeletePayload>
    {
        public ProductDeletePayloadValidator(IUnitOfWork uow)
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                    .WithMessage("Produto deve existir")
                    .WithErrorCode("ME0011")
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
                    .WithErrorCode("ME0011");
        }
    }
}
