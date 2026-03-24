using Booking.Application.Features.OwnerProfile.Queries.GetMyOwnerProfile;
using MediatR;

namespace Booking.Application.Features.OwnerProfile.Commands.Update
{
    public class UpdateOwnerProfileCommand : IRequest<GetMyOwnerProfileDto>
    {
        public UpdateOwnerProfileDto UpdateDto { get; set; } = new();
    }
}