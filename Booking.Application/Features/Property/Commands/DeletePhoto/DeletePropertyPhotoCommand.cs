using MediatR;

namespace Booking.Application.Features.Property.Commands.DeletePhoto
{
    public class DeletePropertyPhotoCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
