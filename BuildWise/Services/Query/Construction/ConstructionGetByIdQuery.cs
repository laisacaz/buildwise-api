using AutoMapper;
using BuildWise.DTO.Address;
using BuildWise.DTO.Construction;
using BuildWise.DTO.Person;
using BuildWise.Entities;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Construction;
using BuildWise.Payload.Product;
using FluentValidation;
using MediatR;

namespace BuildWise.Services.Query.Construction
{
    public class ConstructionGetByIdQuery : IRequest<ConstructionDTO>
    {
        public ConstructionGetByIdQuery(int id)
        {
            Payload = new ConstructionGetByIdPayload { Id = id };
        }
        public ConstructionGetByIdPayload Payload { get; }
    }
    internal class ConstructionGetByIdQueryHandler : IRequestHandler<ConstructionGetByIdQuery, ConstructionDTO>
    {        
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<ConstructionGetByIdPayload> _validator;
        public ConstructionGetByIdQueryHandler(
            IUnitOfWork uow,
            IMapper mapper,
            IValidator<ConstructionGetByIdPayload> validator)
        {
            _uow = uow;
            _validator = validator;
            _mapper = mapper;
        }
        public async Task<ConstructionDTO> Handle(ConstructionGetByIdQuery request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request.Payload);

            Entities.Construction construction = await _uow.Construction.GetByIdAsync(request.Payload.Id);

            ConstructionDTO dto = _mapper.Map<ConstructionDTO>(construction);

            PersonSimpleDTO client = await _uow.Person.GetSimple(construction.ClientId);
            dto.Client = client;

            Address address = await _uow.Person.Address.GetByIdAsync(construction.AddressId);

            if (address is not null)
            {
                AddressDTO addressMap = _mapper.Map<AddressDTO>(address);
                dto.Address = addressMap;
            }
            return dto;            
        }
    }
}
