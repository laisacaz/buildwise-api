using BuildWise.DTO.Address;

namespace BuildWise.DTO.Person
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string IdentityNumber { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string Cellphone { get; set; }
        public AddressDTO Address { get; set; }
    }
}
