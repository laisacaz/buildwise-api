using BuildWise.DTO;
using BuildWise.DTO.Person;
using BuildWise.Payload.Person;
using BuildWise.Services.Command.Person;
using BuildWise.Services.Command.Product;
using BuildWise.Services.Query.Person;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuildWise.Controller
{
    [Route("person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<int>> Insert(
            [FromBody] PersonInsertPayload payload)
        {
            PersonInsertCommand command = new PersonInsertCommand(payload);
            int id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> Update(
            [FromRoute(Name = "id")] int personId,
            [FromBody] PersonUpdatePayload payload)
        {
            PersonUpdateCommand command = new PersonUpdateCommand(personId, payload);
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<PersonDTO>> GetById(
            [FromRoute(Name = "id")] int personId)
        {
            PersonGetByIdQuery query = new PersonGetByIdQuery(personId);
            PersonDTO person = await _mediator.Send(query);
            return Ok(person);
        }


        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<BasePagedSearchDTO<PersonPagedSearchDTO>>> Search(
            [FromQuery] PersonPagedSearchPayload payload)
        {
            PersonPagedSearchQuery query = new PersonPagedSearchQuery(payload);
            BasePagedSearchDTO<PersonPagedSearchDTO> person = await _mediator.Send(query);
            return Ok(person);
        }

        [HttpPut]
        [Route("delete/{id:int}")]
        public async Task<ActionResult> Delete(
            [FromRoute(Name = "id")] int personId)
        {
            PersonDeleteCommand command = new PersonDeleteCommand(personId);
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        [Route("actives/simple")]
        public async Task<ActionResult<List<PersonSimpleDTO>>> GetActivesSimple()
        {
            PersonActivesSimpleGetQuery query = new PersonActivesSimpleGetQuery();
            List<PersonSimpleDTO> person = await _mediator.Send(query);
            return Ok(person);
        }

        [HttpGet]
        [Route("simple")]
        public async Task<ActionResult<List<PersonSimpleDTO>>> GetSimple()
        {
            PersonSimpleGetQuery query = new PersonSimpleGetQuery();
            List<PersonSimpleDTO> person = await _mediator.Send(query);
            return Ok(person);
        }
    }
}
