using FluentValidation;

namespace Booking.Application.Features.Bookings.Commands.Cancel
{
    public class CancelBookingCommandValidator : AbstractValidator<CancelBookingCommand>
    {
        public CancelBookingCommandValidator()
        {
            RuleFor(x => x.BookingId).NotEmpty();
        }
    }
}

