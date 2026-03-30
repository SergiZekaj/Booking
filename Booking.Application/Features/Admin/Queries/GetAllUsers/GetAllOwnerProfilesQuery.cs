using MediatR;

namespace Booking.Application.Features.Admin.Queries.GetAllOwnerProfiles
{
    public class GetAllOwnerProfilesQuery : IRequest<List<GetAllOwnerProfilesDto>> { }
}