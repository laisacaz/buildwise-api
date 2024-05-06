using AutoMapper;
using BuildWise.Extensions;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Product;
using FluentValidation;
using MediatR;

namespace BuildWise.Services.Command.Product
{
    public class ProductUpdateCommand : IRequest<Unit>
    {
        public ProductUpdateCommand(
            int id,
            ProductUpdatePayload payload)
        {
            Payload = payload;
            Payload.SetId(id);
        }
        public ProductUpdatePayload Payload { get; set; }
    }
    internal class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<ProductUpdatePayload> _validator;

        public ProductUpdateCommandHandler(
            IUnitOfWork uow,
            IMapper mapper,
            IValidator<ProductUpdatePayload> validator)
        {
            _uow = uow;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<Unit> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            FluentValidation.Results.ValidationResult result = await _validator.ValidateAsync(request.Payload);
            result.ThrowExceptionIfNotValid();

            Entities.Product oldProduct = await _uow.Product.GetByIdAsync(request.Payload.GetId());
            Entities.Product product = _mapper.Map<Entities.Product>(request.Payload);
            product.CreatedAt = oldProduct.CreatedAt;
            product.Status = true;

            await _uow.Product.UpdateAsync(product);

            return Unit.Value;
        }
    }
}
