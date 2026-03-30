using Booking.Application.Abstractions.Contracts;
using Booking.Application.Contracts;
using Booking.Domain.Enums;
using MediatR;

namespace Booking.Application.Features.Admin.Commands.ApproveOwnerProfile
{
    internal class ApproveOwnerProfileCommandHandler : IRequestHandler<ApproveOwnerProfileCommand, Unit>
    {
        private readonly IOwnerProfileRepository _ownerProfileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ApproveOwnerProfileCommandHandler(IOwnerProfileRepository ownerProfileRepository, IUnitOfWork unitOfWork)
        {
            _ownerProfileRepository = ownerProfileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ApproveOwnerProfileCommand command, CancellationToken cancellationToken)
        {
            var profile = await _ownerProfileRepository.GetByUserIdAsync(command.UserId, cancellationToken);
            if (profile == null)
                throw new Exception("Owner profile not found.");

            if (profile.VerificationStatus == VerificationStatus.Approved)
                throw new Exception("Owner profile is already approved.");

            profile.VerificationStatus = VerificationStatus.Approved;
            profile.LastModifiedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

    }
}