using Booking.Application.Abstractions.Contracts;
using Booking.Application.Contracts;
using Booking.Domain.Roles;
using Booking.Domain.UserRoles;
using Booking.Domain.Users;
using MediatR;

namespace Booking.Application.Features.Users.Commands.Register
{
    internal class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository, IUnitOfWork unitOfWork) 
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var dto = request.UserDto; 

            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
                throw new Exception("Email already registered");

            //var defaultRole = await _roleRepository.GetDefaultRoleAsync(cancellationToken);
            //if (defaultRole == null)
            //    throw new Exception("No default role found");

            RoleEntity? role;
            if (!string.IsNullOrEmpty(dto.Role))
            {
                role = await _roleRepository.GetByNameAsync(dto.Role, cancellationToken);
                if (role == null)
                    throw new Exception($"Role '{dto.Role}' not found.");
            }
            else
            {
                role = await _roleRepository.GetDefaultRoleAsync(cancellationToken);
                if (role == null)
                    throw new Exception("No default role found.");
            }

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

            var userRole = new UserRoleEntity
            {
                UserId = user.Id,
                RoleId = role.Id,
                AssignedAt = DateTime.UtcNow
            };

            user.UserRolesEntity.Add(userRole);


            await _userRepository.AddAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
