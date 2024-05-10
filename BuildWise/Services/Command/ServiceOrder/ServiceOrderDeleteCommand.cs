using BuildWise.Extensions;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Product;
using BuildWise.Payload.ServiceOrder;
using BuildWise.Services.Command.Product;
using FluentValidation;
using MediatR;

namespace BuildWise.Services.Command.ServiceOrder
{
    public class ServiceOrderDeleteCommand : IRequest<Unit>
    {
        public ServiceOrderDeleteCommand(int id)
        {
            Payload = new ServiceDeletePayload { Id = id };
        }
        public ServiceDeletePayload Payload { get; set; }
    }
    internal class ServiceOrderDeleteCommandHandler : IRequestHandler<ServiceOrderDeleteCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<ServiceDeletePayload> _validator;
        public ServiceOrderDeleteCommandHandler(
            IUnitOfWork uow,
            IValidator<ServiceDeletePayload> validator)
        {
            _uow = uow;
            _validator = validator;
        }
        public async Task<Unit> Handle(ServiceOrderDeleteCommand request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request.Payload);
            result.ThrowExceptionIfNotValid();

            Entities.ServiceOrder oldServiceOrder = await _uow.ServiceOrder.GetByIdAsync(request.Payload.Id);
            oldServiceOrder.Status = false;

            await _uow.ServiceOrder.UpdateAsync(oldServiceOrder);

            return Unit.Value;
        }
    }
}
