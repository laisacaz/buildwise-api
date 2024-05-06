using AutoMapper;
using BuildWise.DTO.Address;
using BuildWise.DTO.Person;
using BuildWise.Entities;
using BuildWise.Extensions;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Person;
using BuildWise.Payload.Product;
using FluentValidation;
using MediatR;

namespace BuildWise.Services.Query.Person
{
    public class PersonGetByIdQuery : IRequest<PersonDTO>
    {
        public PersonGetByIdQuery(int id)
        {
            Payload = new PersonGetByIdPayload { Id = id };
        }
        public PersonGetByIdPayload Payload { get; set; }
    }
    internal class PersonGetQueryHandler : IRequestHandler<PersonGetByIdQuery, PersonDTO>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<PersonGetByIdPayload> _validator;
        public PersonGetQueryHandler(
            IUnitOfWork uow,
            IMapper mapper,
            IValidator<PersonGetByIdPayload> validator)
        {
            _uow = uow;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<PersonDTO> Handle (PersonGetByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request.Payload);
            result.ThrowExceptionIfNotValid();

            Entities.Person person = await _uow.Person.GetByIdAsync(request.Payload.Id);
            PersonDTO personMap = _mapper.Map<PersonDTO>(person);

            Address address = await _uow.Person.Address.GetByIdAsync(person.AddressId);

            if (address is not null)
            {
                AddressDTO addressMap = _mapper.Map<AddressDTO>(address);
                personMap.Address = addressMap;
            }
            return personMap;        
        }
    }
}
