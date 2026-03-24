using Booking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Features.OwnerProfile.Queries.GetMyOwnerProfile
{
    public class GetMyOwnerProfileDto
    {
        public string? BusinessName { get; set; }
        public string IdentityCardNumber { get; set; } = null!;
        public VerificationStatus VerificationStatus { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastModifiedAt { get; set; }
    }
}
