using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Construction;
using FluentValidation;
using FluentValidation.Results;
using static BuildWise.Enum.ConstructionEnum;

namespace BuildWise.Validator.Construction
{
    public class ConstructionGetByIdPayloadValidator : AbstractValidator<ConstructionGetByIdPayload>
    {
        public ConstructionGetByIdPayloadValidator(IUnitOfWork uow)
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
                    return true;
                })
                   .WithMessage("Obra deve existir")
                   .WithErrorCode("ME0025");

        }
    }
}
