namespace Booking.Application.Features.Amenities.Command.Create
{
    public class CreateAmenityDto
    {
        public string Name { get; set; } = string.Empty; 
        public string? Description { get; set; }
    }
}
