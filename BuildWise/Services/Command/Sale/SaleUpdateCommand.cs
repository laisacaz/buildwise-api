using AutoMapper;
using BuildWise.DTO.Product;
using BuildWise.Entities;
using BuildWise.Extensions;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Sale;
using FluentValidation;
using MediatR;
using static BuildWise.Enum.SaleEnum;

namespace BuildWise.Services.Command.Sale
{
    public class SaleUpdateCommand : IRequest<Unit>
    {
        public SaleUpdateCommand(int id, SaleUpdatePayload payload)
        {
            Payload = payload;
            payload.SetId(id);
        }
        public SaleUpdatePayload Payload { get; set; }
    }
    internal class SaleUpdateCommandHandler : IRequestHandler<SaleUpdateCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<SaleUpdatePayload> _validator;

        public SaleUpdateCommandHandler(
            IUnitOfWork uow,
            IMapper mapper,
            IValidator<SaleUpdatePayload> validator)
        {
            _uow = uow;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Unit> Handle(SaleUpdateCommand request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request.Payload);
            result.ThrowExceptionIfNotValid();

            Entities.Sale oldSale = await _uow.Sale.GetByIdAsync(request.Payload.GetId());

            Entities.Sale sale = _mapper.Map<Entities.Sale>(request.Payload);

            if (sale.ConstructionId <= 0)
            {
                sale.ConstructionId = null;
            }

            sale.CreatedAt = oldSale.CreatedAt;
            sale.Status = ESaleStatus.Open;
            decimal totalPrice = 0;

            List<SaleProductDTO> existingProducts = await _uow.Sale.Product.GetSaleFullProducts(
                oldSale.Status,
                request.Payload.GetId());

            // verificar se item ainda está na venda caso nao estiver deleta
            foreach (SaleProductDTO product in existingProducts)
            {
                SaleProductUpdatePayload? productStillAdded = request.Payload.Products.Where(
                    x => x.ProductId == product.ProductId).FirstOrDefault();

                if (productStillAdded is null)
                {
                    SaleProduct toBeDeleted = new()
                    {
                        Id = product.Id,
                        ProductId = product.ProductId,
                        StockQuantity = product.StockQuantitySale
                    };                    
                    await _uow.Sale.Product.DeleteAsync(toBeDeleted);
                }

            }
            // atualiza produto ou insere novo
            foreach (SaleProductUpdatePayload product in request.Payload.Products)
            {
                SaleProduct productMap = new SaleProduct()
                {
                    ProductId = product.ProductId,
                    StockQuantity= product.StockQuantity
                };
                Entities.Product fullProduct = await _uow.Product.GetByIdAsync(product.ProductId);

                SaleProductDTO? productAlreadyAdded = existingProducts.Where(
                    x => x.ProductId == product.ProductId).FirstOrDefault();

                if (productAlreadyAdded is not null) {
                    productMap.SaleId = request.Payload.GetId();
                    productMap.Id = productAlreadyAdded.Id;
                    await _uow.Sale.Product.UpdateAsync(productMap);
                }
                else 
                {
                    productMap.CreatedAt = DateTime.UtcNow;
                    productMap.SaleId = request.Payload.GetId();                   
                    await _uow.Sale.Product.InsertAsync(productMap);
                }
                totalPrice += fullProduct.Price * product.StockQuantity;
                sale.TotalItems += product.StockQuantity;
            }
            sale.Subtotal = totalPrice;
            sale.Total = sale.Subtotal;
            sale.CreatedAt = oldSale.CreatedAt;

            await _uow.Sale.UpdateAsync(sale);

            return Unit.Value;
        }
    }

}
