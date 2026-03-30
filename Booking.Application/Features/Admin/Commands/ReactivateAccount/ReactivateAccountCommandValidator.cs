using FluentValidation;

namespace Booking.Application.Features.Admin.Commands.ReactivateAccount
{
    public class ReactivateAccountCommandValidator : AbstractValidator<ReactivateAccountCommand>
    {
        public ReactivateAccountCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}