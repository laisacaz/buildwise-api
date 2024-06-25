using BuildWise.DTO.Report.Product;
using BuildWise.Interfaces.Repository;
using MediatR;

namespace BuildWise.Services.Query.Product
{
    public class ProductRankingQuery : IRequest<List<ProductRankingDTO>>
    {
        public ProductRankingQuery()
        {

        }
    }
    internal class ProductRankingQueryHandler : IRequestHandler<ProductRankingQuery, List<ProductRankingDTO>>
    {
        private readonly IUnitOfWork _uow;
        public ProductRankingQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<ProductRankingDTO>> Handle(ProductRankingQuery request, CancellationToken cancellationToken)
        {
            List<ProductRankingDTO> products = await _uow.Sale.Product.GetRankingProducts();

            foreach (ProductRankingDTO product in products)
            {
                product.Total = product.SaledTimes * product.Price;
            }

            return products;
        }
    }
}
