using BuildWise.DTO.Report;
using BuildWise.Interfaces.Service.Report;
using BuildWise.Payload.Cashier;
using BuildWise.Payload.Report;
using BuildWise.Services.Query.Cashier;
using MediatR;
using static BuildWise.Enums.ReportEnum;

namespace BuildWise.Services.Query.Report
{
    public class GenerateCashierPdfQuery : IRequest<PdfDTO>
    {
        public GenerateCashierPdfQuery(int id)
        {
            Payload = new GenerateCashierPdfPayload { Id = id };
        }
        public GenerateCashierPdfPayload Payload { get; set; }
    }
    internal class GenerateCashierPdfQueryHandler : IRequestHandler<GenerateCashierPdfQuery , PdfDTO>
    {
        private readonly IMediator _mediator;
        private readonly IReportService _generatePDFFileService;

        public GenerateCashierPdfQueryHandler(
            IMediator mediator,
            IReportService generatePDFFileService)
        {
            _mediator = mediator;
            _generatePDFFileService = generatePDFFileService;
        }
        public async Task<PdfDTO> Handle(GenerateCashierPdfQuery request, CancellationToken cancellationToken)
        {
            var data = await _mediator.Send(new CashierGetValuesQuery(request.Payload.Id));

            PdfDTO dto = await _generatePDFFileService.GeneratePDF(
               reportType: EReportType.Cashier,
               data: data);

            return dto;
        }
    }
}
