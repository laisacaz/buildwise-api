using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Construction;
using FluentValidation;
using static BuildWise.Enum.ConstructionEnum;

namespace BuildWise.Validator.Construction
{
    public class ConstructionUpdatePayloadValidator : AbstractValidator<ConstructionUpdatePayload>
    {
        public ConstructionUpdatePayloadValidator(IUnitOfWork uow)
        {
            RuleFor(x => x.GetId())
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
                     if (construction.Status is not EStatusConstruction.Open)
                     {
                         return false;
                     }
                     return true;
                 })
                    .WithMessage("Obra deve existir")
                    .WithErrorCode("ME0025");

            RuleFor(x => x.ClientId)
                .GreaterThan(0)
                    .WithMessage("Cliente deve existir")
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

            RuleFor(x => x.Observation)
                .MaximumLength(200)
                    .WithMessage("Observação deve ter no máximo 200 caracteres")
                    .WithErrorCode("ME0024");

            RuleFor(x => x.Address.Street)
                .MaximumLength(20)
                    .WithMessage("Rua deve ter no máximo 20 caracteres")
                    .WithErrorCode("ME0016");

            RuleFor(x => x.Address.StreetNumber)
                .MaximumLength(10)
                    .WithMessage("Estado deve ter no máximo 10 caracteres")
                    .WithErrorCode("ME0017");

            RuleFor(x => x.Address.District)
                .MaximumLength(20)
                    .WithMessage("Bairro deve ter no máximo 20 caracteres")
                    .WithErrorCode("ME0018");

            RuleFor(x => x.Address.ZipCode)
                .MaximumLength(10)
                    .WithMessage("CEP deve ter no máximo 10 caracteres")
                    .WithErrorCode("ME0019");

            RuleFor(x => x.Address.City)
                .MaximumLength(25)
                    .WithMessage("Cidade deve ter no máximo 30 caracteres")
                    .WithErrorCode("ME0020");

            RuleFor(x => x.Address.State)
                .MaximumLength(25)
                    .WithMessage("Estado deve ter no máximo 30 caracteres")
                    .WithErrorCode("ME0021");
        }
    }
}
