using BuildWise.DTO.Report;
using BuildWise.Payload.Product;
using BuildWise.Payload.Report;
using BuildWise.Services.Query.Report;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuildWise.Controller
{
    [Route("report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("product-ranking")]
        public async Task<ActionResult<PdfDTO>> ProductRanking()
        {
            GenerateProductRankingQueryPdf query = new GenerateProductRankingQueryPdf();
            PdfDTO dto = await _mediator.Send(query);
            return File(dto.Content, dto.ContentType, dto.FileName);
        }

        [HttpGet]
        [Route("sale-by-period")]
        public async Task<ActionResult<PdfDTO>> SaleByPeriod(
            [FromQuery] SaleByDateReportPayload payload)
        {
            GenerateSaleByDatePdf query = new GenerateSaleByDatePdf(payload);
            PdfDTO dto = await _mediator.Send(query);
            return File(dto.Content, dto.ContentType, dto.FileName);
        }
    }
}
