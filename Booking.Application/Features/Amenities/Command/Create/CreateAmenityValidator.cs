using FluentValidation;

namespace Booking.Application.Features.Amenities.Command.Create
{
    public class CreateAmenityValidator : AbstractValidator<CreateAmenityCommand>
    {
        public CreateAmenityValidator()
        {
            RuleFor(x => x.AmenityDto.Name)
                .NotEmpty();
        }
    }
}
