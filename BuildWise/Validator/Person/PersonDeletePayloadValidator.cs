using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Person;
using FluentValidation;

namespace BuildWise.Validator.Person
{
    public class PersonDeletePayloadValidator : AbstractValidator<PersonDeletePayload>
    {
        public PersonDeletePayloadValidator(IUnitOfWork uow)
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Id)
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
        }
    }
}
