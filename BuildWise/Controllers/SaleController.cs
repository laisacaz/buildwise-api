using BuildWise.DTO;
using BuildWise.DTO.Report;
using BuildWise.DTO.Sale;
using BuildWise.Payload.Construction;
using BuildWise.Payload.Sale;
using BuildWise.Services.Command.Construction;
using BuildWise.Services.Command.Sale;
using BuildWise.Services.Query.Construction;
using BuildWise.Services.Query.Report;
using BuildWise.Services.Query.Sale;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuildWise.Controller
{
    [Route("sale")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SaleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<int>> Insert(
            [FromBody] SaleInsertPayload payload)

        {
            SaleInsertCommand command = new SaleInsertCommand(payload);
            int id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> Update(
            [FromRoute(Name = "id")] int id,
            [FromBody] SaleUpdatePayload payload)
        {
            SaleUpdateCommand command = new SaleUpdateCommand(id, payload);
            await _mediator.Send(command);
            return Ok(id);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<SaleDTO>> GetById(
           [FromRoute(Name = "id")] int id)
        {
            SaleGetByIdQuery query = new SaleGetByIdQuery(id);
            SaleDTO sale = await _mediator.Send(query);
            return Ok(sale);
        }

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<BasePagedSearchDTO<SalePagedSearchDTO>>> Search(
           [FromQuery] SalePagedSearchPayload payload)
        {
            SalePagedSearchquery query = new SalePagedSearchquery(payload);
            BasePagedSearchDTO<SalePagedSearchDTO> sale = await _mediator.Send(query);
            return Ok(sale);
        }

        [HttpPut]
        [Route("delete/{id:int}")]
        public async Task<ActionResult> Delete(
            [FromRoute(Name = "id")] int saleId)
        {
            SaleDeleteCommand command = new SaleDeleteCommand(saleId);
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        [Route("finish/{id:int}")]
        public async Task<ActionResult<int>> Finish(
        [FromRoute(Name = "id")] int saleId,
        [FromBody] SaleFinishPayload payload)

        {
            SaleFinishCommand command = new SaleFinishCommand(saleId, payload);
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        [Route("report/{id:int}")]
        public async Task<ActionResult<PdfDTO>> Report(
           [FromRoute(Name = "id")] int saleId)
        {
            GenerateSalePdfQuery query = new GenerateSalePdfQuery(saleId);
            PdfDTO dto = await _mediator.Send(query);
            return File(dto.Content, dto.ContentType, dto.FileName);
        }
    }
}
