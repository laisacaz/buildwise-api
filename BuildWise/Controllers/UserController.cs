﻿using BuildWise.Entities;
using BuildWise.Payload.ServiceOrder;
using BuildWise.Payload.User;
using BuildWise.Services.Command.Service;
using BuildWise.Services.Command.User;
using BuildWise.Services.Query.Product;
using BuildWise.Services.Query.User;
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

        [HttpPost]
        [Route("signin")]
        public async Task<ActionResult> Signin(
            [FromBody] UserSigninPayload payload)
        {
            UserSigninQuery query = new UserSigninQuery(payload);
            Entities.User? user = await _mediator.Send(query);
            return Ok(user);
        }
    }
}
