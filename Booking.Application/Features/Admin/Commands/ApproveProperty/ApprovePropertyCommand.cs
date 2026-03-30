using MediatR;

namespace Booking.Application.Features.Admin.Commands.ApproveProperty
{
    public class ApprovePropertyCommand : IRequest<Unit>
    {
        public Guid PropertyId { get; set; }
    }
}