using Booking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Features.Property.Queries.GetAll
{
    public class PropertyFilterDto
    {
        public string? City { get; set; } 
        public PropertyType? PropertyType { get; set; }
        public int? MaxGuests { get; set; }
        public string? Country { get; set; }
    }
}
