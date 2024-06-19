//using BuildWise.Extensions;
//using BuildWise.Interfaces.Repository;
//using BuildWise.Payload.User;
//using FluentValidation;
//using MediatR;

//namespace BuildWise.Services.Query.User
//{
//    public class UserGetByIdQuery : IRequest<Entities.User>
//    {
//        public UserGetByIdQuery(UserGetByIdPayload payload)
//        {
//            Payload = payload;
//        }
//        public UserGetByIdPayload Payload { get; set; }
//    }
//    internal class UserGetByIdQueryHandler : IRequestHandler<UserGetByIdQuery, Entities.User>
//    {
//        private readonly IPublicUnitOfWork _puow;
//        private readonly IValidator<UserGetByIdPayload> _validator;
//        public UserGetByIdQueryHandler(
//            IPublicUnitOfWork puow,
//            IValidator<UserGetByIdPayload> validator)
//        {
//            _puow = puow;
//            _validator = validator;
//        }
//        public async Task<Entities.User> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
//        {
//            var result = await _validator.ValidateAsync(request.Payload);
//            result.ThrowExceptionIfNotValid();

//            Entities.User user = await _puow.User.GetByIdAsync(request.Payload);
//            return user;
//        }
//    }
//}
