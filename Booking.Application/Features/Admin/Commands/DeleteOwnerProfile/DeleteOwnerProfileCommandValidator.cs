using FluentValidation;

namespace Booking.Application.Features.Admin.Commands.DeleteOwnerProfile
{
    public class DeleteOwnerProfileCommandValidator : AbstractValidator<DeleteOwnerProfileCommand>
    {
        public DeleteOwnerProfileCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}