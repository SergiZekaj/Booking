using MediatR;

namespace Booking.Application.Features.Bookings.Commands.Reject
{
    public class RejectBookingCommand : IRequest<Unit>
    {
        public Guid BookingId { get; set; }
    }
}

