using Booking.Application.Features.Users.Register;
using Booking.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Guid>> Register([FromBody] CreateUserDto userDto)
        {
            var command = new RegisterUserCommand
            {
                UserDto = userDto
            };

            var userId = await _mediator.Send(command);

            return CreatedAtAction(nameof(Register), new { id = userId }, userId);
        }
    }
}
