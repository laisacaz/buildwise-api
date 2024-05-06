using BuildWise.DTO.Report;
using BuildWise.Interfaces.Repository;
using BuildWise.Interfaces.Service.Report;
using BuildWise.Payload.Product;
using BuildWise.Payload.Report;
using BuildWise.Services.Query.Sale;
using MediatR;
using static BuildWise.Enums.ReportEnum;

namespace BuildWise.Services.Query.Report
{
    public class GenerateSalePdfQuery : IRequest<PdfDTO>
    {
        public GenerateSalePdfQuery(int id)
        {
            Payload = new GenerateSalePdfPayload { Id = id };
        }
        public GenerateSalePdfPayload Payload { get; set; }
    }
    internal class GenerateSalePdfQueryHandler : IRequestHandler<GenerateSalePdfQuery, PdfDTO>
    {
        private readonly IMediator _mediator;
        private readonly IReportService _generatePDFFileService;
        public GenerateSalePdfQueryHandler(
            IMediator mediator,
            IReportService generatePDFFileService)
        {
            _mediator = mediator;
            _generatePDFFileService = generatePDFFileService;
        }

        public async Task<PdfDTO> Handle(GenerateSalePdfQuery request, CancellationToken cancellationToken)
        {
            var data = await _mediator.Send(new SaleGetByIdQuery(request.Payload.Id));

            PdfDTO dto = await _generatePDFFileService.GeneratePDF(
               reportType: EReportType.Sale,
               data: data);

            return dto;
        }
    }

}
