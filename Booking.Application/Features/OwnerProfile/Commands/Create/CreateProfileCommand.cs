using MediatR;

namespace Booking.Application.Features.OwnerProfile.Commands.Create
{
    public class CreateProfileCommand : IRequest<Guid>
    {
        public CreateOwnerProfileDto OwnerProfileDto { get; set; } = new();
    }
}
