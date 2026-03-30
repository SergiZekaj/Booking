using FluentValidation;

namespace Booking.Application.Features.Admin.Commands.ApproveOwnerProfile
{
    public class ApproveOwnerProfileCommandValidator : AbstractValidator<ApproveOwnerProfileCommand>
    {
        public ApproveOwnerProfileCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}