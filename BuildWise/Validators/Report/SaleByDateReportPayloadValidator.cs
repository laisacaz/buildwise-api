using BuildWise.Payload.Report;
using FluentValidation;

namespace BuildWise.Validator.Report
{
    public class SaleByDateReportPayloadValidator : AbstractValidator<SaleByDateReportPayload>
    {
        public SaleByDateReportPayloadValidator()
        {
            RuleFor(x => x.StartDate)
                .NotNull()
                    .WithMessage("Data de início é obrigatório")
                    .WithErrorCode("ME0031")
                .NotEmpty()
                    .WithMessage("Data de início é obrigatório")
                    .WithErrorCode("ME0031");
            RuleFor(x => x.EndDate)
                .NotNull()
                    .WithMessage("Data final é obrigatório")
                    .WithErrorCode("ME0031")
                .NotEmpty()
                    .WithMessage("Data final é obrigatório")
                    .WithErrorCode("ME0031");
        }
    }
}
