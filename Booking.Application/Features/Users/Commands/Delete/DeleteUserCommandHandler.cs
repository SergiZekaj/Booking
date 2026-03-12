using Booking.Application.Abstractions.Contracts;
using Booking.Application.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Features.Users.Commands.Delete
{
    internal class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst("uid")!.Value);
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
            if (user == null)
                throw new Exception("User not found.");

            if (!user.IsActive)
                throw new Exception("Account is already deactivated.");

            user.IsActive = false;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
