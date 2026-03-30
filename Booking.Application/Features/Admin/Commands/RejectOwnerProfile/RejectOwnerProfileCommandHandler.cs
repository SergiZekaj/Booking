using Booking.Application.Abstractions.Contracts;
using Booking.Application.Contracts;
using Booking.Domain.Enums;
using MediatR;

namespace Booking.Application.Features.Admin.Commands.RejectOwnerProfile
{
    internal class RejectOwnerProfileCommandHandler : IRequestHandler<RejectOwnerProfileCommand, Unit>
    {
        private readonly IOwnerProfileRepository _ownerProfileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RejectOwnerProfileCommandHandler(IOwnerProfileRepository ownerProfileRepository, IUnitOfWork unitOfWork)
        {
            _ownerProfileRepository = ownerProfileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RejectOwnerProfileCommand command, CancellationToken cancellationToken)
        {
            var profile = await _ownerProfileRepository.GetByUserIdAsync(command.UserId, cancellationToken);
            if (profile == null)
                throw new Exception("Owner profile not found.");

            if (profile.VerificationStatus == VerificationStatus.Rejected)
                throw new Exception("Owner profile is already rejected.");

            profile.VerificationStatus = VerificationStatus.Rejected;
            profile.LastModifiedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}