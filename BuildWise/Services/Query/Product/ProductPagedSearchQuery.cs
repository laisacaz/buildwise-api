using BuildWise.DTO;
using BuildWise.DTO.Product;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Product;
using MediatR;

namespace BuildWise.Services.Query.Product
{
    public class ProductPagedSearchQuery : IRequest<BasePagedSearchDTO<ProductPagedSearchDTO>>
    {
        public ProductPagedSearchQuery(ProductPagedSearchPayload payload)
        {
            Payload = payload;
        }
        public ProductPagedSearchPayload Payload { get; set; }
    }
    internal class ProductPagedSearchQueryHandler : IRequestHandler<ProductPagedSearchQuery, BasePagedSearchDTO<ProductPagedSearchDTO>>
    {
        private readonly IUnitOfWork _uow;
        public ProductPagedSearchQueryHandler(
            IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<BasePagedSearchDTO<ProductPagedSearchDTO>> Handle(ProductPagedSearchQuery request, CancellationToken cancellationToken)
        {
            SearchDTO<ProductPagedSearchDTO> dto = await _uow.Product.PagedSearch(
                request.Payload.Search,
                request.Payload.SearchType,
                request.Payload.GetLimit(),
                request.Payload.GetOffSet());

            var response = new BasePagedSearchDTO<ProductPagedSearchDTO>
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
