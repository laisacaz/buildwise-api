using Dapper.FluentMap.Dommel.Mapping;

namespace BuildWise.DbMapper.Service
{
    public class ServiceOrderMapper : DommelEntityMap<Entities.ServiceOrder>
    {
        public ServiceOrderMapper()
        {
            ToTable("se_service");
            Map(x => x.Id)
                .ToColumn("id", false)
                .IsKey();

            Map(x => x.Description).ToColumn("description", false);
            Map(x => x.CreatedAt).ToColumn("created_at", false);
            Map(x => x.Status).ToColumn("status", false);
            Map(x => x.Price).ToColumn("price", false);            
        }
    }
}
