using MediatR;

namespace Booking.Application.Features.Bookings.Commands.Cancel
{
    public class CancelBookingCommand : IRequest<Unit>
    {
        public Guid BookingId { get; set; }
    }
}

