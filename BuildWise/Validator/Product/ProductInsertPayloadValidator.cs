using BuildWise.Payload.Product;
using FluentValidation;

namespace BuildWise.Validator.Product
{
    public class ProductInsertPayloadValidator : AbstractValidator<ProductInsertPayload>
    {
        public ProductInsertPayloadValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Reference)
                .NotEmpty()
                    .WithMessage("Informar referência")
                    .WithErrorCode("ME0001")
                .NotNull()
                    .WithMessage("Informar referência")
                    .WithErrorCode("ME0001")
                .MaximumLength(20)
                    .WithMessage("Referência deve ter no máximo 15 caracteres")
                    .WithErrorCode("ME0002");


            RuleFor(x => x.Description)
                .NotEmpty()
                    .WithMessage("Informar descrição")
                    .WithErrorCode("ME0003")
                .NotNull()
                    .WithMessage("Informar descrição")
                    .WithErrorCode("ME0003")
                .MaximumLength(50)
                    .WithMessage("Descrição deve ter no máximo 60 caracteres")
                    .WithErrorCode("ME0004");

            RuleFor(x => x.StockQuantity)
                .GreaterThan(0)
                    .WithMessage("Quantidade do produto não pode ser menor que 0")
                    .WithErrorCode("ME0005");

            RuleFor(x => x.Cost)
                .GreaterThanOrEqualTo(0)
                    .WithMessage("Custo não pode ser menor que 0")
                    .WithErrorCode("ME0006");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                    .WithMessage("Preço do produto não pode ser menor que 0")    
                    .WithErrorCode("ME0007");


            RuleFor(x => x.BarCode)
                .MaximumLength(13)
                    .WithMessage("Código de barras deve ter no máximo 13 caracteres")
                    .WithErrorCode("ME0008");
        }
    }
}
