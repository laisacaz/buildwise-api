using AutoMapper;
using BuildWise.Entities;
using BuildWise.Extensions;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Construction;
using FluentValidation;
using MediatR;

namespace BuildWise.Services.Command.Construction
{
    public class ConstructionUpdateCommand : IRequest<Unit>
    {
        public ConstructionUpdateCommand(
            int id, 
            ConstructionUpdatePayload payload)
        {
            Payload= payload;
            payload.SetId(id);
        }
        public ConstructionUpdatePayload Payload { get; }
    }
    internal class ConstructionUpdateCommandHandler : IRequestHandler<ConstructionUpdateCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IValidator<ConstructionUpdatePayload> _validator;
        public ConstructionUpdateCommandHandler(
            IMapper mapper,
            IUnitOfWork uow,
            IValidator<ConstructionUpdatePayload> validator)
        {
            _mapper = mapper;
            _uow = uow;
            _validator = validator;
        }
        public async Task<Unit> Handle(ConstructionUpdateCommand  request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request.Payload);
            result.ThrowExceptionIfNotValid();

            Entities.Construction oldConstruction = await _uow.Construction
                .GetByIdAsync(request.Payload.GetId());

            Entities.Construction construction = _mapper.Map<Entities.Construction>(request.Payload);
            construction.CreatedAt = oldConstruction.CreatedAt;
            construction.Status = oldConstruction.Status;
            construction.AddressId = oldConstruction.AddressId;

            Address oldAddress = await _uow.Person.Address
                .GetByIdAsync(oldConstruction.AddressId);

            if (oldAddress is not null)
            {
                Address address = _mapper.Map<Address>(request.Payload.Address);
                address.CreatedAt = oldAddress.CreatedAt;
                address.Id = oldAddress.Id;
                await _uow.Person.Address.UpdateAsync(address);
            }             

            await _uow.Construction.UpdateAsync(construction);    

            return Unit.Value;
        }
    }
}
