using AutoMapper;
using BuildWise.Payload.Product;

namespace BuildWise.AutoMapper.Product
{
    public class ProductMap : Profile
    {
        public ProductMap()
        {
            CreateMap<ProductInsertPayload, Entities.Product>(); 

            CreateMap<ProductUpdatePayload, Entities.Product>();
        }
    }
}
