using MediatR;


namespace Booking.Application.Features.Property.Queries.GetAllPropertyPhotos
{
    public class GetAllPropertyPhotosQuery : IRequest<List<GetAllPropertyPhotosDto>>
    {
        public Guid PropertyId { get; set; }
    }
}
