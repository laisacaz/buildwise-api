using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Construction;
using FluentValidation;
using MediatR;
using static BuildWise.Enum.ConstructionEnum;

namespace BuildWise.Services.Command.Construction
{
    public class ConstructionDeleteCommand : IRequest<Unit>
    {
        public ConstructionDeleteCommand(int id)
        {
            Payload = new ConstructionDeletePayload { Id = id };
        }
        public ConstructionDeletePayload Payload { get; set; }
    }
    internal class ConstructionDeleteCommandHandler : IRequestHandler<ConstructionDeleteCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<ConstructionDeletePayload> _validator;

        public ConstructionDeleteCommandHandler(
            IUnitOfWork uow,
             IValidator<ConstructionDeletePayload> validator)
        {
            _uow = uow;
            _validator = validator;
        }
        public async Task<Unit> Handle(ConstructionDeleteCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request.Payload);

            Entities.Construction construction = await _uow.Construction.GetByIdAsync(request.Payload.Id);
            construction.Status = EStatusConstruction.Canceled;
            construction.CanceledAt = DateTime.UtcNow;

            await _uow.Construction.UpdateAsync(construction);

            return Unit.Value;
        }
    }
}
