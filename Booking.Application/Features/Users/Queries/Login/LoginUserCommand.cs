using Booking.Application.Abstractions.Contracts;
using MediatR;

namespace Booking.Application.Features.Users.Queries.Login
{
    public class LoginUserCommand : IRequest<AuthTokenResult?>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
