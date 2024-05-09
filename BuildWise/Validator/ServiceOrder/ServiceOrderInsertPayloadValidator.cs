using BuildWise.Payload.Service;
using FluentValidation;

namespace BuildWise.Validator.ServiceOrder
{
    public class ServiceOrderInsertPayloadValidator : AbstractValidator<ServiceOrderInsertPayload>
    {
        public ServiceOrderInsertPayloadValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Description)
                .NotEmpty()
                    .WithMessage("Informar descrição")
                    .WithErrorCode("ME0003")
                .NotNull()
                    .WithMessage("Informar descrição")
                    .WithErrorCode("ME0003")
                .MaximumLength(50)
                    .WithMessage("Descrição deve ter no máximo 100 caracteres")
                    .WithErrorCode("ME0004");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                    .WithMessage("Preço do serviço não pode ser menor que 0")
                    .WithErrorCode("ME0007");
        }
    }
}
