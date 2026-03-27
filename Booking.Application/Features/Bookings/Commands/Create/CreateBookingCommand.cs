using MediatR;

namespace Booking.Application.Features.Bookings.Commands.Create
{
    public class CreateBookingCommand : IRequest<Guid>
    {
        public CreateBookingDto BookingDto { get; set; } = new();
    }
}

