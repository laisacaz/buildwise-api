using BuildWise.DTO;
using BuildWise.DTO.Construction;
using BuildWise.DTO.Sale;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Sale;
using MediatR;

namespace BuildWise.Services.Query.Sale
{
    public class SalePagedSearchquery : IRequest<BasePagedSearchDTO<SalePagedSearchDTO>>
    {
        public SalePagedSearchquery(SalePagedSearchPayload payload)
        {
            Payload = payload;
        }
        public SalePagedSearchPayload Payload { get; set; }
    }
    internal class SalePagedSearchqueryHandler : IRequestHandler<SalePagedSearchquery, BasePagedSearchDTO<SalePagedSearchDTO>>
    {
        private readonly IUnitOfWork _uow;
        public SalePagedSearchqueryHandler(IUnitOfWork uow)
        {            
            _uow = uow;
        }
        public async Task<BasePagedSearchDTO<SalePagedSearchDTO>> Handle(SalePagedSearchquery request, CancellationToken cancellationToken)
        {
            SearchDTO<SalePagedSearchDTO> dto = await _uow.Sale.PagedSearch(
                request.Payload.Search,
                request.Payload.SearchType,
                request.Payload.Id,
                request.Payload.Status,
                request.Payload.GetLimit(),
                request.Payload.GetOffSet());

            var response = new BasePagedSearchDTO<SalePagedSearchDTO>
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
