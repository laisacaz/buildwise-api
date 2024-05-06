using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Construction;
using FluentValidation;
using static BuildWise.Enum.ConstructionEnum;

namespace BuildWise.Validator.Construction
{
    public class ConstructionDeletePayloadValidator : AbstractValidator<ConstructionDeletePayload>
    {
        public ConstructionDeletePayloadValidator(IUnitOfWork uow)
        {
            RuleFor(x => x.Id)
               .GreaterThan(0)
                   .WithMessage("Obra deve existir")
                   .WithErrorCode("ME0025")

                .MustAsync(async (payload, cod, context, cancellation) =>
                {
                    Entities.Construction construction = await uow.Construction.GetByIdAsync(cod);

                    if (construction == null)
                    {
                        return false;
                    }
                    if (construction.Status is EStatusConstruction.Canceled)
                    {
                        return false;
                    }
                    return true;
                })
                   .WithMessage("Obra deve existir")
                   .WithErrorCode("ME0025");
        }
    }
}
