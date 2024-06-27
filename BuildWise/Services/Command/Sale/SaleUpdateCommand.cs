using AutoMapper;
using BuildWise.AutoMapper.Product;
using BuildWise.DTO.Product;
using BuildWise.DTO.Sale;
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

            // resultado da busca de produtos e serviços que estao vinculadas com a venda no banco
            // utiliza do dto com varias props pois o sql eh usado no getbyid da venda para mostrar no data table
            List<SaleProductDTO> existingProducts = await _uow.Sale.Product.GetSaleFullProducts(
                oldSale.Status,
                request.Payload.GetId());

            List<SaleServiceOrderDTO> existingServices = await _uow.Sale.ServiceOrder.GetSaleFullServiceOrders(
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
            foreach(SaleServiceOrderDTO service in existingServices)
            {
                SaleServiceOrderUpdatePayload? serviceStillAdded = request.Payload.Services.Where(
                    x => x.ServiceId == service.ServiceId).FirstOrDefault();

                if(serviceStillAdded is null)
                {
                    SaleServiceOrder toBeDeleted = new()
                    {
                        Id = service.Id,
                        ServiceId = service.ServiceId,
                        StockQuantity = service.StockQuantitySale,                        
                    };
                    await _uow.Sale.ServiceOrder.DeleteAsync(toBeDeleted);
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
            foreach (SaleServiceOrderUpdatePayload service in request.Payload.Services)
            {
                // cria objeto com valores que vieram do payload
                SaleServiceOrder serviceOrderMap = new SaleServiceOrder()
                {
                    ServiceId = service.ServiceId,
                    StockQuantity = service.StockQuantity
                };
                Entities.ServiceOrder fullServiceOrder = await _uow.ServiceOrder.GetByIdAsync(service.ServiceId);

                // verifica se ja foi adicionado
                SaleServiceOrderDTO? serviceAlreadyAdded = existingServices.Where(
                    x => x.ServiceId == service.ServiceId).FirstOrDefault();

                // já foi adicionado
                // no objeto criado lá em cima, vai adicionar o numero da venda e id do item antigo
                // precisou passar esses valores para entao atualizar

                if (serviceAlreadyAdded is not null)
                {
                    serviceOrderMap.SaleId = request.Payload.GetId();
                    serviceOrderMap.Id = serviceAlreadyAdded.Id;
                    await _uow.Sale.ServiceOrder.UpdateAsync(serviceOrderMap);
                }
                // caso contrario vai inserir um novo
                else
                {
                    serviceOrderMap.CreatedAt = DateTime.UtcNow;
                    serviceOrderMap.SaleId = request.Payload.GetId();
                    await _uow.Sale.ServiceOrder.InsertAsync(serviceOrderMap);
                }
                totalPrice += fullServiceOrder.Price * service.StockQuantity;
                sale.TotalItems += service.StockQuantity;
            }
            sale.Subtotal = totalPrice;
            sale.Total = sale.Subtotal;
            sale.CreatedAt = oldSale.CreatedAt;

            await _uow.Sale.UpdateAsync(sale);

            return Unit.Value;
        }
    }

}
