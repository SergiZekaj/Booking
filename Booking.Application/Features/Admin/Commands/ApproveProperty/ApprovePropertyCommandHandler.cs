using Booking.Application.Abstractions.Contracts;
using Booking.Application.Contracts;
using MediatR;

namespace Booking.Application.Features.Admin.Commands.ApproveProperty
{
    internal class ApprovePropertyCommandHandler : IRequestHandler<ApprovePropertyCommand, Unit>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ApprovePropertyCommandHandler(IPropertyRepository propertyRepository, IUnitOfWork unitOfWork)
        {
            _propertyRepository = propertyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ApprovePropertyCommand command, CancellationToken cancellationToken)
        {
            var property = await _propertyRepository.GetByIdAsync(command.PropertyId, cancellationToken);
            if (property == null)
                throw new Exception("Property not found.");

            property.IsApproved = true;
            property.IsActive = true;
            property.LastModifiedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}