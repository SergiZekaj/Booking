using FluentValidation;

namespace Booking.Application.Features.Bookings.Commands.Create
{
    public class CreateBookingValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingValidator()
        {
            RuleFor(x => x.BookingDto.PropertyId).NotEmpty();
            RuleFor(x => x.BookingDto.GuestCount).GreaterThan(0);

            RuleFor(x => x.BookingDto)
                .Must(d => d.StartDate < d.EndDate)
                .WithMessage("StartDate must be before EndDate.");
        }
    }
}

