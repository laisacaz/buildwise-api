using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Sale;
using BuildWise.Validator.Product;
using FluentValidation;
using static BuildWise.Enum.ConstructionEnum;
using static BuildWise.Enum.SaleEnum;

namespace BuildWise.Validator.Sale
{
    public class SaleUpdatePayloadValidator : AbstractValidator<SaleUpdatePayload>
    {
        public SaleUpdatePayloadValidator(IUnitOfWork uow)
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.GetId())
                .GreaterThan(0)
                    .WithMessage("Venda deve existir")
                    .WithErrorCode("ME0029")

                .MustAsync(async (payload, cod, context, cancellation) =>
                {
                    Entities.Sale sale = await uow.Sale.GetByIdAsync(cod);

                    if (sale == null)
                    {
                        return false;
                    }
                    if (sale.Status is not ESaleStatus.Open)
                    {
                        return false;
                    }
                    return true;
                })
                .WithMessage("Venda deve existir")
                .WithErrorCode("ME0029");

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
                    .WithMessage("Você deve inserir no mínimo um produto ou serviço para salvar a venda")
                    .WithErrorCode("ME0028")
            .When(x => x.Services is null || (x.Services is not null && x.Services.Count < 0));

            RuleFor(x => x.Services)
                .Must(services => services != null && services.Count > 0)
                    .WithMessage("Você deve inserir no mínimo um produto ou serviço para salvar a venda")
                    .WithErrorCode("ME0028")
            .When(x => x.Products is null || (x.Products is not null && x.Products.Count < 0));

            RuleForEach(x => x.Products)
                .SetValidator(new SaleProductUpdatePayloadValidator(uow));

            RuleForEach(x => x.Services)
                .SetValidator(new SaleServiceOrderUpdatePayloadValidator(uow));

            RuleFor(x => x.Observation)
                .MaximumLength(200)
                    .WithMessage("Observação deve ter no máximo 200 caracteres")
                    .WithErrorCode("ME0024");
        }
    }
}
