using AutoMapper;
using BuildWise.Extensions;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Product;
using BuildWise.Payload.ServiceOrder;
using FluentValidation;
using MediatR;

namespace BuildWise.Services.Command.ServiceOrder
{
    public class ServiceOrderUpdateCommand : IRequest<Unit>
    {
        public ServiceOrderUpdateCommand(int id, ServiceOrderUpdatePayload payload)
        {
            Payload = payload;
            Payload.SetId(id);
        }
        public ServiceOrderUpdatePayload Payload { get; set; }       
    }
    internal class ServiceOrderUpdateCommandHandler : IRequestHandler<ServiceOrderUpdateCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<ServiceOrderUpdatePayload> _validator;
        public ServiceOrderUpdateCommandHandler(
            IUnitOfWork uow,
            IMapper mapper,
            IValidator<ServiceOrderUpdatePayload> validator)
        {
            _uow = uow;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<Unit> Handle(ServiceOrderUpdateCommand request, CancellationToken cancellationToken)
        {
            FluentValidation.Results.ValidationResult result = await _validator.ValidateAsync(request.Payload);
            result.ThrowExceptionIfNotValid();

            Entities.ServiceOrder oldServiceOrder = await _uow.ServiceOrder.GetByIdAsync(request.Payload.GetId());
            Entities.ServiceOrder serviceOrder = _mapper.Map<Entities.ServiceOrder>(request.Payload);
            serviceOrder.CreatedAt = oldServiceOrder.CreatedAt;
            serviceOrder.Status = oldServiceOrder.Status;

            await _uow.ServiceOrder.UpdateAsync(serviceOrder);

            return Unit.Value;
        }
    }
}
