using BuildWise.Payload.Sale;
using BuildWise.Services.Command.Sale;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuildWise.Controller
{
    [Route("service")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }
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
}
