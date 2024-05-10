using BuildWise.DTO.Product;
using BuildWise.DTO;
using BuildWise.Payload.Product;
using BuildWise.Payload.Sale;
using BuildWise.Services.Command.Sale;
using BuildWise.Services.Command.Service;
using BuildWise.Services.Query.Product;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BuildWise.Services.Query.ServiceOrder;
using BuildWise.DTO.ServiceOrder;
using BuildWise.Payload.ServiceOrder;
using BuildWise.Services.Command.Product;
using BuildWise.Services.Command.ServiceOrder;

namespace BuildWise.Controller
{
    [Route("service-order")]
    [ApiController]
    public class ServiceOrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ServiceOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<int>> Insert(
            [FromBody] ServiceOrderInsertPayload payload)

        {
            ServiceOrderInsertCommand command = new ServiceOrderInsertCommand(payload);
            int id = await _mediator.Send(command);
            return Ok(id);
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> Update(
         [FromRoute(Name = "id")] int serviceId,
         [FromBody] ServiceOrderUpdatePayload payload)
        {
            ServiceOrderUpdateCommand command = new ServiceOrderUpdateCommand(serviceId, payload);
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Entities.ServiceOrder>> GetById(
            [FromRoute(Name = "id")] int serviceId)
        {
            ServiceOrderGetQuery query = new ServiceOrderGetQuery(serviceId);
            Entities.ServiceOrder serviceOrder = await _mediator.Send(query);
            return Ok(serviceOrder);
        }

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<BasePagedSearchDTO<ServiceOrderPagedSearchDTO>>> Search(
            [FromQuery] ServiceOrderPagedSearchPayload payload)
        {
            ServiceOrderSearchQuery query = new ServiceOrderSearchQuery(payload);
            BasePagedSearchDTO<ServiceOrderPagedSearchDTO> products = await _mediator.Send(query);
            return Ok(products);
        }

        [HttpPut]
        [Route("delete/{id:int}")]
        public async Task<ActionResult> Delete(
            [FromRoute(Name = "id")] int serviceId)
        {
            ServiceOrderDeleteCommand command = new ServiceOrderDeleteCommand(serviceId);
            await _mediator.Send(command);
            return Ok();
        }
    }
}
