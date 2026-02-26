using Booking.Domain.Users;

namespace Booking.Application.Abstractions.Contracts
{
    public interface IAuthManager
    {
        AuthTokenResult GenerateToken(UserEntity user);
    }
}
