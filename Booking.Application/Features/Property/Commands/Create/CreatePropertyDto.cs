using System;
using System.Collections.Generic;
using System.Text;
using Booking.Domain.Enums;
using MediatR;

namespace Booking.Application.Features.Property.Commands.Create
{
    public class CreatePropertyDto
    {
        public Guid OwnerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public PropertyType PropertyType { get; set; }
        public int MaxGuests { get; set; }
        public TimeOnly CheckInTime { get; set; }
        public TimeOnly CheckOutTime { get; set; }
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
    }
}
