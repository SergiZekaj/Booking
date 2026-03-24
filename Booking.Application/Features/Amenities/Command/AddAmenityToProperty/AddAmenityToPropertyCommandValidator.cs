using FluentValidation;

namespace Booking.Application.Features.Amenities.Command.AddAmenityToProperty
{
    public class AddAmenityToPropertyValidator : AbstractValidator<AddAmenityToPropertyCommand>
    {
        public AddAmenityToPropertyValidator()
        {
            RuleFor(x => x.PropertyId).NotEmpty();
            RuleFor(x => x.AmenityId).NotEmpty();
        }
    }
}