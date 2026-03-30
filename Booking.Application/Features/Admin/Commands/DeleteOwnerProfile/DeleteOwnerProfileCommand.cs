using MediatR;

namespace Booking.Application.Features.Admin.Commands.DeleteOwnerProfile
{
    public class DeleteOwnerProfileCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
    }
}