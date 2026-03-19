using Booking.Application.Abstractions.Contracts;
using Booking.Application.Contracts;
using Booking.Application.Features.Users.Commands.ChangePassword;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Runtime.Intrinsics.Arm;

namespace Booking.Application.Features.Users.Commands.ForgotPassword
{
    internal class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Unit>
    {
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ForgotPasswordCommandHandler(IEmailService emailService, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _emailService = emailService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ForgotPasswordCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email);
            if (user == null)
                throw new Exception("User not found.");

            user.PasswordResetToken = Guid.NewGuid().ToString();
            user.PasswordResetTokenExpiry = DateTime.UtcNow.AddHours(1);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var resetLink = $"https://localhost:5215/api/v1/user/reset-password?token={user.PasswordResetToken}";
            await _emailService.SendEmailAsync(user.Email, "Password Reset", $"Click here to reset your password: {resetLink}");

            return Unit.Value;
        }
    }
}

