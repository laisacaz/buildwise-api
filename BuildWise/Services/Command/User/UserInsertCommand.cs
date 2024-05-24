using AutoMapper;
using BuildWise.Extensions;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.ServiceOrder;
using BuildWise.Payload.User;
using FluentValidation;
using MediatR;

namespace BuildWise.Services.Command.User
{
    public class UserInsertCommand : IRequest<int>
    {
        public UserInsertCommand(UserInsertPayload payload)
        {

            Payload = payload;
        }
        public UserInsertPayload Payload { get; set; }
    }
    internal class UserInsertCommandHandler : IRequestHandler<UserInsertCommand, int> 
    {
        private readonly IPublicUnitOfWork _puow;
        private readonly IMapper _mapper;
        private readonly IValidator<UserInsertPayload> _validator;
        public UserInsertCommandHandler(
            IPublicUnitOfWork puow,
            IMapper mapper,
            IValidator<UserInsertPayload> validator)
        {
            _puow = puow;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<int> Handle(UserInsertCommand request, CancellationToken cancellationToken)
        {
            FluentValidation.Results.ValidationResult result = await _validator.ValidateAsync(request.Payload);
            result.ThrowExceptionIfNotValid();

            Entities.User user = _mapper.Map<Entities.User>(request.Payload);
            user.CreatedAt = DateTime.UtcNow;
            int id = await _puow.User.InsertAsync(user);
            return id;
        }
    }
}
