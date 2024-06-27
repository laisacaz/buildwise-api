using BuildWise.Interfaces.Repository;
using BuildWise.Pages;
using BuildWise.Payload.User;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using static BuildWise.Enum.ConstructionEnum;

namespace BuildWise.Validator.User
{
    public class UserSigninPayloadValidator : AbstractValidator<UserSigninPayload>
    {
        public UserSigninPayloadValidator(IPublicUnitOfWork uowp)
        {
            RuleFor(x => x.Email)
            .NotNull()
                  .WithMessage("Email deve existir")
                  .WithErrorCode("ME0011")
            .MaximumLength(200)
                  .WithMessage("Email deve ter no máximo 200 caracteres")
                  .WithErrorCode("ME0011")
            .EmailAddress()
                  .WithMessage("Email inválido")
                  .WithErrorCode("ME0011");

            RuleFor(x => x.Password)
            .NotNull()
                    .WithMessage("Senha precisa ser preenchida")
                    .WithErrorCode("ME0011")
                .WithName("Senha");

            RuleFor(x => x)
                 .MustAsync(async (payload, cod, context, cancellation) =>
                 {
                     Entities.User? user = await uowp.User.GetUserByEmailAndPassword(payload.Email, payload.Password);

                     if (user is  null)
                     {
                         return false;
                     }
                     return true;
                 })
                .WithMessage("Email ou senha incorretos")
                .WithErrorCode("ME0011");
        }
    }
}
