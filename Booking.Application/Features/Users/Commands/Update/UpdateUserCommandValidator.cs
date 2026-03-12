using FluentValidation;

namespace Booking.Application.Features.Users.Commands.Update
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Update.FirstName)
                .NotEmpty()
                .MaximumLength(100)
                .When(x => x.Update.FirstName != null);

            RuleFor(x => x.Update.LastName)
                .NotEmpty()
                .MaximumLength(100)
                .When(x => x.Update.LastName != null);

            RuleFor(x => x.Update.PhoneNumber)
                .Matches(@"^\+\d{10,15}$")
                .WithMessage("Phone number must be in format +355691234567")
                .When(x => x.Update.PhoneNumber != null);
        }
    }
}