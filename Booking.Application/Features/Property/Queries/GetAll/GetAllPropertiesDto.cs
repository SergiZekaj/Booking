using Booking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Features.Property.Queries.GetAll
{
    public class GetAllPropertiesDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public PropertyType PropertyType { get; set; }
        public int MaxGuests { get; set; }
        public bool IsApproved { get; set; }
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public TimeOnly CheckInTime { get; set; }
        public TimeOnly CheckOutTime { get; set; }

    }
}
