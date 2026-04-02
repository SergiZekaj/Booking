using Booking.Application.Abstractions.Contracts;
using Booking.Application.Contracts;
using Booking.Domain.Bookings;
using Booking.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Booking.Application.Features.Bookings.Commands.Cancel
{
    internal class CancelBookingCommandHandler : IRequestHandler<CancelBookingCommand, Unit>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;

        public CancelBookingCommandHandler(
            IHttpContextAccessor httpContextAccessor,
            IBookingRepository bookingRepository,
            IUnitOfWork unitOfWork,
            INotificationService notificationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _bookingRepository = bookingRepository;
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
        }

        public async Task<Unit> Handle(CancelBookingCommand command, CancellationToken cancellationToken)
        {
            var uidClaim = _httpContextAccessor.HttpContext!.User.FindFirst("uid")!.Value;
            var userId = Guid.Parse(uidClaim);

            var booking = await _bookingRepository.GetByIdAsync(command.BookingId, cancellationToken);
            if (booking == null)
                throw new Exception("Booking not found.");

            if (booking.GuestId != userId)
                throw new Exception("You are not authorized.");

            if (booking.BookingStatus != BookingStatus.Pending && booking.BookingStatus != BookingStatus.Confirmed)
                throw new Exception("Only pending or confirmed bookings can be cancelled.");

            booking.CancelledOnUtc = DateTime.UtcNow;
            booking.BookingStatus = BookingStatus.Cancelled;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _notificationService.NotifyUserAsync(
                booking.PropertyId.ToString(),
                $"A booking has been cancelled by the guest.");
            return Unit.Value;
        }
    }
}

