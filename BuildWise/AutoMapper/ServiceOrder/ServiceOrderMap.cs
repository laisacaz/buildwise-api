using AutoMapper;
using BuildWise.Payload.Construction;
using BuildWise.Payload.ServiceOrder;

namespace BuildWise.AutoMapper.ServiceOrder
{
    public class ServiceOrderMap : Profile
    {
        public ServiceOrderMap()
        {
            CreateMap<ServiceOrderInsertPayload, Entities.ServiceOrder>();
            CreateMap<ServiceOrderUpdatePayload, Entities.ServiceOrder>();
        }
    }
}
