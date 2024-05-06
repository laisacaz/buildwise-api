using BuildWise.Entities;
using Dapper.FluentMap.Dommel.Mapping;

namespace BuildWise.DbMapper.Person
{
    public class AddressMapper : DommelEntityMap<Address>
    {
        public AddressMapper()
        {
            ToTable("pe_address");
            Map(x => x.Id)
                .ToColumn("id", false)
                .IsKey();

            Map(x => x.StreetNumber).ToColumn("street_number", false);
            Map(x => x.City).ToColumn("city", false);
            Map(x => x.Street).ToColumn("street", false);
            Map(x => x.District).ToColumn("district", false);
            Map(x => x.CreatedAt).ToColumn("created_at", false);
            Map(x => x.State).ToColumn("state", false);
            Map(x => x.ZipCode).ToColumn("zipcode", false);
        }
    }
}
