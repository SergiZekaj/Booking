using Booking.Domain.Enums;
using Booking.Domain.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Booking.Domain.OwnerProfiles
{
    public class OwnerProfileEntity
    {
        [Key]
        public Guid UserId { get; set; }
        public string IdentityCardNumber { get; set; } = null!;
        public VerificationStatus VerificationStatus { get; set; }
        public string? BusinessName { get; set; } //string? because it can be null (Optional)
        public string? StripeAccountId { get; set; } //new
        public bool PayoutsEnabled { get; set; } //new
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastModifiedAt { get; set; }

        public UserEntity User { get; set; } = null!;
    }
}
