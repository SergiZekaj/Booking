using MediatR;

namespace Booking.Application.Features.Reviews.Command.Create
{
    public class CreateReviewCommand : IRequest<Guid>
    {
        public CreateReviewDto ReviewDto { get; set; } = new();
    }
}
