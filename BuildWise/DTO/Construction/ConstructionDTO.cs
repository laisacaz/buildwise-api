using BuildWise.DTO.Address;
using BuildWise.DTO.Person;
using static BuildWise.Enum.ConstructionEnum;

namespace BuildWise.DTO.Construction
{
    public class ConstructionDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ClientId { get; set; }
        public PersonSimpleDTO Client { get; set; }
        public string Observation { get; set; }
        public EStatusConstruction Status { get; set; }
        public AddressDTO? Address { get; set; }
    }
}
