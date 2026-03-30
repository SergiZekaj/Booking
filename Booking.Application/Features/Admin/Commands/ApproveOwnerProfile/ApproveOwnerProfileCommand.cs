using MediatR;

namespace Booking.Application.Features.Admin.Commands.ApproveOwnerProfile
{
    public class ApproveOwnerProfileCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
    }
}