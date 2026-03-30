using Booking.Application.Abstractions.Contracts;
using Booking.Application.Contracts;
using MediatR;

namespace Booking.Application.Features.Admin.Commands.DeleteOwnerProfile
{
    internal class DeleteOwnerProfileCommandHandler : IRequestHandler<DeleteOwnerProfileCommand, Unit>
    {
        private readonly IOwnerProfileRepository _ownerProfileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOwnerProfileCommandHandler(IOwnerProfileRepository ownerProfileRepository, IUnitOfWork unitOfWork)
        {
            _ownerProfileRepository = ownerProfileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteOwnerProfileCommand command, CancellationToken cancellationToken)
        {
            var profile = await _ownerProfileRepository.GetByUserIdAsync(command.UserId, cancellationToken);
            if (profile == null)
                throw new Exception("Owner profile not found.");

            _ownerProfileRepository.Delete(profile);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}