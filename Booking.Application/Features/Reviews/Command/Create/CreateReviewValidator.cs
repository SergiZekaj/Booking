using FluentValidation;

namespace Booking.Application.Features.Reviews.Command.Create
{
    public class CreateReviewValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewValidator()
        {
            RuleFor(x => x.ReviewDto.BookingId)
                .NotEmpty();
            RuleFor(x => x.ReviewDto.Rating)
                .InclusiveBetween(1, 5);
            RuleFor(x => x.ReviewDto.Comment)
                .NotEmpty();
        }
    }
}
