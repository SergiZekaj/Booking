using FluentValidation;

namespace Booking.Application.Features.Users.Commands.ChangePassword
{
    public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        public ChangeUserPasswordValidator()
        {
            RuleFor(x => x.changeUserPasswordDto.CurrentPassword).NotEmpty();
            RuleFor(x => x.changeUserPasswordDto.ConfirmPassword).NotEmpty();
            RuleFor(x => x.changeUserPasswordDto.NewPassword)
                 .NotEmpty()
                 .MinimumLength(8)
                 .MaximumLength(30)
                 .Matches(@"[A-Z]+").WithMessage("Password must contain at least one uppercase letter")
                 .Matches(@"[a-z]+").WithMessage("Password must contain at least one lowercase letter")
                 .Matches(@"\d+").WithMessage("Password must contain at least one number")
                 .Matches(@"[\!\@\#\$\%\^\&\*\(\)\-\+\=]+")
                 .WithMessage("Password must contain at least one special character (!@#$%^&*()-+=)");
        }
    }
}