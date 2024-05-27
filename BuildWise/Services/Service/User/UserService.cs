using BuildWise.Interfaces.Repository.User;
using BuildWise.Interfaces.Service.User;

namespace BuildWise.Services.Service.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
    }
}
