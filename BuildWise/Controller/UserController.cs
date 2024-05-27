using BuildWise.Payload.ServiceOrder;
using BuildWise.Payload.User;
using BuildWise.Services.Command.Service;
using BuildWise.Services.Command.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuildWise.Controller
{

    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<int>> Insert(
            [FromBody] UserInsertPayload payload)
        {
            UserInsertCommand command = new UserInsertCommand(payload);
            int id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult> GetById()
        {

        }
    }
}
