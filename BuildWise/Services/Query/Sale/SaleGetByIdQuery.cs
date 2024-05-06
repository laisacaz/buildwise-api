using AutoMapper;
using BuildWise.DTO.Address;
using BuildWise.DTO.Construction;
using BuildWise.DTO.Person;
using BuildWise.DTO.Product;
using BuildWise.DTO.Sale;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Product;
using BuildWise.Payload.Sale;
using FluentValidation;
using MediatR;
using static BuildWise.Enum.SaleEnum;

namespace BuildWise.Services.Query.Sale
{
    public class SaleGetByIdQuery : IRequest<SaleDTO>
    {
        public SaleGetByIdQuery(int id)
        {
            Payload = new SaleGetByIdPayload { Id = id };
        }
        public SaleGetByIdPayload Payload { get; set; }
    }

    internal class SaleGetByIdQueryHandler : IRequestHandler<SaleGetByIdQuery, SaleDTO>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<SaleGetByIdPayload> _validator;
        public SaleGetByIdQueryHandler(
            IUnitOfWork uow,
            IValidator<SaleGetByIdPayload> validator,
            IMapper mapper)
        {
            _uow = uow;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<SaleDTO> Handle(SaleGetByIdQuery request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request.Payload);

            Entities.Sale sale = await _uow.Sale.GetByIdAsync(request.Payload.Id);

            //construction
            ConstructionDTO? constructionDto = null;
            if (sale.ConstructionId > 0) { 

                Entities.Construction construction = await _uow.Construction.GetByIdAsync(sale.ConstructionId);
                AddressDTO? constructionAddressMap = null;

                if (construction.AddressId > 0)
                {
                    Entities.Address constructionAddress = await _uow.Person.Address.GetByIdAsync(construction.AddressId);
                    constructionAddressMap = _mapper.Map<AddressDTO>(constructionAddress);
                }
                PersonSimpleDTO constructionClient = await _uow.Person.GetSimple(construction.ClientId);

                constructionDto = new ConstructionDTO
                {
                    Id = construction.Id,
                    Observation = construction.Observation,
                    ClientId = construction.ClientId,
                    Client = constructionClient,
                    Address = constructionAddressMap,
                    CreatedAt = construction.CreatedAt,
                    Status = construction.Status,
                };
            }                    

            //pessoas

            PersonSimpleDTO client = await _uow.Person.GetSimple(sale.ClientId);
            PersonSimpleDTO seller = await _uow.Person.GetSimple(sale.SellerId);

            SaleRecordDTO saledto = _mapper.Map<SaleRecordDTO>(sale);

            // produtos

                List<SaleProductDTO> products = await _uow.Sale.Product.GetSaleFullProducts(
                    sale.Status,
                    request.Payload.Id);

            SaleDTO dto = new SaleDTO
            {
                Client = client,
                Sale = saledto,
                Construction = constructionDto,
                Seller = seller,
                Products = products
            };
            return dto;
        }
    }
}
