using BuildWise.DTO;
using BuildWise.DTO.Construction;
using BuildWise.DTO.Person;
using BuildWise.Entities;
using BuildWise.Payload.Construction;
using BuildWise.Services.Command.Construction;
using BuildWise.Services.Command.Person;
using BuildWise.Services.Query.Construction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuildWise.Controller
{
    [Route("construction")]
    [ApiController]
    public class ConstructionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConstructionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<int>> Insert(
            [FromBody] ConstructionInsertPayload payload)
        {
            ConstructionInsertCommand command = new ConstructionInsertCommand(payload);
            int id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> Update(
            [FromRoute(Name = "id")] int id,
            [FromBody] ConstructionUpdatePayload payload)
        {
            ConstructionUpdateCommand command = new ConstructionUpdateCommand(id, payload);
            await _mediator.Send(command);
            return Ok(id);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<ConstructionDTO>> GetById(
           [FromRoute(Name = "id")] int id)
        {
            ConstructionGetByIdQuery query = new ConstructionGetByIdQuery(id);
            ConstructionDTO construction = await _mediator.Send(query);
            return Ok(construction);
        }

        [HttpPut]
        [Route("finish/{id:int}")]
        public async Task<ActionResult> Finish(
           [FromRoute(Name = "id")] int constructionId)
        {
            ConstructionFinishCommand command = new ConstructionFinishCommand(constructionId);
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<BasePagedSearchDTO<ConstructionPagedSearchDTO>>> Search(
            [FromQuery] ConstructionPagedSearchPayload payload)
        {
            ConstructionPagedSearchQuery query = new ConstructionPagedSearchQuery(payload);
            BasePagedSearchDTO<ConstructionPagedSearchDTO> dto = await _mediator.Send(query);
            return Ok(dto);
        }

        [HttpPut]
        [Route("delete/{id:int}")]
        public async Task<ActionResult> Delete(
            [FromRoute(Name = "id")] int constructionId)
        {
            ConstructionDeleteCommand command = new ConstructionDeleteCommand(constructionId);
            await _mediator.Send(command);
            return Ok();
        }

    }
}
