using AutoMapper;
using BuildWise.Extensions;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Product;
using FluentValidation;
using MediatR;

namespace BuildWise.Services.Command.Product
{
    public class ProductDeleteCommand : IRequest<Unit>
    {
        public ProductDeleteCommand(int id)
        {
            Payload = new ProductDeletePayload { Id = id };
        }
        public ProductDeletePayload Payload { get; set; }
    }
    internal class ProductDeleteCommandHandler : IRequestHandler<ProductDeleteCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<ProductDeletePayload> _validator;
        public ProductDeleteCommandHandler(
            IUnitOfWork uow,
            IValidator<ProductDeletePayload> validator)
        {
            _uow = uow;
            _validator = validator;
        }
        public async Task<Unit> Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request.Payload);
            result.ThrowExceptionIfNotValid();

            Entities.Product oldProduct = await _uow.Product.GetByIdAsync(request.Payload.Id);
            oldProduct.Status = false;

            await _uow.Product.UpdateAsync(oldProduct);

            return Unit.Value;
        }
    }
}
