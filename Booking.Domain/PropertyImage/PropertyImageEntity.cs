using Booking.Domain.Estate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Booking.Domain.PropertyImage
{
    public class PropertyImageEntity
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey(nameof(Property))]
        public Guid PropertyId { get; set; }
        public PropertyEntity Property { get; set; } = null!;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
