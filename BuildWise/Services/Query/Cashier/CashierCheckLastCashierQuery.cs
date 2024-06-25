using BuildWise.DTO.Cashier;
using BuildWise.Interfaces.Repository;
using MediatR;

namespace BuildWise.Services.Query.Cashier
{
    public class CashierCheckLastCashierQuery : IRequest<CashierSimpleDTO>
    {
        public CashierCheckLastCashierQuery()
        {
            
        }
    }
    internal class CashierCheckLastCashierQueryHandler : IRequestHandler<CashierCheckLastCashierQuery, CashierSimpleDTO>
    {
        private readonly IUnitOfWork _uow;

        public CashierCheckLastCashierQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<CashierSimpleDTO> Handle(CashierCheckLastCashierQuery request, CancellationToken cancellationToken)
        {
            CashierSimpleDTO cashier = await _uow.Cashier.GetLastCashier();
            return cashier;
        }
    }
}
