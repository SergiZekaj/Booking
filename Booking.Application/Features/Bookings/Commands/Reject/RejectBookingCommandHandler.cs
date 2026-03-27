using Booking.Application.Abstractions.Contracts;
using Booking.Application.Contracts;
using Booking.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Booking.Application.Features.Bookings.Commands.Reject
{
    internal class RejectBookingCommandHandler : IRequestHandler<RejectBookingCommand, Unit>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBookingRepository _bookingRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RejectBookingCommandHandler(
            IHttpContextAccessor httpContextAccessor,
            IBookingRepository bookingRepository,
            IPropertyRepository propertyRepository,
            IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _bookingRepository = bookingRepository;
            _propertyRepository = propertyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RejectBookingCommand command, CancellationToken cancellationToken)
        {
            var uidClaim = _httpContextAccessor.HttpContext!.User.FindFirst("uid")!.Value;
            var userId = Guid.Parse(uidClaim);

            var booking = await _bookingRepository.GetByIdAsync(command.BookingId, cancellationToken);
            if (booking == null)
                throw new Exception("Booking not found.");

            if (booking.BookingStatus != BookingStatus.Pending)
                throw new Exception("Only pending bookings can be rejected.");

            var property = await _propertyRepository.GetByIdAsync(booking.PropertyId, cancellationToken);
            if (property == null)
                throw new Exception("Property not found.");

            if (property.OwnerId != userId)
                throw new Exception("You are not authorized.");

            booking.RejectedOnUtc = DateTime.UtcNow;
            booking.BookingStatus = BookingStatus.Rejected;

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}

