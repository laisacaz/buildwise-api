using Dapper.FluentMap.Dommel.Mapping;

namespace BuildWise.DbMapper.Cashier
{
    public class CashierMapper : DommelEntityMap<Entities.Cashier>
    {
        public CashierMapper()
        {
            ToTable("ca_cashier");
            Map(x => x.Id)
                .ToColumn("id", false)
                .IsKey();

            Map(x => x.CreatedAt).ToColumn("created_at", false);
            Map(x => x.ClosedAt).ToColumn("closed_at", false);
            Map(x => x.Status).ToColumn("status", false);
            Map(x => x.InitialValue).ToColumn("initial_value", false);
            Map(x => x.ClosureValue).ToColumn("closure_value", false);
            Map(x => x.AmountAvailable).ToColumn("actual_money", false);
            Map(x => x.Outs).ToColumn("out", false);
            Map(x => x.Entries).ToColumn("entry", false);
        }
    }
}
