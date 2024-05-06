using BuildWise.Interfaces.Repository;
using BuildWise.Pages;
using BuildWise.Payload.Product;
using FluentValidation;
using FluentValidation.Results;

namespace BuildWise.Validator.Product
{
    public class ProductGetByIdPayloadValidator : AbstractValidator<ProductGetByIdPayload>
    {
        public ProductGetByIdPayloadValidator(IUnitOfWork uow)
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
