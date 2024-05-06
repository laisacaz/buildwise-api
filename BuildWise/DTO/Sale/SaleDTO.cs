using BuildWise.DTO.Construction;
using BuildWise.DTO.Person;
using BuildWise.DTO.Product;

namespace BuildWise.DTO.Sale
{
    public class SaleDTO
    {
        public PersonSimpleDTO Client { get; set; }
        public PersonSimpleDTO Seller { get; set; }
        public ConstructionDTO Construction { get; set; }
        public SaleRecordDTO Sale { get; set; } 
        public List<SaleProductDTO> Products { get; set; } 
    }
}
