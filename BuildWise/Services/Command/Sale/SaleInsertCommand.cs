using AutoMapper;
using BuildWise.AutoMapper.Product;
using BuildWise.Entities;
using BuildWise.Extensions;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Sale;
using FluentValidation;
using MediatR;
using static BuildWise.Enum.SaleEnum;

namespace BuildWise.Services.Command.Sale
{
    public class SaleInsertCommand : IRequest<int>
    {
        public SaleInsertCommand(SaleInsertPayload payload)
        {
            Payload = payload;
        }
        public SaleInsertPayload Payload { get; set; }
    }
    internal class SaleInsertCommandHandler : IRequestHandler<SaleInsertCommand, int>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<SaleInsertPayload> _validator;
        public SaleInsertCommandHandler(
            IUnitOfWork uow,
            IMapper mapper,
            IValidator<SaleInsertPayload> validator)
        {
            _uow = uow;
            _mapper= mapper;
            _validator = validator;
        }

        public async Task<int> Handle(SaleInsertCommand request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request.Payload);
            result.ThrowExceptionIfNotValid();

            Entities.Sale sale = _mapper.Map<Entities.Sale>(request.Payload);
            sale.CreatedAt = DateTime.UtcNow;
            sale.Status = ESaleStatus.Open;

                if(sale.ConstructionId <= 0)
                {
                    sale.ConstructionId = null;
                }

                foreach(SaleProductInsertPayload product in request.Payload.Products)
                {
                    sale.TotalItems = sale.TotalItems + product.StockQuantity;
                    Entities.Product fullProduct = await _uow.Product.GetByIdAsync(product.ProductId);
                    sale.Subtotal = sale.Subtotal + (fullProduct.Price * product.StockQuantity);
                }
                sale.Total = sale.Subtotal;

                int saleId = await _uow.Sale.InsertAsync(sale);
                
                foreach (SaleProductInsertPayload product in request.Payload.Products)
                {
                    SaleProduct productMap = _mapper.Map<SaleProduct>(product);
                    productMap.CreatedAt = DateTime.UtcNow;
                    productMap.SaleId = saleId;
                    await _uow.Sale.Product.InsertAsync(productMap);
                }

            return saleId;
        }
    }
}

