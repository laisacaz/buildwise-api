using BuildWise.Extensions;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Product;
using FluentValidation;
using MediatR;

namespace BuildWise.Services.Query.Product
{
    public class ProductGetByIdQuery : IRequest<Entities.Product>
    {
        public ProductGetByIdQuery(int id)
        {
            Payload = new ProductGetByIdPayload { Id = id };
        }
        public ProductGetByIdPayload Payload { get; set; }
    }
    internal class ProductGetQueryHandler : IRequestHandler<ProductGetByIdQuery, Entities.Product>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<ProductGetByIdPayload> _validator;
        public ProductGetQueryHandler(
            IUnitOfWork uow,
            IValidator<ProductGetByIdPayload> validator)
        {
            _uow = uow;
            _validator = validator;
        }
        public async Task<Entities.Product> Handle(ProductGetByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request.Payload);
            result.ThrowExceptionIfNotValid();

            Entities.Product product = await _uow.Product.GetByIdAsync(request.Payload.Id);
            return product;
        }
    }
}
