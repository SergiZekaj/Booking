using Booking.Application.Abstractions.Contracts;
using Booking.Application.Features.Users.Commands.Register;
using Booking.Application.Features.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Booking.Application.Features.Users.Queries.Login;
using Booking.Application.Features.Users.Queries.GetMyProfile;
using Booking.Application.Features.Users.Commands.Update;
using Booking.Application.Features.Users.Commands.Delete;
using Booking.Application.Features.Users.Commands.UploadProfilePhoto;

namespace Booking.Api.Controllers     // TODO: Admin endpoints - GetAllUsers, ApproveProperty, ManageBookings, ChangeEmail, ReactivateAccount
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

        [HttpGet("my-profile")]
        public async Task<ActionResult<GetMyProfileDto>> GetMyProfile()
        {
            var result = await _mediator.Send(new GetMyProfileQuery());
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<ActionResult<GetMyProfileDto>> Update([FromBody] UpdateUserDto updateUserDto) 
        {
            var command = new UpdateUserCommand
            {
                Update = updateUserDto
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete() 
        {
            await _mediator.Send(new DeleteUserCommand());
            return NoContent();
        }

        [HttpPost("upload-profile-photo")]
        public async Task<ActionResult<string>> UploadProfilePhoto([FromBody] IFormFile file)
        {
            var result = await _mediator.Send(new UploadProfilePhotoCommand { File = file });
            return Ok(result);
        }
    }
}
