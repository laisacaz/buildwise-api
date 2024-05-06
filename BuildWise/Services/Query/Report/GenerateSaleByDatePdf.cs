using BuildWise.DTO.Report;
using BuildWise.Interfaces.Service.Report;
using BuildWise.Payload.Report;
using MediatR;
using static BuildWise.Enums.ReportEnum;

namespace BuildWise.Services.Query.Report
{
    public class GenerateSaleByDatePdf : IRequest<PdfDTO>
    {
        public GenerateSaleByDatePdf(SaleByDateReportPayload payload)
        {
            Payload = payload;
        }
        public SaleByDateReportPayload Payload { get; set; }
    }
    internal class GenerateSaleByDatePdfHandler : IRequestHandler<GenerateSaleByDatePdf, PdfDTO>
    {
        private readonly IMediator _mediator;
        private readonly IReportService _generatePDFFileService;
        public GenerateSaleByDatePdfHandler(
            IMediator mediator,
            IReportService generatePDFFileService)
        {
            _mediator = mediator;
            _generatePDFFileService = generatePDFFileService;
        }
        public async Task<PdfDTO> Handle(GenerateSaleByDatePdf request, CancellationToken cancellationToken)
        {
            var data = await _mediator.Send(new SaleByDateQuery(request.Payload));

            PdfDTO dto = await _generatePDFFileService.GeneratePDF(
               reportType: EReportType.SaleByDate,
               data: data);

            return dto;
        }
    }
}
