using MediatR;

namespace Booking.Application.Features.Admin.Commands.ReactivateAccount
{
    public class ReactivateAccountCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
    }
}