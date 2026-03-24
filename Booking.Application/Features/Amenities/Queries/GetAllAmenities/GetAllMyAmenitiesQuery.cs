using MediatR;

namespace Booking.Application.Features.Amenities.Queries.GetAllAmenities
{
    public class GetAllMyAmenitiesQuery : IRequest<List<GetAllMyAmenitiesDto>>
    {
    }
}
