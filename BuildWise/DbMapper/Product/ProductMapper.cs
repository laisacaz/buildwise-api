using Dapper.FluentMap.Dommel.Mapping;

namespace BuildWise.Mapper.Product
{
    public class ProductMapper : DommelEntityMap<Entities.Product>
    {
        public ProductMapper()
        {
            ToTable("pro_product");
            Map(x => x.Id)
                .ToColumn("id", false)
                .IsKey();

            Map(x => x.Description).ToColumn("description", false);
            Map(x => x.Reference).ToColumn("reference", false);
            Map(x => x.BarCode).ToColumn("barcode", false);
            Map(x => x.StockQuantity).ToColumn("stock_quantity", false);
            Map(x => x.CreatedAt).ToColumn("created_at", false);
            Map(x => x.Status).ToColumn("status", false);
            Map(x => x.Price).ToColumn("price", false);
            Map(x => x.Cost).ToColumn("cost", false);

        }
    }
}
