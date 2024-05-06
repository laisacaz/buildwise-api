using Dapper.FluentMap.Dommel.Mapping;

namespace BuildWise.DbMapper.Person
{
    public class PersonMapper : DommelEntityMap<Entities.Person>
    {
        public PersonMapper()
        {
            ToTable("pe_person");
            Map(x => x.Id)
                .ToColumn("id", false)
                .IsKey();

            Map(x => x.Name).ToColumn("name", false);
            Map(x => x.IdentityNumber).ToColumn("identity_number", false);
            Map(x => x.Cellphone).ToColumn("cellphone", false);
            Map(x => x.SocialSecurityNumber).ToColumn("social_security_number", false);
            Map(x => x.CreatedAt).ToColumn("created_at", false);
            Map(x => x.AddressId).ToColumn("id_address", false);
            Map(x => x.Status).ToColumn("status", false);
        }
    }
}
