using Booking.Application.Abstractions.Contracts;
using Booking.Application.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Features.Users.Commands.RemoveProfilePhoto
{
    internal class RemoveProfilePhotoCommandHandler : IRequestHandler<RemoveProfilePhotoCommand, Unit>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public RemoveProfilePhotoCommandHandler(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(RemoveProfilePhotoCommand command, CancellationToken cancellationToken)
        {

            var userId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst("uid")!.Value);
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
            if (user == null)
                throw new Exception("User doesn't exist.");

            user.ProfileImageUrl = string.Empty;

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
