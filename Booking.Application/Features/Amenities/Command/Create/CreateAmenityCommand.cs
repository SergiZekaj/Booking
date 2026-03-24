using MediatR;

namespace Booking.Application.Features.Amenities.Command.Create
{
    public class CreateAmenityCommand : IRequest<Guid>
    {
        public CreateAmenityDto AmenityDto { get; set; } = new();
    }
}
