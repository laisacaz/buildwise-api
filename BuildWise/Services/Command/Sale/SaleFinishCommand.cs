using BuildWise.Extensions;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Sale;
using FluentValidation;
using MediatR;
using static BuildWise.Enum.SaleEnum;

namespace BuildWise.Services.Command.Sale
{
    public class SaleFinishCommand : IRequest<Unit>
    {
        public SaleFinishCommand(int id, SaleFinishPayload payload)
        {
            Payload = payload;
            Payload.SetId(id);
        }
        public SaleFinishPayload Payload { get; set; }
    }
    internal class SaleFinishCommandHandler : IRequestHandler<SaleFinishCommand, Unit>
    {        
        private readonly IUnitOfWork _uow;
        private readonly IValidator<SaleFinishPayload> _validator;
        public SaleFinishCommandHandler(IUnitOfWork uow,
            IValidator<SaleFinishPayload> validator)
        {
            _uow = uow;
            _validator = validator;
        }

        public async Task<Unit> Handle(SaleFinishCommand request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request.Payload);
            result.ThrowExceptionIfNotValid();

            Entities.Sale finishedSale = await _uow.Sale.GetByIdAsync(request.Payload.GetId());
            
            finishedSale.FinalizedAt = DateTime.UtcNow;
            finishedSale.CreatedAt = finishedSale.CreatedAt;
            finishedSale.Status = ESaleStatus.Finalized;      
            finishedSale.PaidAmount = request.Payload.PaidAmount;
            finishedSale.ReceivementMethod = request.Payload.ReceivementMethod;

            if (request.Payload.Increase > 0)
            {
                finishedSale.Total = finishedSale.Subtotal + request.Payload.Increase.Value;
                finishedSale.Increase = request.Payload.Increase.Value;
            }

            if (request.Payload.Discount > 0)
            {
                finishedSale.Total = finishedSale.Total - request.Payload.Discount.Value;
                finishedSale.Discount = request.Payload.Discount.Value;
            }

            await _uow.Sale.UpdateAsync(finishedSale);  

            return Unit.Value;
        }
    }
}
