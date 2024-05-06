using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Sale;
using BuildWise.Repository;
using BuildWise.Validator.Product;
using FluentValidation;

namespace BuildWise.Validator.Sale
{
    public class SaleInsertPayloadValidator : AbstractValidator<SaleInsertPayload>
    {
        public SaleInsertPayloadValidator(IUnitOfWork uow)
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.ClientId)
               .GreaterThan(0)
                   .WithMessage("Cliente é obrigatório")
                   .WithErrorCode("ME0023")

                .MustAsync(async (payload, cod, context, cancellation) =>
                {
                    Entities.Person person = await uow.Person.GetByIdAsync(cod);

                    if (person == null)
                    {
                        return false;
                    }
                    if (person.Status is false)
                    {
                        return false;
                    }
                    return true;
                })
                  .WithMessage("Cliente deve existir")
                  .WithErrorCode("ME0023");

            RuleFor(x => x.SellerId)
               .GreaterThan(0)
                   .WithMessage("Vendedor é obrigatório")
                   .WithErrorCode("ME0026")

                .MustAsync(async (payload, cod, context, cancellation) =>
                {
                    Entities.Person person = await uow.Person.GetByIdAsync(cod);

                    if (person == null)
                    {
                        return false;
                    }
                    if (person.Status is false)
                    {
                        return false;
                    }
                    return true;
                })
                  .WithMessage("Vendedor deve existir")
                  .WithErrorCode("ME0026");

            RuleFor(x => x.ConstructionId)
               .GreaterThanOrEqualTo(0)
                   .WithMessage("Construção deve existir")
                   .WithErrorCode("ME0027")
                .MustAsync(async (payload, cod, context, cancellation) =>
                {
                    Entities.Construction construction = await uow.Construction.GetByIdAsync(cod);

                    if (construction == null)
                    {
                        return false;
                    }                    
                    return true;
                })
                  .WithMessage("Construção deve existir")
                  .WithErrorCode("ME0027")
                .When(x => x.ConstructionId > 0);

            RuleFor(x => x.Products)
                .Must(products => products != null && products.Count > 0)
                    .WithMessage("Você deve inserir no mínimo um produto para salvar a venda")
                    .WithErrorCode("ME0028");

            RuleForEach(x => x.Products)
                .SetValidator(new SaleProductInsertPayloadValidator(uow));

            RuleFor(x => x.Observation)
                .MaximumLength(200)
                    .WithMessage("Observação deve ter no máximo 200 caracteres")
                    .WithErrorCode("ME0024");
        }
    }
}
