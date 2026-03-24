using MediatR;

namespace Booking.Application.Features.Reviews.Queries.GetPropertyReviews
{
    public class GetPropertyReviewsQuery : IRequest<List<GetPropertyReviewsDto>>
    {
        public Guid PropertyId { get; set; }
    }
}
