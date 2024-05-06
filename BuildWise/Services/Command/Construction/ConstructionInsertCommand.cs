using AutoMapper;
using BuildWise.Entities;
using BuildWise.Extensions;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Construction;
using FluentValidation;
using MediatR;
using static BuildWise.Enum.ConstructionEnum;

namespace BuildWise.Services.Command.Construction
{
    public class ConstructionInsertCommand : IRequest<int>
    {
        public ConstructionInsertCommand(ConstructionInsertPayload payload)
        {
            Payload = payload;
        }
        public ConstructionInsertPayload Payload { get; }
    }
    internal class ConstructionInsertCommandHandler : IRequestHandler<ConstructionInsertCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IValidator<ConstructionInsertPayload> _validator;
        public ConstructionInsertCommandHandler(
            IMapper mapper,
            IUnitOfWork uow,
            IValidator<ConstructionInsertPayload> validator)
        {
            _mapper = mapper;
            _uow = uow;
            _validator = validator;
        }
        public async Task<int> Handle(ConstructionInsertCommand request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request.Payload);
            result.ThrowExceptionIfNotValid();

            Entities.Construction construction = _mapper.Map<Entities.Construction>(request.Payload);
            construction.CreatedAt = DateTime.UtcNow;
            construction.Status = EStatusConstruction.Open;                

            if (request.Payload.Address is not null)
            {
                Entities.Address address = _mapper.Map<Entities.Address>(request.Payload.Address);
                address.CreatedAt = DateTime.UtcNow;
                int addressId = await _uow.Person.Address.InsertAsync(address);
                construction.AddressId = addressId;
            }
            int id = await _uow.Construction.InsertAsync(construction);

            return id;
        }
    }
}
