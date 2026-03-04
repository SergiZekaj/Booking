using FluentValidation;

namespace Booking.Application.Features.Property.Commands.Create
{
    public class CreatePropertyValidator : AbstractValidator<CreatePropertyCommand>
    {
        public CreatePropertyValidator()
        {
            RuleFor(x => x.PropertyDto.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.PropertyDto.Description)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.PropertyDto.MaxGuests)
                .GreaterThan(0);

            RuleFor(x => x.PropertyDto.Country)
                .NotEmpty();

            RuleFor(x => x.PropertyDto.City)
                .NotEmpty();

            RuleFor(x => x.PropertyDto.Street)
                .NotEmpty();

            RuleFor(x => x.PropertyDto.PostalCode)
                .NotEmpty();

            RuleFor(x => x.PropertyDto.OwnerId)
                .NotEmpty();
        }
    }
}