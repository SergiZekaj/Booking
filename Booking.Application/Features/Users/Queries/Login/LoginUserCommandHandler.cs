using Booking.Application.Abstractions.Contracts;
using MediatR;

namespace Booking.Application.Features.Users.Queries.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthTokenResult?>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthManager _authManager;

        public LoginUserCommandHandler(IUserRepository userRepository, IAuthManager authManager)
        {
            _userRepository = userRepository;
            _authManager = authManager;
        }

        public async Task<AuthTokenResult?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                return null;

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return null;

            if (!user.IsActive)
                return null;

            return _authManager.GenerateToken(user);
        }
    }
}
