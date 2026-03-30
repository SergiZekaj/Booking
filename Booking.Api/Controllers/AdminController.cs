using Booking.Application.Features.Admin.Commands.ApproveOwnerProfile;
using Booking.Application.Features.Admin.Commands.ApproveProperty;
using Booking.Application.Features.Admin.Commands.DeleteOwnerProfile;
using Booking.Application.Features.Admin.Commands.ReactivateAccount;
using Booking.Application.Features.Admin.Commands.RejectOwnerProfile;
using Booking.Application.Features.Admin.Queries.GetAllOwnerProfiles;
using Booking.Application.Features.Admin.Queries.GetAllUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("users")]
        public async Task<ActionResult<List<GetAllUsersDto>>> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return Ok(result);
        }

        [HttpGet("owner-profiles")]
        public async Task<ActionResult<List<GetAllOwnerProfilesDto>>> GetAllOwnerProfiles()
        {
            var result = await _mediator.Send(new GetAllOwnerProfilesQuery());
            return Ok(result);
        }

        [HttpPost("properties/approve")]
        public async Task<IActionResult> ApproveProperty([FromBody] ApprovePropertyCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("users/reactivate")]
        public async Task<IActionResult> ReactivateAccount([FromBody] ReactivateAccountCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("owner-profiles/approve")]
        public async Task<IActionResult> ApproveOwnerProfile([FromBody] ApproveOwnerProfileCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("owner-profiles/reject")]
        public async Task<IActionResult> RejectOwnerProfile([FromBody] RejectOwnerProfileCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("owner-profiles/delete")]
        public async Task<IActionResult> DeleteOwnerProfile([FromBody] DeleteOwnerProfileCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}