using Booking.Domain.Addresses;
using Booking.Domain.Bookings;
using Booking.Domain.Enums;
using Booking.Domain.PropertyImage;
using Booking.Domain.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Booking.Domain.Estate
{
    public class PropertyEntity
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey(nameof(Owner))]
        public Guid OwnerId { get; set; }
        public UserEntity Owner { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public PropertyType PropertyType { get; set; }

        [ForeignKey(nameof(Address))]
        public Guid AddressId { get; set; }
        public AddressEntity Address { get; set; } = null!;

        public int MaxGuests { get; set; }
        public TimeOnly CheckInTime { get; set; }
        public TimeOnly CheckOutTime { get; set; }
        public bool IsActive { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastModifiedAt { get; set; }
        public DateTime? LastBookedOnUtc { get; set; } //Booking might not have history

        public ICollection<BookingEntity> Bookings { get; set; } = new List<BookingEntity>();
        public ICollection<PropertyImageEntity> Images { get; set; } = new List<PropertyImageEntity>();
    }
}
