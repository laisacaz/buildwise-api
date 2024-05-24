using Dapper.FluentMap.Dommel.Mapping;

namespace BuildWise.DbMapper.User
{
    public class UserMapper : DommelEntityMap<Entities.User>
    {
        public UserMapper()
        {
            ToTable("user");
            Map(x => x.Id)
                .ToColumn("id", false)
                .IsKey();

            Map(x => x.Name).ToColumn("name", false);
            Map(x => x.Email).ToColumn("email", false);
            Map(x => x.Password).ToColumn("password", false);
            Map(x => x.RegisteredNumber).ToColumn("registered_number", false);
            Map(x => x.CreatedAt).ToColumn("created_at", false);
        }    
    }
}
