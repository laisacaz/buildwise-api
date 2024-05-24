using AutoMapper;
using BuildWise.Payload.ServiceOrder;
using BuildWise.Payload.User;

namespace BuildWise.AutoMapper.User
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<UserInsertPayload, Entities.User>();
        }
    }
}
