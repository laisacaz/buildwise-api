using AutoMapper;
using BuildWise.Payload.Construction;
using BuildWise.Payload.Service;

namespace BuildWise.AutoMapper.ServiceOrder
{
    public class ServiceOrderMap : Profile
    {
        public ServiceOrderMap()
        {
            CreateMap<ServiceOrderInsertPayload, Entities.ServiceOrder>();
        }
    }
}
