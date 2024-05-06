using AutoMapper;
using BuildWise.DTO.Sale;
using BuildWise.Entities;
using BuildWise.Payload.Sale;

namespace BuildWise.AutoMapper.Sale
{
    public class SaleMap : Profile
    {
        public SaleMap()
        {
            CreateMap<SaleInsertPayload, Entities.Sale>();
            CreateMap<SaleUpdatePayload, Entities.Sale>();
            
            CreateMap<SaleProductInsertPayload, SaleProduct>();
            CreateMap<SaleProductUpdatePayload, SaleProduct>();
            
            CreateMap<Entities.Sale, SaleRecordDTO>();
        }
    }
}
