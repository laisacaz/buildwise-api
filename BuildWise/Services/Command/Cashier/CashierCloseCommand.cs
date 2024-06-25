using AutoMapper;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Cashier;
using BuildWise.Services.Command.Person;
using MediatR;

namespace BuildWise.Services.Command.Cashier
{
    public class CashierCloseCommand : IRequest<Unit>
    {
        public CashierCloseCommand(int id, CashierClosePayload payload)
        {
            Payload = payload;
            payload.SetId(id);
        }
        public CashierClosePayload Payload { get; }
    }
    internal class CashierCloseCommandHandler : IRequestHandler<CashierCloseCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CashierCloseCommandHandler(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CashierCloseCommand request, CancellationToken cancellationToken)
        {
            //validar id
            Entities.Cashier oldCashier = await _uow.Cashier.GetByIdAsync(request.Payload.GetId());
            Entities.Cashier cashier = _mapper.Map<Entities.Cashier>(request.Payload);

            cashier.Id = oldCashier.Id;            
            cashier.InitialValue = oldCashier.InitialValue;            
            cashier.CreatedAt = oldCashier.CreatedAt;            
            cashier.Status = false;
            cashier.ClosedAt = DateTime.UtcNow;
            cashier.ClosureValue = request.Payload.ClosureValue;

            await _uow.Cashier.UpdateAsync(cashier);

            return Unit.Value;
        }
    }
}
