using AutoMapper;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Cashier;
using MediatR;

namespace BuildWise.Services.Command.Cashier
{
    public class CashierEntryCommand : IRequest<Unit>
    {
        public CashierEntryCommand(int id, CashierEntryPayload payload)
        {
            Payload = payload;
            Payload.Id = id;
        }
        public CashierEntryPayload Payload { get; set; }
    }
    internal class CashierEntryCommandHandler : IRequestHandler<CashierEntryCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public CashierEntryCommandHandler(IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CashierEntryCommand request, CancellationToken cancellationToken)
        {
            //validar id
            Entities.Cashier oldCashier = await _uow.Cashier.GetByIdAsync(request.Payload.Id);
            if(oldCashier.Entries is null)
            {
                oldCashier.Entries = 0;
            }
            oldCashier.Entries += request.Payload.Amount;
            if (oldCashier.AmountAvailable is null)
            {
                oldCashier.AmountAvailable = 0;
            }
            oldCashier.AmountAvailable += request.Payload.Amount;
           
            await _uow.Cashier.UpdateAsync(oldCashier);

            return Unit.Value;
        }
    }
}
