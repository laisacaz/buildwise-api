using BuildWise.DTO.Report.Product;
using BuildWise.DTO.Report.Service;
using BuildWise.Interfaces.Repository;
using BuildWise.Services.Query.Product;
using MediatR;

namespace BuildWise.Services.Query.ServiceOrder
{
    public class ServiceRankingQuery : IRequest<List<ServiceRankingDTO>>
    {
        public ServiceRankingQuery()
        {
            
        }
    }
    internal class ServiceRankingQueryHandler : IRequestHandler<ServiceRankingQuery, List<ServiceRankingDTO>>
    {
        private readonly IUnitOfWork _uow;
        public ServiceRankingQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<ServiceRankingDTO>> Handle(ServiceRankingQuery request, CancellationToken cancellationToken)
        {
            List<ServiceRankingDTO> services = await _uow.Sale.ServiceOrder.GetRankingServices();

            foreach (ServiceRankingDTO service in services)
            {
                service.Total = service.SaledTimes * service.Price;
            }

            return services;
        }
    }
}
