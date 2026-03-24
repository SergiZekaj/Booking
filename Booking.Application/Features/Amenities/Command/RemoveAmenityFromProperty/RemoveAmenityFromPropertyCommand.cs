using MediatR;

namespace Booking.Application.Features.Amenities.Command.RemoveAmenityFromProperty
{
    public class RemoveAmenityFromPropertyCommand : IRequest<Unit>
    {
        public Guid PropertyId { get; set; }
        public Guid AmenityId { get; set; }
    }
}