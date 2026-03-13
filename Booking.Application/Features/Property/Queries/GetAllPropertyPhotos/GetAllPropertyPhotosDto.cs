namespace Booking.Application.Features.Property.Queries.GetAllPropertyPhotos
{
    public class GetAllPropertyPhotosDto
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}
