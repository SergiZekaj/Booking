using Booking.Application.Features.Users.Queries.GetMyProfile;
using MediatR;

namespace Booking.Application.Features.Users.Commands.Update
{
    public class UpdateUserCommand : IRequest<GetMyProfileDto>
    {
        public UpdateUserDto Update { get; set; } = new();
    }
}
