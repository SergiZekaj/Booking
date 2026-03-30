using Booking.Domain.Enums;

namespace Booking.Application.Features.Admin.Queries.GetAllOwnerProfiles
{
    public class GetAllOwnerProfilesDto
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string IdentityCardNumber { get; set; } = string.Empty;
        public string? BusinessName { get; set; }
        public VerificationStatus VerificationStatus { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}