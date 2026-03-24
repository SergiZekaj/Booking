namespace Booking.Application.Features.Reviews.Queries.GetPropertyReviews
{
    public class GetPropertyReviewsDto
    {
        public Guid Id { get; set; }
        public string GuestFirstName { get; set; } = string.Empty;
        public string GuestLastName { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
