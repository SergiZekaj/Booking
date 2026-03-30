using Booking.Application.Features.Amenities.Command.AddAmenityToProperty;
using Booking.Application.Features.Amenities.Command.Create;
using Booking.Application.Features.Amenities.Command.RemoveAmenityFromProperty;
using Booking.Application.Features.Amenities.Queries.GetAllAmenities;
using Booking.Application.Features.Amenities.Queries.GetPropertyAmenities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AmenityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AmenityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateAmenityDto dto)
        {
            var result = await _mediator.Send(new CreateAmenityCommand { AmenityDto = dto });
            return Ok(result);
        }

        [HttpGet("all")]
        [Authorize(Roles = "Host")]
        public async Task<ActionResult<List<GetAllMyAmenitiesDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllMyAmenitiesQuery());
            return Ok(result);
        }

        [HttpGet("property/{propertyId}")]
        [Authorize(Roles = "Host")]
        public async Task<ActionResult<List<GetPropertyAmenitiesDto>>> GetPropertyAmenities([FromRoute] Guid propertyId)
        {
            var result = await _mediator.Send(new GetPropertyAmenitiesQuery { PropertyId = propertyId });
            return Ok(result);
        }

        [HttpPost("add-to-property")]
        [Authorize(Roles = "Host")]
        public async Task<IActionResult> AddToProperty([FromBody] AddAmenityToPropertyCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("remove-from-property")]
        [Authorize(Roles = "Host")]
        public async Task<IActionResult> RemoveFromProperty([FromBody] RemoveAmenityFromPropertyCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}