using Booking.Domain.PropertyImage;

namespace Booking.Application.Abstractions.Contracts
{
    public interface IPropertyImageRepository : IGenericRepository<PropertyImageEntity>
    {
        Task<List<PropertyImageEntity>> GetAllImagesById(Guid propertyId, CancellationToken cancellationToken);
    }
}
