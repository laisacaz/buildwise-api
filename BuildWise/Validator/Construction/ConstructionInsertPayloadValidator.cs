using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Construction;
using FluentValidation;

namespace BuildWise.Validator.Construction
{
    public class ConstructionInsertPayloadValidator : AbstractValidator<ConstructionInsertPayload>
    {
        public ConstructionInsertPayloadValidator(IUnitOfWork uow)
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
