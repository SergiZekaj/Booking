using MediatR;

namespace Booking.Application.Features.Amenities.Command.AddAmenityToProperty
{
    public class AddAmenityToPropertyCommand : IRequest<Unit>
    {
        public Guid PropertyId { get; set; }
        public Guid AmenityId  { get; set; }
    }
}
