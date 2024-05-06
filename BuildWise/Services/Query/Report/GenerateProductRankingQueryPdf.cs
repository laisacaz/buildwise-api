using BuildWise.DTO.Report;
using BuildWise.Interfaces.Service.Report;
using BuildWise.Payload.Product;
using BuildWise.Services.Query.Product;
using MediatR;
using static BuildWise.Enums.ReportEnum;

namespace BuildWise.Services.Query.Report
{
    public class GenerateProductRankingQueryPdf : IRequest<PdfDTO>
    {
        public GenerateProductRankingQueryPdf()
        {
        }
    }
    internal class GenerateProductRankingQueryPdfHandler : IRequestHandler<GenerateProductRankingQueryPdf, PdfDTO>
    {
        private readonly IMediator _mediator;
        private readonly IReportService _generatePDFFileService;
        public GenerateProductRankingQueryPdfHandler(
            IMediator mediator,
            IReportService generatePDFFileService)
        {
            _mediator = mediator;
            _generatePDFFileService = generatePDFFileService;
        }
        public async Task<PdfDTO> Handle(GenerateProductRankingQueryPdf request, CancellationToken cancellationToken)
        {
            var data = await _mediator.Send(new ProductRankingQuery());

            PdfDTO dto = await _generatePDFFileService.GeneratePDF(
               reportType: EReportType.ProductRanking,
               data: data);

            return dto;
        }
    }
}
