using BuildWise.DTO.Person;
using BuildWise.Interfaces.Repository;
using MediatR;

namespace BuildWise.Services.Query.Person
{
    public class PersonActivesSimpleGetQuery : IRequest<List<PersonSimpleDTO>>
    {
        public PersonActivesSimpleGetQuery()
        {
            
        }
    }
    internal class PersonActivesSimpleGetQueryHandler : IRequestHandler<PersonActivesSimpleGetQuery, List<PersonSimpleDTO>>
    {
        private readonly IUnitOfWork _uow;
        public PersonActivesSimpleGetQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<List<PersonSimpleDTO>> Handle(PersonActivesSimpleGetQuery request, CancellationToken cancellationToken)
        {
            List<PersonSimpleDTO> dto = await _uow.Person.GetAllActivesSimple();
            return dto;
        }        
        
    }
}
