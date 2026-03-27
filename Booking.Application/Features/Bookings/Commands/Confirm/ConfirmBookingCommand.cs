using MediatR;

namespace Booking.Application.Features.Bookings.Commands.Confirm
{
    public class ConfirmBookingCommand : IRequest<Unit>
    {
        public Guid BookingId { get; set; }
    }
}

