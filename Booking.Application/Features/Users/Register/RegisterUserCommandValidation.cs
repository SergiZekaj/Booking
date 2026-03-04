using FluentValidation;

namespace Booking.Application.Features.Users.Register
{
    public class RegisterUserCommandValidation : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidation()
        {
            RuleFor(x => x.UserDto.FirstName)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.UserDto.LastName)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.UserDto.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.UserDto.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(30)
                .Matches(@"[A-Z]+").WithMessage("Password must contain at least one uppercase letter")
                .Matches(@"[a-z]+").WithMessage("Password must contain at least one lowercase letter")
                .Matches(@"\d+").WithMessage("Password must contain at least one number")
                .Matches(@"[\!\@\#\$\%\^\&\*\(\)\-\+\=]+")
                .WithMessage("Password must contain at least one special character (!@#$%^&*()-+=)");

            RuleFor(x => x.UserDto.PhoneNumber)
                .Matches(@"^\+\d{10,15}$")
                .When(x => x.UserDto.PhoneNumber != null);

        }
    }
}
