using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Person;
using FluentValidation;

namespace BuildWise.Validator.Person
{
    public class PersonUpdatePayloadValidator : AbstractValidator<PersonUpdatePayload>
    {
        public PersonUpdatePayloadValidator(IUnitOfWork uow)
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.GetId())
               .GreaterThan(0)
                   .WithMessage("Pessoa deve existir")
                   .WithErrorCode("ME0022")

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
                   .WithMessage("Pessoa deve existir")
                   .WithErrorCode("ME0022");

            RuleFor(x => x.Name)
               .NotEmpty()
                   .WithMessage("Nome é obrigatório")
                   .WithErrorCode("ME0010")
               .NotNull()
                   .WithMessage("Nome é obrigatório")
                   .WithErrorCode("ME0010")
               .MaximumLength(50)
                   .WithMessage("Nome deve ter no máximo 70 caracteres")
                   .WithErrorCode("ME0011");

            RuleFor(x => x.IdentityNumber)
                .MaximumLength(15)
                    .WithMessage("CPF deve ter no máximo 15 caracteres")
                    .WithErrorCode("ME0012");

            RuleFor(x => x.SocialSecurityNumber)
                .MaximumLength(10)
                    .WithMessage("RG deve ter no máximo 15 caracteres")
                    .WithErrorCode("ME0013");

            RuleFor(x => x.Cellphone)
                .MaximumLength(11)
                    .WithMessage("Celular deve ter no máximo 11 caracteres")
                    .WithErrorCode("ME0014");

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
