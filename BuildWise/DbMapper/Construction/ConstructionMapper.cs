using BuildWise.Entities;
using Dapper.FluentMap.Dommel.Mapping;

namespace BuildWise.DbMapper.Construction
{
    public class ConstructionMapper : DommelEntityMap<Entities.Construction>
    {
        public ConstructionMapper()
        {
            ToTable("con_construction");
            Map(x => x.Id)
                .ToColumn("id", false)
                .IsKey();

            Map(x => x.CreatedAt).ToColumn("created_at", false);
            Map(x => x.FinalizedAt).ToColumn("finalized_at", false);
            Map(x => x.CanceledAt).ToColumn("canceled_at", false);
            Map(x => x.Status).ToColumn("status", false);
            Map(x => x.Observation).ToColumn("observation", false);
            Map(x => x.ClientId).ToColumn("id_client", false);            
            Map(x => x.AddressId).ToColumn("id_address", false);            
        }
    }
}
