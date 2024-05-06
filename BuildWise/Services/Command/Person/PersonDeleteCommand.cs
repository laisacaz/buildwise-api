using AutoMapper;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Person;
using BuildWise.Payload.Product;
using BuildWise.Services.Command.Product;
using MediatR;

namespace BuildWise.Services.Command.Person
{
    public class PersonDeleteCommand : IRequest<Unit>
    {
        public PersonDeleteCommand(int id)
        {
            Payload = new PersonDeletePayload { Id = id };
        }
        public PersonDeletePayload Payload { get; set; }
    }
    internal class PersonDeleteCommandHandler : IRequestHandler<PersonDeleteCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PersonDeleteCommandHandler(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(PersonDeleteCommand request, CancellationToken cancellationToken)
        {
            //validar id
            Entities.Person oldPerson = await _uow.Person.GetByIdAsync(request.Payload.Id);
            oldPerson.Status = false;

            await _uow.Person.UpdateAsync(oldPerson);

            return Unit.Value;
        }
    }
}
