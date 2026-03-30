using Booking.Application.Features.OwnerProfile.Commands.Create;
using Booking.Application.Features.OwnerProfile.Commands.Update;
using Booking.Application.Features.OwnerProfile.Queries.GetMyOwnerProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OwnerProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OwnerProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<Guid>> Register([FromBody] CreateOwnerProfileDto createOwnerProfileDto)
        {
            var command = new CreateProfileCommand
            {
                OwnerProfileDto = createOwnerProfileDto
            };

            var userId = await _mediator.Send(command);

            return CreatedAtAction(nameof(Register), userId);
        }

        [HttpGet("my-profile")]
        [Authorize]
        public async Task<ActionResult<GetMyOwnerProfileDto>> GetMyProfile()
        {
            var result = await _mediator.Send(new GetMyOwnerProfileQuery());
            return Ok(result);
        }

        [HttpPut("update")]
        [Authorize]
        public async Task<ActionResult<UpdateOwnerProfileDto>> Update([FromBody] UpdateOwnerProfileDto updateDto)
        {
            var command = new UpdateOwnerProfileCommand
            {
                UpdateDto = updateDto
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}