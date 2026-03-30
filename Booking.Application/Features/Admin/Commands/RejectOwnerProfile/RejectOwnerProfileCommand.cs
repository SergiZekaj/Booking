using MediatR;

namespace Booking.Application.Features.Admin.Commands.RejectOwnerProfile
{
    public class RejectOwnerProfileCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
    }
}