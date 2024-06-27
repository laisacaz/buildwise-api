using AutoMapper;
using BuildWise.Extensions;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Cashier;
using BuildWise.Payload.Construction;
using BuildWise.Services.Command.Construction;
using FluentValidation;
using MediatR;
using static BuildWise.Enum.ConstructionEnum;

namespace BuildWise.Services.Command.Cashier
{
    public class CashierOpenCommand : IRequest<int>
    {
        public CashierOpenCommand(CashierOpenPayload payload)
        {
            Payload = payload;
        }
        public CashierOpenPayload Payload { get; set; }
    }
    internal class CashierOpenCommandHandler : IRequestHandler<CashierOpenCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        //private readonly IValidator<CashierOpenPayload> _validator;
        public CashierOpenCommandHandler(
            IMapper mapper,
            IUnitOfWork uow
            //,
            //IValidator<CashierOpenPayload> validator
            )
        {
            _mapper = mapper;
            _uow = uow;
            //_validator = validator;
        }
        public async Task<int> Handle(CashierOpenCommand request, CancellationToken cancellationToken)
        {
            //var result = await _validator.ValidateAsync(request.Payload);
            //result.ThrowExceptionIfNotValid();

            Entities.Cashier cashier = _mapper.Map<Entities.Cashier>(request.Payload);
            cashier.CreatedAt = DateTime.UtcNow;
            cashier.AmountAvailable = request.Payload.InitialValue;
            cashier.Status = true;
            int id = await _uow.Cashier.InsertAsync(cashier);

            return id;
        }
    }
}
