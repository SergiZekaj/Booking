using Booking.Application.Abstractions.Contracts;
using Booking.Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Features.Admin.Commands.ReactivateAccount
{
    internal class ReactivateAccountCommandHandler : IRequestHandler<ReactivateAccountCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReactivateAccountCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ReactivateAccountCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);
            if (user == null)
                throw new Exception("User not found.");

            if (user.IsActive)
                throw new Exception("Account is already active");

            user.IsActive = true;
            user.LastModifiedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}