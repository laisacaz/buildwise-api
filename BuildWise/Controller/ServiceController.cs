using BuildWise.Payload.Sale;
using BuildWise.Payload.Service;
using BuildWise.Services.Command.Sale;
using BuildWise.Services.Command.Service;
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
    [FromBody] ServiceInsertPayload payload)

    {
        ServiceInsertCommand command = new ServiceInsertCommand(payload);
        int id = await _mediator.Send(command);
        return Ok(id);
    }
}
