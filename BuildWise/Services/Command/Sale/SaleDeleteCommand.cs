using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Construction;
using BuildWise.Payload.Sale;
using BuildWise.Services.Command.Construction;
using FluentValidation;
using MediatR;
using static BuildWise.Enum.ConstructionEnum;
using static BuildWise.Enum.SaleEnum;

namespace BuildWise.Services.Command.Sale
{
    public class SaleDeleteCommand : IRequest<Unit>
    {
        public SaleDeleteCommand(int id)
        {
            Payload = new SaleDeletePayload { Id = id };
        }
        public SaleDeletePayload Payload { get; set; }
    }
    internal class SaleDeleteCommandCommandHandler : IRequestHandler<SaleDeleteCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<SaleDeletePayload> _validator;

        public SaleDeleteCommandCommandHandler(
            IUnitOfWork uow,
            IValidator<SaleDeletePayload> validator)
        {
            _uow = uow;
            _validator = validator;
        }
        public async Task<Unit> Handle(SaleDeleteCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request.Payload);

            Entities.Sale sale = await _uow.Sale.GetByIdAsync(request.Payload.Id);
            sale.Status = ESaleStatus.Canceled;

            await _uow.Sale.UpdateAsync(sale);

            return Unit.Value;
        }
    }
}
