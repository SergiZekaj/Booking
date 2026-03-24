using MediatR;

namespace Booking.Application.Features.Amenities.Queries.GetPropertyAmenities
{
    public class GetPropertyAmenitiesQuery : IRequest<List<GetPropertyAmenitiesDto>>
    {
        public Guid PropertyId { get; set; }
    }
}
