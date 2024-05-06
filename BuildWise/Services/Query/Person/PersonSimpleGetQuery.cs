using BuildWise.DTO.Person;
using BuildWise.Interfaces.Repository;
using BuildWise.Services.Query.Person;
using MediatR;

namespace BuildWise.Services.Query.Person
{
    public class PersonSimpleGetQuery : IRequest<List<PersonSimpleDTO>>
    {
        public PersonSimpleGetQuery()
        {

        }
    }

internal class PersonSimpleGetQueryHandler : IRequestHandler<PersonSimpleGetQuery, List<PersonSimpleDTO>>
{
    private readonly IUnitOfWork _uow;
    public PersonSimpleGetQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    public async Task<List<PersonSimpleDTO>> Handle(PersonSimpleGetQuery request, CancellationToken cancellationToken)
    {
        List<PersonSimpleDTO> dto = await _uow.Person.GetAllSimple();
        return dto;
    }

}
}