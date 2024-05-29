using BuildWise.Pages;
using BuildWise.Payload.User;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace BuildWise.Validator.User
{
    public class UserGetByIdPayloadValidator : AbstractValidator<UserGetByIdPayload>
    {
        public UserGetByIdPayloadValidator()
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


            RuleFor(x => x.Email)
            .MustAsync(async (payload, email, context, cancelationToken) =>
            {
                Entities.User.User user = await userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    context.AddFailure(
                        ValidationFailureHelper.New(
                            propertyName: ErrorCodes.ME0002,
                            ErrorCodes.ME0002,
                            nameof(ErrorCodes.ME0002)));
                    return true;
                }

                if (user.Level != Enums.UserLevel.Regular)
                {
                    context.AddFailure(
                        ValidationFailureHelper.New(
                            propertyName: PropertiesName.Usuario,
                            ErrorCodes.ME0115,
                            nameof(ErrorCodes.ME0115)));
                    return true;
                }

                System.Collections.Generic.List<Dto.Company.BranchCompanySimpleDTO> companiesWithAccess = await uowp.UserBranchCompany.GetBranchCompaniesAccessAsync(user.Id);
                if (companiesWithAccess.Count == 0)
                {
                    context.AddFailure(
                        ValidationFailureHelper.New(
                            propertyName: PropertiesName.Usuario,
                            ErrorCodes.ME0013,
                            nameof(ErrorCodes.ME0013)));
                    return true;
                }
                return true;
            });

        }
    }
}
