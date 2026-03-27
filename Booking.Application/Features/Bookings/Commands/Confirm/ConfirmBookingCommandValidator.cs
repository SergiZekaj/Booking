using FluentValidation;

namespace Booking.Application.Features.Bookings.Commands.Confirm
{
    public class ConfirmBookingCommandValidator : AbstractValidator<ConfirmBookingCommand>
    {
        public ConfirmBookingCommandValidator()
        {
            RuleFor(x => x.BookingId).NotEmpty();
        }
    }
}

