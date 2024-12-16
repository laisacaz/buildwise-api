using BuildWise.Payload.Address;

namespace BuildWise.Payload.Person
{
    public class PersonUpdatePayload
    {
        private int id;
        public void SetId(int id) { this.id = id; }
        public int GetId() { return id; }
        public string Name { get; set; }
        public string? IdentityNumber { get; set; }
        public string? SocialSecurityNumber { get; set; }
        public string? Cellphone { get; set; }
        public AddressUpdatePayload Address { get; set; }
    }
}
