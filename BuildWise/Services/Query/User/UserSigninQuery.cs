using BuildWise.Extensions;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.User;
using FluentValidation;
using MediatR;

namespace BuildWise.Services.Query.User
{
    public class UserSigninQuery : IRequest<Entities.User?>
    {
        public UserSigninQuery(UserSigninPayload payload)
        {
            Payload = payload;
        }
        public UserSigninPayload Payload { get; set; }
    }
    internal class UserGetByIdQueryHandler : IRequestHandler<UserSigninQuery, Entities.User?>
    {
        private readonly IPublicUnitOfWork _puow;
        private readonly IValidator<UserSigninPayload> _validator;
        public UserGetByIdQueryHandler(
            IPublicUnitOfWork puow,
            IValidator<UserSigninPayload> validator)
        {
            _puow = puow;
            _validator = validator;
        }
        public async Task<Entities.User?> Handle(UserSigninQuery request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request.Payload);
            result.ThrowExceptionIfNotValid();

            Entities.User? user = await _puow.User.GetUserByEmailAndPassword(request.Payload.Email, request.Payload.Password);
            
            return user ?? null;
        }
    }
}
