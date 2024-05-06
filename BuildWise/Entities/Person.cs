namespace BuildWise.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
        public bool Status { get; set; }
        public string IdentityNumber { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string Cellphone { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
