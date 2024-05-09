using AutoMapper;
using BuildWise.Extensions;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Product;
using BuildWise.Payload.Service;
using FluentValidation;
using MediatR;

namespace BuildWise.Services.Command.Service
{
    public class ServiceOrderInsertCommand : IRequest<int>
    {
        public ServiceOrderInsertCommand(ServiceOrderInsertPayload payload)
        {
            Payload = payload;
        }
        public ServiceOrderInsertPayload Payload { get; set; }
    }
    internal class ServiceOrderInsertCommandHandler : IRequestHandler<ServiceOrderInsertCommand, int>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<ServiceOrderInsertPayload> _validator;

        public ServiceOrderInsertCommandHandler(
            IUnitOfWork uow,
            IMapper mapper,
            IValidator<ServiceOrderInsertPayload> validator)
        {
            _uow = uow;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<int> Handle(ServiceOrderInsertCommand request, CancellationToken cancellationToken)
        {
            FluentValidation.Results.ValidationResult result = await _validator.ValidateAsync(request.Payload);
            result.ThrowExceptionIfNotValid();

            Entities.ServiceOrder service = _mapper.Map<Entities.ServiceOrder>(request.Payload);
            service.Status = true;
            service.CreatedAt = DateTime.UtcNow;
            int id = await _uow.ServiceOrder.InsertAsync(service);
            return id;
        }
    }

}
