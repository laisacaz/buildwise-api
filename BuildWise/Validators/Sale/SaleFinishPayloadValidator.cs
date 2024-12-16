using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Sale;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using static BuildWise.Enum.SaleEnum;

namespace BuildWise.Validator.Sale
{
    public class SaleFinishPayloadValidator : AbstractValidator<SaleFinishPayload>
    {
        public SaleFinishPayloadValidator(IUnitOfWork uow)
        {
            CascadeMode = CascadeMode.Stop;


            RuleFor(x => x.GetId())
                .GreaterThan(0)
                    .WithMessage("Venda não localizada")
                    .WithErrorCode("ME0004")
                .MustAsync(async (payload, cod, context, cancellation) =>
                {
                    Entities.Sale sale = await uow.Sale.GetByIdAsync(cod);

                    if (sale == null)
                    {                    
                        return false;
                    }

                    if (sale.Status is not ESaleStatus.Open)
                    {
                        return false;
                    }
                    return true;
                }) 
                    .WithMessage("Venda deve existir e estar em aberto")
                    .WithErrorCode("ME0007");

            RuleFor(x => x.PaidAmount)
                .GreaterThan(0)
                    .WithMessage("Valor recebido deve ser maior do que zero.")
                    .WithErrorCode("ME0005")
                .MustAsync(async (payload, paidAmount, context, cancellation) =>
                {
                    Entities.Sale sale = await uow.Sale.GetByIdAsync(payload.GetId());

                    if (payload.Increase > 0)
                    {
                        sale.Total += payload.Increase.Value;
                    }

                    if (payload.Discount > 0)
                    {
                        sale.Total -= payload.Discount.Value;                        
                    }

                    if (payload.ReceivementMethod != ESaleReceivementMethod.Money
                        && paidAmount != sale.Total)
                    {
                        return false;
                    }                    
                    return true;
                })
                    .WithMessage("Valor pago deve ser igual ao valor total")
                    .WithErrorCode("ME0008"); ;

            RuleFor(x => x.ReceivementMethod)
                .NotNull()
                    .WithMessage("Forma de pagamento deve ser informada.")
                    .WithErrorCode("ME0006");

            RuleFor(x => x.Discount)
                .GreaterThan(0)
                    .WithMessage("Valor do desconto não pode ser menor que 0")
                    .WithErrorCode("ME0006")
                .MustAsync(async (payload, discountValue, context, cancellation) =>
                {
                    Entities.Sale sale = await uow.Sale.GetByIdAsync(payload.GetId());

                    if (payload.Increase > 0)
                    {
                        sale.Total += payload.Increase.Value;
                    }

                    if (discountValue > sale.Total)
                    {
                        sale.Total += payload.Increase.Value;

                        return false;
                    }
                    return true;
                })
                    .WithMessage("Valor pago deve ser igual ao valor total")
                    .WithErrorCode("ME0008")
            .When(x => x.Discount > 0);
        }
    }
}
