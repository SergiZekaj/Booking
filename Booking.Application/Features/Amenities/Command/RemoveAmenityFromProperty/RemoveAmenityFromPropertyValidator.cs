using FluentValidation;

namespace Booking.Application.Features.Amenities.Command.RemoveAmenityFromProperty
{
    public class RemoveAmenityFromPropertyValidator : AbstractValidator<RemoveAmenityFromPropertyCommand>
    {
        public RemoveAmenityFromPropertyValidator()
        {
            RuleFor(x => x.PropertyId).NotEmpty();
            RuleFor(x => x.AmenityId).NotEmpty();
        }
    }
}