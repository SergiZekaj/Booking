using FluentValidation;

namespace Booking.Application.Features.Admin.Commands.RejectOwnerProfile
{
    public class RejectOwnerProfileCommandValidator : AbstractValidator<RejectOwnerProfileCommand>
    {
        public RejectOwnerProfileCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}