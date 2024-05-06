using BuildWise.Entities;
using Dapper.FluentMap.Dommel.Mapping;

namespace BuildWise.DbMapper.Sale
{
    public class SaleProductMapper : DommelEntityMap<SaleProduct>
    {
        public SaleProductMapper()
        {
            ToTable("sale_product");
            Map(x => x.Id)
                .ToColumn("id", false)
                .IsKey();

            Map(x => x.CreatedAt).ToColumn("created_at", false);
            Map(x => x.StockQuantity).ToColumn("stock_quantity", false);
            Map(x => x.ProductId).ToColumn("id_product", false);
            Map(x => x.SaleId).ToColumn("id_sale", false);
        }
    }
}
