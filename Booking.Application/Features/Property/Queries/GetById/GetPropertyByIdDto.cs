using Booking.Domain.Enums;
using Booking.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Features.Property.Queries.GetById
{
    public class GetPropertyByIdDto
    {
        public Guid Id { get; set; }
        public string OwnerFirstName { get; set; } = string.Empty;
        public string OwnerLastName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public PropertyType PropertyType { get; set; }
        public int MaxGuests { get; set; }
        public TimeOnly CheckInTime { get; set; }
        public TimeOnly CheckOutTime { get; set; }
        public bool IsActive { get; set; }
        public bool IsApproved  { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public DateTime? LastBookedOnUtc { get; set; }
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;

    }
}
