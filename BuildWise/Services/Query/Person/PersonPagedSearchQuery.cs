using BuildWise.DTO;
using BuildWise.DTO.Person;
using BuildWise.DTO.Product;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Person;
using MediatR;

namespace BuildWise.Services.Query.Person
{
    public class PersonPagedSearchQuery : IRequest<BasePagedSearchDTO<PersonPagedSearchDTO>>
    {
        public PersonPagedSearchQuery(PersonPagedSearchPayload payload)
        {
            Payload = payload;
        }
        public PersonPagedSearchPayload Payload { get; set; }
    }
    internal class PersonPagedSearchQueryHandler : IRequestHandler<PersonPagedSearchQuery, BasePagedSearchDTO<PersonPagedSearchDTO>>
    {
        private readonly IUnitOfWork _uow;
        public PersonPagedSearchQueryHandler(
            IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<BasePagedSearchDTO<PersonPagedSearchDTO>> Handle(PersonPagedSearchQuery request, CancellationToken cancellationToken)
        {
            SearchDTO<PersonPagedSearchDTO> dto = await _uow.Person.PagedSearch(
                request.Payload.Search,
                request.Payload.SearchType,
                request.Payload.Id,
                request.Payload.GetLimit(),
                request.Payload.GetOffSet());

            var response = new BasePagedSearchDTO<PersonPagedSearchDTO>
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
