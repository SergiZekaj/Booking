using FluentValidation;

namespace Booking.Application.Features.Bookings.Commands.Reject
{
    public class RejectBookingCommandValidator : AbstractValidator<RejectBookingCommand>
    {
        public RejectBookingCommandValidator()
        {
            RuleFor(x => x.BookingId).NotEmpty();
        }
    }
}

