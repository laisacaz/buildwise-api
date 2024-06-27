using BuildWise.Entities;
using Dapper.FluentMap.Dommel.Mapping;

namespace BuildWise.DbMapper.Sale
{
    public class SaleServiceOrderMapper : DommelEntityMap<SaleServiceOrder>
    {
        public SaleServiceOrderMapper()
        {
            ToTable("sale_service");
            Map(x => x.Id)
                .ToColumn("id", false)
                .IsKey();

            Map(x => x.CreatedAt).ToColumn("created_at", false);
            Map(x => x.StockQuantity).ToColumn("stock_quantity", false);
            Map(x => x.ServiceId).ToColumn("id_service", false);
            Map(x => x.SaleId).ToColumn("id_sale", false);
        }
    }
}
