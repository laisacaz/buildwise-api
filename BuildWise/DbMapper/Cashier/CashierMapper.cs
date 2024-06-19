using Dapper.FluentMap.Dommel.Mapping;

namespace BuildWise.DbMapper.Cashier
{
    public class CashierMapper : DommelEntityMap<Entities.Cashier>
    {
        public CashierMapper()
        {
            ToTable("con_construction");
            Map(x => x.Id)
                .ToColumn("id", false)
                .IsKey();

            Map(x => x.CreatedAt).ToColumn("created_at", false);
            Map(x => x.ClosedAt).ToColumn("closed_at", false);
            Map(x => x.Status).ToColumn("status", false);
            Map(x => x.InitialValue).ToColumn("initial_value", false);
            Map(x => x.ClosureValue).ToColumn("closure_value", false);
        }
    }
}
