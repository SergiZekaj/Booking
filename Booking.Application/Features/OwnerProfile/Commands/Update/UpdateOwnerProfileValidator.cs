using FluentValidation;

namespace Booking.Application.Features.OwnerProfile.Commands.Update
{
    public class UpdateOwnerProfileValidator : AbstractValidator<UpdateOwnerProfileCommand>
    {
        public UpdateOwnerProfileValidator() 
        {
            RuleFor(x => x.UpdateDto.BusinessName)
                .NotEmpty()
                .When(x => x.UpdateDto.BusinessName != null);

            RuleFor(x => x.UpdateDto.IdentityCardNumber)
                .NotEmpty()
                .When(x => x.UpdateDto.IdentityCardNumber != null);
        }
    }
}
