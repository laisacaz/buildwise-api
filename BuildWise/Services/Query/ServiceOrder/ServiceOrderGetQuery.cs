using BuildWise.Extensions;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Product;
using BuildWise.Payload.ServiceOrder;
using BuildWise.Services.Query.Product;
using FluentValidation;
using MediatR;

namespace BuildWise.Services.Query.ServiceOrder
{
    public class ServiceOrderGetQuery : IRequest<Entities.ServiceOrder>
    {
        public ServiceOrderGetQuery(int id)
        {
            Payload = new ServiceOrderGetByIdPayload { Id = id };
        }
        public ServiceOrderGetByIdPayload Payload { get; set; }
    }
    internal class ServiceOrderGetQueryHandler : IRequestHandler<ServiceOrderGetQuery, Entities.ServiceOrder>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<ServiceOrderGetByIdPayload> _validator;
        public ServiceOrderGetQueryHandler(
            IUnitOfWork uow,
            IValidator<ServiceOrderGetByIdPayload> validator)
        {
            _uow = uow;
            _validator = validator;
        }
        public async Task<Entities.ServiceOrder> Handle(ServiceOrderGetQuery request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request.Payload);
            result.ThrowExceptionIfNotValid();

            Entities.ServiceOrder serviceOrder = await _uow.ServiceOrder.GetByIdAsync(request.Payload.Id);
            return serviceOrder;
        }
    }
}
