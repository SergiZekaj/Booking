using MediatR;
using Booking.Application.Abstractions.Contracts;
using Booking.Domain.Users;

namespace Booking.Application.Features.Users.Register
{
    internal class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        public RegisterUserCommandHandler(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var dto = request.UserDto; 

            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
                throw new Exception("Email already registered");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new UserEntity
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PasswordHash = passwordHash,
                PhoneNumber = dto.PhoneNumber ?? string.Empty,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return user.Id;
        }
    }
}
