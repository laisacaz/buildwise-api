using AutoMapper;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Cashier;
using MediatR;

namespace BuildWise.Services.Command.Cashier
{
    public class CashierOutCommand : IRequest<Unit>
    {
        public CashierOutCommand(int id, CashierOutPayload payload)
        {
            Payload = payload;
            Payload.Id = id;
        }
        public CashierOutPayload Payload { get; set; }
    }
    internal class CashierOutCommandHandler : IRequestHandler<CashierOutCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public CashierOutCommandHandler(IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CashierOutCommand request, CancellationToken cancellationToken)
        {
            //validar id
            Entities.Cashier oldCashier = await _uow.Cashier.GetByIdAsync(request.Payload.Id);
            if(oldCashier.AmountAvailable >= 0)
            {
                if(oldCashier.Outs is null)
                {
                    oldCashier.Outs = 0;
                }
                oldCashier.Outs += request.Payload.Amount;
                oldCashier.AmountAvailable -= request.Payload.Amount;
            }

            await _uow.Cashier.UpdateAsync(oldCashier);

            return Unit.Value;
        }
    }
}
