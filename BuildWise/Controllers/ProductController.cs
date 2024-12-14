using BuildWise.DTO;
using BuildWise.DTO.Product;
using BuildWise.Payload.Product;
using BuildWise.Services.Command.Product;
using BuildWise.Services.Query.Product;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuildWise.Controller
{
    [Route("product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<int>> Insert(
            [FromBody] ProductInsertPayload payload)
        {
            ProductInsertCommand command = new ProductInsertCommand(payload);
            int id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> Update(
            [FromRoute(Name = "id")] int productId,
            [FromBody] ProductUpdatePayload payload)
        {
            ProductUpdateCommand command = new ProductUpdateCommand(productId, payload);
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Entities.Product>> GetById(
            [FromRoute(Name = "id")] int productId) 
        {
            ProductGetByIdQuery query = new ProductGetByIdQuery(productId);
            Entities.Product product = await _mediator.Send(query);
            return Ok(product);
        }

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<BasePagedSearchDTO<ProductPagedSearchDTO>>> Search(
            [FromQuery] ProductPagedSearchPayload payload)
        {
            ProductPagedSearchQuery query = new ProductPagedSearchQuery(payload);
            BasePagedSearchDTO<ProductPagedSearchDTO> products = await _mediator.Send(query);
            return Ok(products);
        }

        [HttpPut]
        [Route("delete/{id:int}")]
        public async Task<ActionResult> Delete(
        [FromRoute(Name = "id")] int productId)
        {
            ProductDeleteCommand command = new ProductDeleteCommand(productId);
            await _mediator.Send(command);
            return Ok();
        }

    }
}
