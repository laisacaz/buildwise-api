using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Person;
using FluentValidation;
using FluentValidation.Results;

namespace BuildWise.Validator.Person
{
    public class PersonGetByIdPayloadValidator : AbstractValidator<PersonGetByIdPayload>
    {
        public PersonGetByIdPayloadValidator(IUnitOfWork uow)
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
