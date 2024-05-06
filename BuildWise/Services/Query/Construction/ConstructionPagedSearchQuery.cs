using BuildWise.DTO;
using BuildWise.DTO.Construction;
using BuildWise.DTO.Product;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Construction;
using MediatR;
using static BuildWise.Enum.ConstructionEnum;

namespace BuildWise.Services.Query.Construction
{
    public class ConstructionPagedSearchQuery : IRequest<BasePagedSearchDTO<ConstructionPagedSearchDTO>>
    {
        public ConstructionPagedSearchQuery(ConstructionPagedSearchPayload payload)
        {
            Payload = payload;
        }
        public ConstructionPagedSearchPayload Payload { get; set; }
    }
    internal class ConstructionPagedSearchQueryHandler : IRequestHandler<ConstructionPagedSearchQuery, BasePagedSearchDTO<ConstructionPagedSearchDTO>>
    {
        private readonly IUnitOfWork _uow;
        public ConstructionPagedSearchQueryHandler(
            IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<BasePagedSearchDTO<ConstructionPagedSearchDTO>> Handle(ConstructionPagedSearchQuery request, CancellationToken cancellationToken)
        {
            SearchDTO<ConstructionPagedSearchDTO> dto = await _uow.Construction.PagedSearch(
                request.Payload.Search,
                request.Payload.SearchType,
                request.Payload.Id,
                request.Payload.Status,
                request.Payload.GetLimit(),
                request.Payload.GetOffSet());

            var response = new BasePagedSearchDTO<ConstructionPagedSearchDTO>
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
