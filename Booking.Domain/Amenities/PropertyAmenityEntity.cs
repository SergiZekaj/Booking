using Booking.Domain.Estate;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Domain.Amenities
{
    public class PropertyAmenityEntity
    {
        [ForeignKey(nameof(Property))]
        public Guid PropertyId { get; set; }
        public PropertyEntity Property { get; set; } = null!;

        [ForeignKey(nameof(Amenity))]
        public Guid AmenityId { get; set;}
        public AmenityEntity Amenity { get; set; } = null!;
    }
}
