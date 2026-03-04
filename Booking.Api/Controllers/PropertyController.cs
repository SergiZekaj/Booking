using Booking.Application.Features.Property.Commands.Create;
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
    }
}