using BuildWise.DTO.Product;
using BuildWise.DTO;
using MediatR;
using BuildWise.DTO.ServiceOrder;
using BuildWise.Payload.ServiceOrder;
using BuildWise.Interfaces.Repository;

namespace BuildWise.Services.Query.ServiceOrder
{
    public class ServiceOrderSearchQuery : IRequest<BasePagedSearchDTO<ServiceOrderPagedSearchDTO>>
    {
        public ServiceOrderSearchQuery(ServiceOrderPagedSearchPayload payload)
        {
            Payload = payload;
        }
        public ServiceOrderPagedSearchPayload Payload { get; set; }
    }
    internal class ServiceOrderSearchQueryHandler : IRequestHandler<ServiceOrderSearchQuery, BasePagedSearchDTO<ServiceOrderPagedSearchDTO>>
    {
        private readonly IUnitOfWork _uow;
        public ServiceOrderSearchQueryHandler(
            IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<BasePagedSearchDTO<ServiceOrderPagedSearchDTO>> Handle(ServiceOrderSearchQuery request, CancellationToken cancellationToken)
        {
            SearchDTO<ServiceOrderPagedSearchDTO> dto = await _uow.ServiceOrder.PagedSearch(
                request.Payload.Search,
                request.Payload.GetLimit(),
                request.Payload.GetOffSet());

            var response = new BasePagedSearchDTO<ServiceOrderPagedSearchDTO>
            {
                Data = dto.Data,
                PageNumber = request.Payload.PageNumber.Value,
                PageSize = request.Payload.PageSize.Value,
                TotalRecords = dto.TotalRecords,
            };
            return response;
        }
    }
}
