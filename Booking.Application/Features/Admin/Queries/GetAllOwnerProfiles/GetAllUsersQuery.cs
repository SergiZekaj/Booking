using MediatR;

namespace Booking.Application.Features.Admin.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<List<GetAllUsersDto>> 
    {
        
    }
}