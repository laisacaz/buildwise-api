using AutoMapper;
using BuildWise.Payload.Cashier;
using BuildWise.Payload.Construction;

namespace BuildWise.AutoMapper.Cashier
{
    public class CashierMap : Profile
    {
        public CashierMap()
        {
            CreateMap<CashierOpenPayload, Entities.Cashier>();
            CreateMap<CashierClosePayload, Entities.Cashier>();
        }
    }
}
