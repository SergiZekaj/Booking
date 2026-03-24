using Booking.Domain.Amenities;

namespace Booking.Application.Abstractions.Contracts
{
    public interface IPropertyAmenityRepository : IGenericRepository<PropertyAmenityEntity>
    {
        Task<PropertyAmenityEntity?> GetByPropertyAndAmenityAsync(Guid propertyId, Guid amenityId, CancellationToken cancellationToken);
        Task<List<PropertyAmenityEntity>> GetAllByPropertyIdAsync(Guid propertyId, CancellationToken cancellationToken);
        Task<List<AmenityEntity>> GetAmenitiesByPropertyIdAsync(Guid propertyId, CancellationToken cancellationToken);
    }
}