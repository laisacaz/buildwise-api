using AutoMapper;
using BuildWise.DTO.Construction;
using BuildWise.Entities;
using BuildWise.Payload.Construction;
using BuildWise.Payload.Person;

namespace BuildWise.AutoMapper.Construction
{
    public class ConstructionMap : Profile
    {
        public ConstructionMap()
        {
            CreateMap<ConstructionInsertPayload, Entities.Construction>();
            CreateMap<ConstructionUpdatePayload, Entities.Construction>();
            CreateMap<Entities.Construction, ConstructionDTO>();
        }
    }
}
