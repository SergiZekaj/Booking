using FluentValidation;

namespace Booking.Application.Features.Property.Commands.Update
{
    public class UpdatePropertyValidator : AbstractValidator<UpdatePropertyCommand>
    {
        public UpdatePropertyValidator()
        {
            RuleFor(x => x.Update.Name)
                .NotEmpty()
                .MaximumLength(100)
                .When(x => x.Update.Name != null);

            RuleFor(x => x.Update.Description)
                .NotEmpty()
                .MaximumLength(500)
                .When(x => x.Update.Description != null);

            RuleFor(x => x.Update.MaxGuests)
                .GreaterThan(0)
                .When(x => x.Update.MaxGuests.HasValue);

            RuleFor(x => x.Update.PropertyType)
               .IsInEnum()
               .WithMessage("Invalid property type.")
               .When(x => x.Update.PropertyType.HasValue);
        }
    }
}
