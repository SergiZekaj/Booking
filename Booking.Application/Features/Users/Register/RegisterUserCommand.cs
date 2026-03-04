using MediatR;

namespace Booking.Application.Features.Users.Register
{
    public class RegisterUserCommand : IRequest<Guid>
    {
        public CreateUserDto UserDto { get; set; }
    }
}
