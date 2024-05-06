using AutoMapper;
using BuildWise.Entities;
using BuildWise.Extensions;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Person;
using FluentValidation;
using MediatR;
using System.Net;

namespace BuildWise.Services.Command.Person
{
    public class PersonUpdateCommand : IRequest<Unit>
    {
        public PersonUpdateCommand(int id, PersonUpdatePayload payload)
        {
            Payload = payload;            
            Payload.SetId(id);
        }
        public PersonUpdatePayload Payload { get; set; }
    }
    internal class PersonUpdateCommandHandler : IRequestHandler<PersonUpdateCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        IValidator<PersonUpdatePayload> _validator;
        public PersonUpdateCommandHandler(
            IUnitOfWork uow,
            IMapper mapper,
            IValidator<PersonUpdatePayload> validator)
        {
            _uow = uow;
            _mapper = mapper;
            _validator = validator;

        }
        public async Task<Unit> Handle(PersonUpdateCommand request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request.Payload);
            result.ThrowExceptionIfNotValid();            

            Entities.Person oldPerson = await _uow.Person.GetByIdAsync(request.Payload.GetId());
            Entities.Person person = _mapper.Map<Entities.Person>(request.Payload);

            person.CreatedAt = oldPerson.CreatedAt;
            person.AddressId = oldPerson.AddressId;
            person.Status = true;

            Address oldAddress = await _uow.Person.Address
                .GetByIdAsync(oldPerson.AddressId);

            if (oldAddress is not null) { 
                
                Address address = _mapper.Map<Address>(request.Payload.Address);                    
                address.CreatedAt = oldAddress.CreatedAt;
                address.Id = oldAddress.Id;
                await _uow.Person.Address.UpdateAsync(address);
            }               
            await _uow.Person.UpdateAsync(person);

            return Unit.Value;
        }
    }
}
