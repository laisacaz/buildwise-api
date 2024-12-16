using BuildWise.Entities;
using BuildWise.Payload.Address;

namespace BuildWise.Payload.Person
{
    public class PersonInsertPayload
    {        
        public string? Name { get; set; }
        public string? IdentityNumber { get; set; }
        public string? SocialSecurityNumber { get; set; }
        public string? Cellphone { get; set; }
        public AddressInsertPayload? Address { get; set; }
    }
}
