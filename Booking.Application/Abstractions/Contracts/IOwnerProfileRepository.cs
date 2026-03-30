using Booking.Domain.OwnerProfiles;

namespace Booking.Application.Abstractions.Contracts
{
    public interface IOwnerProfileRepository : IGenericRepository<OwnerProfileEntity>
    {
        Task<OwnerProfileEntity?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<List<OwnerProfileEntity>> GetAllAsync(CancellationToken cancellationToken);
    }
}
