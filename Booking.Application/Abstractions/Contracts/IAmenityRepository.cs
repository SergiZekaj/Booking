using Booking.Domain.Amenities;

namespace Booking.Application.Abstractions.Contracts
{
    public interface IAmenityRepository : IGenericRepository<AmenityEntity>
    {
        Task<bool> ExistsAsync(string name, CancellationToken cancellationToken);
    }
}
