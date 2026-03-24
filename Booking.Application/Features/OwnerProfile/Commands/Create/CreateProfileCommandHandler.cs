using Booking.Application.Abstractions.Contracts;
using Booking.Application.Contracts;
using Booking.Domain.Enums;
using Booking.Domain.OwnerProfiles;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Booking.Application.Features.OwnerProfile.Commands.Create
{
    public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, Guid>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOwnerProfileRepository _ownerProfileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProfileCommandHandler(IHttpContextAccessor contextAccessor, IOwnerProfileRepository ownerProfileRepository, IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = contextAccessor;
            _ownerProfileRepository = ownerProfileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst("uid")!.Value);
            var dto = request.OwnerProfileDto;
            var existingProfile = await _ownerProfileRepository.GetByUserIdAsync(userId, cancellationToken);
            if (existingProfile != null)
                throw new Exception("Owner profile already exists.");

            var ownerProfile = new OwnerProfileEntity
            {
                UserId = userId,
                IdentityCardNumber = dto.IdentityCardNumber,
                BusinessName = dto.BusinessName,
                VerificationStatus = VerificationStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            await _ownerProfileRepository.AddAsync(ownerProfile, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return userId;
        }
    }
}
