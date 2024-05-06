using AutoMapper;
using BuildWise.Extensions;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Product;
using FluentValidation;
using MediatR;

namespace BuildWise.Services.Command.Product
{
    public class ProductInsertCommand : IRequest<int>
    {
        public ProductInsertCommand(ProductInsertPayload payload)
        {
            Payload = payload;
        }
        public ProductInsertPayload Payload { get; }
    }
    public class ProductInsertCommandHandler : IRequestHandler<ProductInsertCommand, int>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<ProductInsertPayload> _validator;
        public ProductInsertCommandHandler(
            IUnitOfWork uow,
            IMapper mapper,
            IValidator<ProductInsertPayload> validator)
        {
            _uow = uow;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<int> Handle(ProductInsertCommand request, CancellationToken cancellationToken)
        {
            FluentValidation.Results.ValidationResult result = await _validator.ValidateAsync(request.Payload);
            result.ThrowExceptionIfNotValid();

            Entities.Product product = _mapper.Map<Entities.Product>(request.Payload);
            product.Status = true;
            product.CreatedAt = DateTime.UtcNow;
            int id = await _uow.Product.InsertAsync(product);
            return id;
        }
    }
}
