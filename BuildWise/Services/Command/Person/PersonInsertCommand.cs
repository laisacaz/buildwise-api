using AutoMapper;
using BuildWise.Entities;
using BuildWise.Extensions;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Person;
using FluentValidation;
using MediatR;

namespace BuildWise.Services.Command.Person
{
    public class PersonInsertCommand : IRequest<int>
    {
        public PersonInsertCommand(PersonInsertPayload payload)
        {
            Payload = payload;
        }
        public PersonInsertPayload Payload { get; set; }
    }
    internal class PersonInsertCommandHandler : IRequestHandler<PersonInsertCommand, int>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<PersonInsertPayload> _validator;
        public PersonInsertCommandHandler(
            IUnitOfWork uow,
            IMapper mapper,
            IValidator<PersonInsertPayload> validator)
        {
            _uow = uow;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<int> Handle(PersonInsertCommand request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request.Payload);
            result.ThrowExceptionIfNotValid();

            Entities.Person person = _mapper.Map<Entities.Person>(request.Payload);
            person.Status = true;
            person.CreatedAt = DateTime.UtcNow;
               
            if (request.Payload.Address is not null)
            {
                Address address = _mapper.Map<Address>(request.Payload.Address);
                address.CreatedAt = DateTime.UtcNow;

                person.AddressId = await _uow.Person.Address.InsertAsync(address);
                       
            }
            int id = await _uow.Person.InsertAsync(person);

            return id;
        }
    }
}
