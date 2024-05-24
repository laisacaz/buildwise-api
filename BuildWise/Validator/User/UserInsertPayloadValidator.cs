using BuildWise.Payload.ServiceOrder;
using BuildWise.Payload.User;
using FluentValidation;

namespace BuildWise.Validator.User
{
    public class UserInsertPayloadValidator : AbstractValidator<UserInsertPayload>
    {
        public UserInsertPayloadValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Email)
               .NotEmpty()
                   .WithMessage("Informar email")
                   .WithErrorCode("ME0003")
               .NotNull()
                   .WithMessage("Informar email")
                   .WithErrorCode("ME0003")
               .MaximumLength(60)
                   .WithMessage("Email deve ter no máximo 60 caracteres")
                   .WithErrorCode("ME0004");

            RuleFor(x => x.Name)
               .NotEmpty()
                   .WithMessage("Informar nome")
                   .WithErrorCode("ME0003")
               .NotNull()
                   .WithMessage("Informar nome")
                   .WithErrorCode("ME0003")
               .MaximumLength(150)
                   .WithMessage("nome deve ter no máximo 150 caracteres")
                   .WithErrorCode("ME0004");

            RuleFor(x => x.Password)
                .NotEmpty()
                    .WithMessage("Informar senha")
                    .WithErrorCode("ME0003")
                .NotNull()
                    .WithMessage("Informar senha")
                    .WithErrorCode("ME0003")
                .MaximumLength(30)
                    .WithMessage("Senha deve ter no máximo 30 caracteres")
                    .WithErrorCode("ME0004");

            RuleFor(x => x.RegisteredNumber)
                .NotEmpty()
                    .WithMessage("Informar CNPJ")
                    .WithErrorCode("ME0003")
                .NotNull()
                    .WithMessage("Informar CNPJ")
                    .WithErrorCode("ME0003")
                .MaximumLength(14)
                    .WithMessage("CNPJ deve ter no máximo 14 caracteres")
                    .WithErrorCode("ME0004");
        }
    }
}
