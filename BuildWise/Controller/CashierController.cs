using BuildWise.Payload.Cashier;
using BuildWise.Services.Command.Cashier;
using BuildWise.Services.Command.Construction;
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

        //[HttpPut]
        //[Route("")]
        //public async Task<ActionResult> CloseCashier(
        //   [FromBody] CashierClosePayload payload)
        //{
        //    CashierCloseCommand command = new CashierCloseCommand(payload);
        //    await _mediator.Send(command);
        //    return Ok();
        //}
    }
}
