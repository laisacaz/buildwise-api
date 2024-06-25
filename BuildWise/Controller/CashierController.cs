using BuildWise.DTO.Cashier;
using BuildWise.Payload.Cashier;
using BuildWise.Services.Command.Cashier;
using BuildWise.Services.Command.Construction;
using BuildWise.Services.Query.Cashier;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuildWise.Controller
{
    [Route("cashier")]
    [ApiController]
    public class CashierController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CashierController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<int>> OpenCashier(
            [FromBody] CashierOpenPayload payload)
        {
            CashierOpenCommand command = new CashierOpenCommand(payload);
            int id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> CloseCashier(
            [FromRoute(Name = "id")] int cashierId,
            [FromBody] CashierClosePayload payload)
        {
            CashierCloseCommand command = new CashierCloseCommand(cashierId, payload);
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<CashierGetValuesDTO>> GetValues(
           [FromRoute(Name = "id")] int cashierId)
        {
            CashierGetValuesQuery query = new CashierGetValuesQuery(cashierId);
            CashierGetValuesDTO dto = await _mediator.Send(query);
            return Ok(dto);
        }

        [HttpGet]
        [Route("check-last-cashier")]
        public async Task<ActionResult<bool>> IsLastCashierOpen()
        {
            CashierCheckLastCashierQuery query = new CashierCheckLastCashierQuery();
            CashierSimpleDTO dto = await _mediator.Send(query);
            return Ok(dto);
        }
    }
}
