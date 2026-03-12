using MediatR;

namespace Booking.Application.Features.Users.Commands.Register
{
    public class RegisterUserCommand : IRequest<Guid>
    {
        public CreateUserDto UserDto { get; set; }
    }
}
