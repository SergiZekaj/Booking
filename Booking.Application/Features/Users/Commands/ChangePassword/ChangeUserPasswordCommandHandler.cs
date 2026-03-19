using BCrypt.Net;
using Booking.Application.Abstractions.Contracts;
using Booking.Application.Contracts;
using Booking.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Features.Users.Commands.ChangePassword
{
    internal class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, Unit>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeUserPasswordCommandHandler(IHttpContextAccessor _httpContextAccesor, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = _httpContextAccesor;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ChangeUserPasswordCommand command, CancellationToken cancellationToken)
        {
            var dto = command.changeUserPasswordDto; 

            
            var userId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst("uid")!.Value);
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

            if(user == null)
                throw new Exception("User not found.");

            if(!user.IsActive)
                throw new Exception("User is deactivated");

            if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.PasswordHash))
                throw new Exception("Current password is incorrect");

            if (dto.NewPassword != dto.ConfirmPassword)
                throw new Exception("Passwrods do not match.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            user.LastModifiedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}