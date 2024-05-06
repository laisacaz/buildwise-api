using AutoMapper;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Construction;
using FluentValidation;
using MediatR;
using static BuildWise.Enum.ConstructionEnum;

namespace BuildWise.Services.Command.Construction
{
    public class ConstructionFinishCommand : IRequest<Unit>
    {
        public ConstructionFinishCommand(int id)
        {
            Payload = new ConstructionFinishPayload { Id = id };
        }
        public ConstructionFinishPayload Payload { get; set; }
    }
    internal class ConstructionFinishCommandHandler : IRequestHandler<ConstructionFinishCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<ConstructionFinishPayload> _validator;

        public ConstructionFinishCommandHandler(
            IUnitOfWork uow,
            IValidator<ConstructionFinishPayload> validator)
        {
            _uow = uow;
            _validator = validator;
        }
        public async Task<Unit> Handle(ConstructionFinishCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request.Payload);

            Entities.Construction construction = await _uow.Construction.GetByIdAsync(request.Payload.Id);
            construction.Status = EStatusConstruction.Finalized;
            construction.FinalizedAt = DateTime.UtcNow;

            await _uow.Construction.UpdateAsync(construction);

            return Unit.Value;
        }
    }
}
