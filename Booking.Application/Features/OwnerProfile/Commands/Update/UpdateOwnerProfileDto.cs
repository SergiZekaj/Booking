namespace Booking.Application.Features.OwnerProfile.Commands.Update
{
    public class UpdateOwnerProfileDto
    {
        public string? BusinessName { get; set; }
        public string? IdentityCardNumber { get; set; } = null!;
    }
}
