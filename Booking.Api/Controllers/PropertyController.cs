using Booking.Application.Features.Property.Commands.Create;
using Booking.Application.Features.Property.Commands.Delete;
using Booking.Application.Features.Property.Commands.Update;
using Booking.Application.Features.Property.Queries.GetAll;
using Booking.Application.Features.Property.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreatePropertyDto propertyDto)
        {
            var command = new CreatePropertyCommand
            {
                PropertyDto = propertyDto
            };

            var propertyId = await _mediator.Send(command);
            return CreatedAtAction(nameof(Create), new { id = propertyId }, propertyId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetPropertyByIdDto>> Get([FromRoute] Guid id)
        {
            var query =  await _mediator.Send(new GetPropertyByIdQuery { Id = id });

            return Ok(query);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<GetAllPropertiesDto>>> Get([FromQuery] PropertyFilterDto filter)
        {
            var result = await _mediator.Send(new GetAllPropertiesQuery { Filter = filter });
            return Ok(result);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<GetPropertyByIdDto>> Update([FromRoute] Guid id, [FromBody] UpdatePropertyDto updateDto)
        {
            var command = new UpdatePropertyCommand
            {
                Id = id,
                Update = updateDto
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id) 
        {
            await _mediator.Send(new DeletePropertyCommand { Id = id });
            return NoContent();
        }
    }
}