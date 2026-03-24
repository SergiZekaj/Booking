using System.ComponentModel.DataAnnotations;

namespace Booking.Domain.Amenities
{
    public class AmenityEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public ICollection<PropertyAmenityEntity> PropertyAmenities { get; set; } = new List<PropertyAmenityEntity>();
    }
}
