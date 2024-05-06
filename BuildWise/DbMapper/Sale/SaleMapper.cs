using BuildWise.Entities;
using Dapper.FluentMap.Dommel.Mapping;

namespace BuildWise.DbMapper.Sale
{
    public class SaleMapper : DommelEntityMap<Entities.Sale>
    {
        public SaleMapper()
        {
            ToTable("sale");
            Map(x => x.Id)
                .ToColumn("id", false)
                .IsKey();

            Map(x => x.CreatedAt).ToColumn("created_at", false);
            Map(x => x.CanceledAt).ToColumn("canceled_at", false);
            Map(x => x.ReceivedAt).ToColumn("received_at", false);
            Map(x => x.Subtotal).ToColumn("subtotal", false);
            Map(x => x.Total).ToColumn("total", false);
            Map(x => x.Observation).ToColumn("observation", false);
            Map(x => x.ClientId).ToColumn("id_client", false);
            Map(x => x.SellerId).ToColumn("id_seller", false);
            Map(x => x.ConstructionId).ToColumn("id_construction", false);
            Map(x => x.Discount).ToColumn("discount", false);
            Map(x => x.FinalizedAt).ToColumn("finalized_at", false);
            Map(x => x.Increase).ToColumn("increase", false);
            Map(x => x.ReceivementMethod).ToColumn("receivement_method", false);            
            Map(x => x.Status).ToColumn("status", false);            
            Map(x => x.TotalItems).ToColumn("total_items", false);            
            Map(x => x.PaidAmount).ToColumn("paid_amount", false);            
        }
    }
}
