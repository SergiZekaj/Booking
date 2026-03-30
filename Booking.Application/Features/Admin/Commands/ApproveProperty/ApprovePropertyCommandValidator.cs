using FluentValidation;

namespace Booking.Application.Features.Admin.Commands.ApproveProperty
{
    public class ApprovePropertyCommandValidator : AbstractValidator<ApprovePropertyCommand>
    {
        public ApprovePropertyCommandValidator()
        {
            RuleFor(x => x.PropertyId).NotEmpty();
        }
    }
}