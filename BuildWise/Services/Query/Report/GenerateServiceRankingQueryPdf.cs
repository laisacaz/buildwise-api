using BuildWise.DTO.Report;
using BuildWise.Interfaces.Service.Report;
using BuildWise.Services.Query.Product;
using BuildWise.Services.Query.ServiceOrder;
using MediatR;
using static BuildWise.Enums.ReportEnum;

namespace BuildWise.Services.Query.Report
{
    public class GenerateServiceRankingQueryPdf : IRequest<PdfDTO>
    {
        public GenerateServiceRankingQueryPdf()
        {
            
        }
    }
    internal class GenerateServiceRankingQueryPdfHandler : IRequestHandler<GenerateServiceRankingQueryPdf, PdfDTO>
    {
        private readonly IMediator _mediator;
        private readonly IReportService _generatePDFFileService;
        public GenerateServiceRankingQueryPdfHandler(
            IMediator mediator,
            IReportService generatePDFFileService)
        {
            _mediator = mediator;
            _generatePDFFileService = generatePDFFileService;
        }
        public async Task<PdfDTO> Handle(GenerateServiceRankingQueryPdf request, CancellationToken cancellationToken)
        {
            var data = await _mediator.Send(new ServiceRankingQuery());

            PdfDTO dto = await _generatePDFFileService.GeneratePDF(
               reportType: EReportType.ServiceOrderRanking,
               data: data);

            return dto;
        }
    }
}
