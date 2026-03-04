using Booking.Application.Abstractions.Contracts;
using Booking.Application.Features.Users.Login;
using Booking.Application.Features.Users.Register;
using Booking.Application.Features.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
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

        [HttpPost("login")]
        public async Task<ActionResult<AuthTokenResult>> Login([FromBody] LoginUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (result == null)
                return Unauthorized();

            return Ok(result);
        }
    }
}
