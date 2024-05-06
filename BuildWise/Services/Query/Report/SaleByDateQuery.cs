using BuildWise.DTO.Report.Product;
using BuildWise.DTO.Report.Sale;
using BuildWise.Extensions;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Report;
using FluentValidation;
using MediatR;

namespace BuildWise.Services.Query.Report
{
    public class SaleByDateQuery : IRequest<List<SaleByDateDTO>>
    {
        public SaleByDateQuery(SaleByDateReportPayload payload)
        {
            Payload = payload;
        }
        public SaleByDateReportPayload Payload { get; set; }
    }
    internal class SaleByDateQueryHandler : IRequestHandler<SaleByDateQuery, List<SaleByDateDTO>>
    {
        private readonly IUnitOfWork _uow;
        public SaleByDateQueryHandler(
            IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<SaleByDateDTO>> Handle(SaleByDateQuery request, CancellationToken cancellationToken)
        {

            List<SaleByDateDTO> sales = await _uow.Sale.GetAllByDate(
                request.Payload.StartDate,
                request.Payload.EndDate);
          
            return sales;
        }
    }
}
